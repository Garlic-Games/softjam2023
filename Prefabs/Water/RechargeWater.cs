using Godot;

public partial class RechargeWater : Node3D
{
	[Export]
    private float _currentLoad = 100;

    public float DrainWater(float ammount)
	{
        _currentLoad -= ammount;
        if(_currentLoad < 0)
        {
            ammount = ammount + _currentLoad;
            _currentLoad = 0;
        }
        return ammount;
	}
}
