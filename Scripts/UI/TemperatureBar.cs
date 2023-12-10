using Godot;
using System;

public partial class TemperatureBar : TextureProgressBar
{
    
    public void _Init(){
        BarsUI parent = GetParent<BarsUI>();
        parent._temperature.TemperatureChanged += UpdateValue;
    }

    private void UpdateValue(float value)
    {
        base.Value = value;
    }
}
