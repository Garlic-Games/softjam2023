using Godot;
using System;
using Softjam2023.Scripts.Autoload;
using Softjam2023.Scripts.Util;

public partial class WaterAffectedCollider : RigidBody3D {
    [Export]
    public float RatioMin = 1f;

    [Export]
    public float RatioMax = 2f;

    [Export]
    public float TempLostPerHit = 7f;

    private float _maxTemp = 100f;
    private float _minTemp = 0f;

    private float _tempCount = 0f;
    private float _ratio;
    private TempShower _tempShower;

    public override void _Ready() {
        _ratio = GD.Randf() * RatioMax + RatioMin;
        if (Constants.DebugMode) {
            ObjectInstanceProviderAutoLoad autoLoad =
                GetNode<ObjectInstanceProviderAutoLoad>("/root/ObjectInstanceProviderAutoLoad");
            _tempShower = autoLoad.GimmeAProgressBarTempShower();
        }
    }

    public override void _Process(double delta) {
        if (_tempCount < _maxTemp) {
            _tempCount += _ratio * (float) delta;
        }

        if (_tempCount > _maxTemp) {
            _tempCount = _maxTemp;
        }

        if (_tempShower != null) {
            Camera3D camera = GetViewport().GetCamera3D();
            Vector3 position = GlobalPosition;
            if (camera != null) {
                if (!camera.IsPositionBehind(position)) {
                    Vector2 position2d = camera.UnprojectPosition(GlobalPosition);
                    _tempShower.Show();
                    _tempShower.SetPosition(position2d);
                    _tempShower.SetBarPercent(_tempCount);
                } else {
                    _tempShower.Hide();
                }
            }
        }
    }

    public void NotifyWaterCollision() {
        _tempCount -= TempLostPerHit;
        if (_tempCount < _minTemp) {
            _tempCount = _minTemp;
        }
    }

    public float temperature => _tempCount;
}