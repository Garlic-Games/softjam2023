using Godot;
using System;
using Softjam2023.Scripts.Entity;

public partial class Bird : EntityAffectedByWater {
    private enum TemperatureStatus {
        Cool,
        Medium,
        Hot,
        Fire
    };


    [Export]
    public BirdSky BirdSky;

    [Export]
    public float MinCoolThreshold = 0f;

    [Export]
    public float MinMediumThreshold = 30f;

    [Export]
    public float MinHotThreshold = 60f;

    [Export]
    public float MinFireThreshold = 75f;
    
    
    [Export]
    public float RatioMin = 1f;

    [Export]
    public float RatioMax = 2f;

    [Export]
    public float TempLostPerHit = 7f;

    private Node3D seekNode;

    private float Speed = 5f;
    private Vector3 direction_forward = new Vector3(0, 0, -1);
    private float lastDistance = 99999f;
    private bool moving = false;
    private TemperatureStatus _temperatureStatus = TemperatureStatus.Cool;
    
    //
    private float _ratio;

    private float _maxTemp = 100f;
    private float _minTemp = 0f;
    private float _tempCount = 0f;

    private GpuParticles3D _fireParticles;

    public override void _Ready() {
        base._Ready();
        _ratio = GD.Randf() * RatioMax + RatioMin;
        _fireParticles = (GpuParticles3D) FindChild("FireParticles");
        Node3D firstNode = BirdSky.getRandomNode();
        GlobalTransform = firstNode.GlobalTransform;
        NextPath();
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

    private void NextPath() {
        seekNode = BirdSky.getRandomNode();
        lastDistance = GlobalPosition.DistanceTo(seekNode.GlobalPosition);
        LookAt(seekNode.GlobalPosition);
        moving = true;
    }

    public override void _PhysicsProcess(double delta) {
        if (moving) {
            Translate(direction_forward.Normalized() * Speed * (float) delta);
        }

        float newDistance = GlobalPosition.DistanceTo(seekNode.GlobalPosition);
        if (newDistance > lastDistance) {
            moving = false;
            NextPath();
        } else {
            lastDistance = newDistance;
        }

        if (currentHumidityValue > MinFireThreshold) {
            _temperatureStatus = TemperatureStatus.Fire;
        } else if (currentHumidityValue > MinHotThreshold) {
            _temperatureStatus = TemperatureStatus.Hot;
        } else if (currentHumidityValue > MinMediumThreshold) {
            _temperatureStatus = TemperatureStatus.Medium;
        } else {
            _temperatureStatus = TemperatureStatus.Cool;
        }

        if (_temperatureStatus == TemperatureStatus.Fire) {
            if (!_fireParticles.Emitting) {
                _fireParticles.Emitting = true;
            }
        } else {
            if (_fireParticles.Emitting) {
                _fireParticles.Emitting = false;
            }
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