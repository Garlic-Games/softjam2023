using Godot;
using System;

public partial class Hidration : Node3D
{
	[Export]
	private float _currentHumidity;
    [Export]
    private float _growthTreshold;
    [Export]
	private float _maxHumidity;

    [Export]
	private float _minGrowthRatio;
	[Export]
    private float _maxGrowthRatio;

	private Sizer _sizer;

	public override void _Ready()
	{
		_sizer = GetNode<Sizer>("../Sizer");
    }

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if(_currentHumidity >= _growthTreshold)
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
