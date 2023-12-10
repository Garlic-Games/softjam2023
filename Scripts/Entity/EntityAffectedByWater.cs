using Godot;
using Softjam2023.Scripts.Autoload;
using Softjam2023.Scripts.Util;

namespace Softjam2023.Scripts.Entity;

public abstract partial class EntityAffectedByWater : Node3D {
    [Export]
    public WaterAffectedCollider _waterAffectedCollider;


    private TempShower _tempShower;

    public override void _Ready() {
        _waterAffectedCollider.WaterCollided += OnWaterCollided;
        if (Constants.DebugMode) {
            ObjectInstanceProviderAutoLoad autoLoad =
                GetNode<ObjectInstanceProviderAutoLoad>("/root/ObjectInstanceProviderAutoLoad");
            _tempShower = autoLoad.GimmeAProgressBarTempShower();
        }
    }

    public override void _Process(double delta) {
        if (_tempShower != null) {
            Camera3D camera = GetViewport().GetCamera3D();
            Vector3 position = GlobalPosition;
            if (camera != null) {
                if (!camera.IsPositionBehind(position)) {
                    Vector2 position2d = camera.UnprojectPosition(GlobalPosition);
                    _tempShower.Show();
                    _tempShower.SetPosition(position2d);
                    _tempShower.SetBarPercent(currentHumidityValue/maximumHumidityValue * 100);
                } else {
                    _tempShower.Hide();
                }
            }
        }
    }


    protected abstract void OnWaterCollided();

    public abstract float currentHumidityValue { get; }
    public abstract float maximumHumidityValue { get; }
}