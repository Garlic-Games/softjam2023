using Godot;
using System;
using Softjam2023.Scripts.Entity;

public partial class Hidration : EntityAffectedByWater
{
    [ExportCategory("Humidity Data")]
    [Export]
    private float _temperaturePercentage = 100;
    [Export]
    private float _humidityTickPerSecondByTemperature = 0.5f;
    [Export]
    private float _currentHumidity;
    [Export]
    private float _growthTreshold;
    [Export]
    private float _maxHumidity;
    [Export]
    private float _humidityGainedPerHit = 7;
    
    [ExportCategory("PlantGrowth Settings")]
    [Export]
    private float _minGrowthRatio;
	[Export]
    private float _maxGrowthRatio;

	private Sizer _sizer;
    private Plant _plant;
    private WaterAffectedCollider _collider;
    private float _humidityTickPerSecond => (_humidityTickPerSecondByTemperature/100) * _temperaturePercentage;

    public override void _Ready()
	{
        base._Ready();
		_sizer = GetNode<Sizer>("../Sizer");
        _plant = GetNode<Plant>("../..");
    }

    protected override void OnWaterCollided() {
        _currentHumidity += _humidityGainedPerHit;
        if (_currentHumidity > _maxHumidity) {
            _currentHumidity = _maxHumidity;
        }
        NotifyHumidityChange();
    }


    public override void _PhysicsProcess(double delta)
    {
        DoGrow(delta);
        ReduceHumidity(delta);
    }

    private void ReduceHumidity(double delta)
    {
        if(_currentHumidity <= 0)
        {
            _plant.EmitPlantDead();
            _currentHumidity = 0;
            return;
        }
        _currentHumidity -= Convert.ToSingle(_humidityTickPerSecond * delta);
        NotifyHumidityChange();
    }

    private void NotifyHumidityChange() {
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
    
    public void ApplyTemperature(float percentage)
    {
        _temperaturePercentage = percentage;
    }


    public override float currentHumidityValue => _currentHumidity;
    public override float maximumHumidityValue => _maxHumidity;
}
