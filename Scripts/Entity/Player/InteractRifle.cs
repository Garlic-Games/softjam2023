using System;
using Godot;
using Godot.Collections;
using Softjam2023.Scripts.Autoload;
using Softjam2023.Scripts.Util;

public partial class InteractRifle : Node
{


    [Export]
    private float _gunCharge = 20; //seconds

    [Export]
    private float _maxGunCharge = 25;

    [Export]
    private float _chargeRatio = 4;

    [Signal]
    public delegate void ShootEventHandler();
    [Signal]
    public delegate void ReloadEventHandler();
    [Signal]
    public delegate void GunChargeChangedEventHandler(float newAmmo);

    private MainPlayerController player;
    private ObjectInstanceProviderAutoLoad _autoLoad;
    private Node2D _drawLine3d;

    private float _spawn_speed = 0.09f;
    private float _cooldown = 0f;
    private float RAY_LENGTH = 2.5f;

    private bool canReload = false;

    public override void _Ready()
    {
        player = (MainPlayerController)FindParent("ControlablePlayer");
        _autoLoad = GetNode<ObjectInstanceProviderAutoLoad>("/root/ObjectInstanceProviderAutoLoad");
        _drawLine3d = GetNode<Node2D>("/root/DrawLine3d");
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
        // GD.Print("Current charge: " + gunCharge);
        bool isShooting = Input.IsActionPressed("player_shoot");
        bool isReloading = Input.IsActionPressed("player_reload");
        bool chargeChanged = false;

        if (isReloading && canReload)
        {
            _gunCharge += (float)delta * _chargeRatio;
            EmitSignal(SignalName.Reload);
            if (_gunCharge > _maxGunCharge)
            {
                _gunCharge = _maxGunCharge;
            }
            chargeChanged = true;
        }

        if (isShooting)
        {
            _gunCharge -= (float)delta;
            chargeChanged = true;
            if (_cooldown < 0 && _gunCharge > 0f)
            {
                doShoot();
                _cooldown = _spawn_speed;
            }

            _cooldown -= (float)delta;
        }
        else
        {
            _cooldown = 0;
        }

        if (chargeChanged)
        {
            EmitSignal(SignalName.GunChargeChanged, _gunCharge);
        }

    }

    public override void _PhysicsProcess(double delta)
    {
        if (_gunCharge >= _maxGunCharge)
        {
            canReload = false;
            player.rifleWeapon.shouldShine = false;
            return;
        }
        PhysicsDirectSpaceState3D spaceState = player.GetWorld3D().DirectSpaceState;

        Transform3D muzzleTransform = player.rifleWeapon.barrelStartPoint.GlobalTransform;
        Vector3 origin = muzzleTransform.Origin;
        Vector3 end = origin + (muzzleTransform.Basis.Z.Normalized() * RAY_LENGTH);

        if (Constants.DebugMode)
        {
            Variant[] drawLineData = new Variant[] { origin, end, new Color(1, 0, 0), 0.3f };
            _drawLine3d.Call("DrawLine", drawLineData);
        }

        PhysicsRayQueryParameters3D query = PhysicsRayQueryParameters3D.Create(origin, end);
        query.CollideWithAreas = true;
        Dictionary result = spaceState.IntersectRay(query);

        canReload = CollidedWithWaterSource(result);
        player.rifleWeapon.shouldShine = canReload;
    }

    private bool CollidedWithWaterSource(Dictionary result)
    {
        if (result.Count > 0 && result.TryGetValue("collider", out Variant collidedObject))
        {
            if (collidedObject.Obj is CollisionObject3D)
            {
                if (((CollisionObject3D)collidedObject)
                    .GetCollisionLayerValue(Constants.CollisionLayers.WaterSource))
                {
                    return true;
                }
            }
        }

        return false;
    }

    private void doShoot()
    {
        var projectile = _autoLoad.GimmeAWaterProjectile();
        // var projectile = _autoLoad.GimmeATestProjectile();
        var newTransform = player.rifleWeapon.barrelEndPoint.GlobalTransform;
        projectile.GlobalTransform = newTransform;
        projectile.LookAt(player.aimPoint.GlobalPosition);
        projectile.Shoot();

        EmitSignal(SignalName.Shoot);
    }

    public float GunCharge
    {
        get => _gunCharge;
    }

    public float MaxCharge
    {
        get => _maxGunCharge;
    }
}