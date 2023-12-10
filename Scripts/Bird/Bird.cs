using Godot;
using System;

public partial class Bird : Node3D {
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

    private Node3D seekNode;

    private float Speed = 5f;
    private Vector3 direction_forward = new Vector3(0, 0, -1);
    private float lastDistance = 99999f;
    private bool moving = false;
    private TemperatureStatus _temperatureStatus = TemperatureStatus.Cool;

    private WaterAffectedCollider _waterAffectedCollider;
    private GpuParticles3D _fireParticles;

    public override void _Ready() {
        _waterAffectedCollider = (WaterAffectedCollider) FindChild("Hitbox");
        _fireParticles = (GpuParticles3D) FindChild("FireParticles");
        Node3D firstNode = BirdSky.getRandomNode();
        GlobalTransform = firstNode.GlobalTransform;
        NextPath();
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

        if (temperature > MinFireThreshold) {
            _temperatureStatus = TemperatureStatus.Fire;
        } else if (temperature > MinHotThreshold) {
            _temperatureStatus = TemperatureStatus.Hot;
        } else if (temperature > MinMediumThreshold) {
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


    public float temperature => _waterAffectedCollider.temperature;
}