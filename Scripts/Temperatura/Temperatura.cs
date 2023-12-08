using Godot;
using System;

public partial class Temperatura : Node3D
{
    private enum TemperatureStatus { Calm, Warning, Danger };

    [Signal]
    public delegate void CalmTemperatureReachedEventHandler();
    [Signal]
    public delegate void WarningTemperatureReachedEventHandler();
    [Signal]
    public delegate void DangerTemperatureReachedEventHandler();
    [Signal]
    public delegate void TemperatureChangedEventHandler(float temperaturePercentage);


    [Export]
    private float _warningTreshold = 3;
    [Export]
    private float _dangerTreshold = 6;


    [Export]
    private float _currentTemperature = 1;
    [Export]
    private float _maxTemperature = 10;

    [Export]
    private float _tempAugment = 0.01f;

    [Export]
    private int _heatSources = 0;
    [Export]
    private int _maxHeatSources = 10;

    [Export]
    private float _tempReduction = 0.1f;

    private TemperatureStatus _status = TemperatureStatus.Calm;

    public override void _Process(double delta)
    {
        if (_heatSources == 0)
        {
            ReduceTemperatureByFrame(delta);
            return;
        }
        RunCurrentState();
        AddTemperature();
    }

    public void AddHeatSource()
    {
        _heatSources++;
    }

    public void RemoveHeatSource()
    {
        if (_heatSources > 0)
        {
            _heatSources--;
        }
    }

    private void AddTemperature()
    {
        int heatSources = _heatSources;
        if (_heatSources > _maxHeatSources)
        {
            heatSources = _maxHeatSources;
        }
        _currentTemperature += _tempAugment * heatSources;
        if (_currentTemperature > _maxTemperature)
        {
            _currentTemperature = _maxTemperature;
        }
        EmitSignal(SignalName.TemperatureChanged, (_currentTemperature / _maxTemperature)*100 );
    }

    private void ReduceTemperatureByFrame(double delta)
    {
        if (_currentTemperature <= 0) return;
        _currentTemperature -= Convert.ToSingle(delta * _tempReduction);
        EmitSignal(SignalName.TemperatureChanged, (_currentTemperature / _maxTemperature) * 100);
    }

    private void RunCurrentState()
    {
        switch (_status)
        {
            case TemperatureStatus.Calm:
                if (_currentTemperature > _dangerTreshold)
                {
                    _status = TemperatureStatus.Warning;
                    EmitSignal(SignalName.WarningTemperatureReached);
                }
                break;
            case TemperatureStatus.Warning:
                if (_currentTemperature < _warningTreshold)
                {
                    _status = TemperatureStatus.Calm;
                    EmitSignal(SignalName.CalmTemperatureReached);
                }
                if (_currentTemperature > _dangerTreshold)
                {
                    _status = TemperatureStatus.Danger;
                    EmitSignal(SignalName.DangerTemperatureReached);
                }
                break;
            case TemperatureStatus.Danger:
                if (_currentTemperature < _dangerTreshold)
                {
                    _status = TemperatureStatus.Warning;
                    EmitSignal(SignalName.WarningTemperatureReached);
                }
                break;
        }
    }
}
