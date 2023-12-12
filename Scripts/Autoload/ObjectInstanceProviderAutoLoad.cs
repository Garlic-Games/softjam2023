using Godot;
using Softjam2023.Scripts.Player;

namespace Softjam2023.Scripts.Autoload;

public partial class ObjectInstanceProviderAutoLoad : Node {

    private DebugViewPortAutoLoad _debugViewPortAutoLoad;
    public override void _Ready()
    {
        
        _debugViewPortAutoLoad = 
            GetNode<DebugViewPortAutoLoad>("/root/DebugViewPortAutoLoad");
    }

    public WaterProjectile GimmeAWaterProjectile()
    {
        
        string path = "res://Prefabs/Player/water_projectile.tscn";

        if (ResourceLoader.Load(path) is PackedScene projectileResource)
        {
            var projectile = projectileResource.Instantiate();
            AddChild(projectile);
            return projectile as WaterProjectile;
        }

        return null;
    }

    public WaterExplosion GimmeAWaterExplosion()
    {
        
        string path = "res://Prefabs/Player/water_explosion.tscn";

        if (ResourceLoader.Load(path) is PackedScene explosionResource)
        {
            var explosion = explosionResource.Instantiate();
            AddChild(explosion);
            return explosion as WaterExplosion;
        }

        return null;
    }
    
    

    public TestProjectile GimmeATestProjectile()
    {
        
        string path = "res://Prefabs/Player/test_projectile.tscn";

        if (ResourceLoader.Load(path) is PackedScene projectileResource)
        {
            var projectile = projectileResource.Instantiate();
            ((Node3D)projectile).TopLevel = true;
            AddChild(projectile);
            return projectile as TestProjectile;
        }

        return null;
    }

    public TempShower GimmeAProgressBarTempShower()
    {
        
        string path = "res://Prefabs/Temperature/temp_shower.tscn";

        if (ResourceLoader.Load(path) is PackedScene tempshowerResource)
        {
            var shower = tempshowerResource.Instantiate();
            _debugViewPortAutoLoad.AddChild(shower);
            return shower as TempShower;
        }

        return null;
    }

    public Bird GimmeABird(Node3D parent)
    {
        
        string path = "res://Prefabs/Bird/Bird.tscn";

        if (ResourceLoader.Load(path) is PackedScene birdResource)
        {
            var bird = birdResource.Instantiate();
            parent.AddChild(bird);
            return bird as Bird;
        }

        return null;
    }
}

