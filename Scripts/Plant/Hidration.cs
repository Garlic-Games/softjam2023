using Godot;
using System;

public partial class Hidration : Node3D
{
    [ExportCategory("Humidity Data")]
    [Export]
    private float _humidityTickPerSecond;
    [Export]
    private float _currentHumidity;
    [Export]
    private float _growthTreshold;
    [Export]
	private float _maxHumidity;

    [ExportCategory("PlantGrowth Settings")]
    [Export]
    private float _minGrowthRatio;
	[Export]
    private float _maxGrowthRatio;

	private Sizer _sizer;
    private Plant _plant;

	public override void _Ready()
	{
		_sizer = GetNode<Sizer>("../Sizer");
        _plant = GetNode<Plant>("../..");
    }

	public override void _Process(double delta)
    {
        DoGrow(delta);
        ReduceHumidity(delta);
    }

    private void ReduceHumidity(double delta)
    {
        if(_currentHumidity <= 0)
        {
            _currentHumidity = 0;
            return;
        }
        _currentHumidity -= Convert.ToSingle(_humidityTickPerSecond * delta);
        _plant.EmitHumidity((_currentHumidity / _maxHumidity) * 100);
    }

    private void DoGrow(double delta)
    {
        if (_currentHumidity >= _growthTreshold)
        {
            float currentGrowthRatio = Convert.ToSingle(CalculateGrowthRatio() * delta);
            _sizer.Grow(currentGrowthRatio);
        }
    }

    private float CalculateGrowthRatio()
    {
        var maxHumidityWithoutTreshHold = _maxHumidity - _growthTreshold;
        var currentHumidityAfterTreshhold = _currentHumidity - _growthTreshold;
        var currentHumidityPercentage = currentHumidityAfterTreshhold / maxHumidityWithoutTreshHold * _maxGrowthRatio;

        var currentGrowthRatio = _minGrowthRatio + ((1/_maxGrowthRatio) * currentHumidityPercentage);
        return currentGrowthRatio;
    }
}
