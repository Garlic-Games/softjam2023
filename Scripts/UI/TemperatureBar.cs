using Godot;
using System;

public partial class TemperatureBar : TextureProgressBar
{
    [Export]
    public Temperatura _temperatura;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        _temperatura.TemperatureChanged += UpdateValue;

    }

    private void UpdateValue(float value)
    {
        base.Value = value;
    }
}
