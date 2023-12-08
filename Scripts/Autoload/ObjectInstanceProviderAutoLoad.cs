using Godot;
using Softjam2023.Scripts.Player;

namespace Softjam2023.Scripts.Autoload;

public partial class ObjectInstanceProviderAutoLoad : Node
{


    public override void _Ready()
    {
        string path = "res://Prefabs/Player/water_projectile.tscn";

        if (ResourceLoader.Load(path) is PackedScene projectileResource)
        {
            var projectile = projectileResource.Instantiate();
        }
        
        // var scene = scene_resource.instance()
        // parent_node.add_child(scene)


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
}

