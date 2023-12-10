using Godot;
using Softjam2023.Scripts.Entity;

public partial class Bullseye : EntityAffectedByWater {
    
    
    [Export]
    public float RatioMin = 1f;

    [Export]
    public float RatioMax = 2f;

    [Export]
    public float TempLostPerHit = 7f;

    //
    private float _ratio;

    private float _maxTemp = 100f;
    private float _minTemp = 0f;
    private float _tempCount = 0f;

    public override void _Ready() {
        base._Ready();
        _ratio = GD.Randf() * RatioMax + RatioMin;
    }

    public override void _Process(double delta) {
        base._Process(delta);
        if (_tempCount < _maxTemp) {
            _tempCount += _ratio * (float) delta;
        }

        if (_tempCount > _maxTemp) {
            _tempCount = _maxTemp;
        }
    }

    protected override void OnWaterCollided() {
        _tempCount -= TempLostPerHit;
        if (_tempCount < _minTemp) {
            _tempCount = _minTemp;
        }
    }

    public override float currentHumidityValue => _tempCount;
    public override float maximumHumidityValue => _maxTemp;
}