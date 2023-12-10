using Godot;
using System;

public partial class GrowthBar : TextureProgressBar
{

    public void _Init()
    {
        BarsUI parent = GetParent<BarsUI>();
        parent._plant.SizeChanged += UpdateValue;

    }

    private void UpdateValue(float value)
    {
        base.Value = value;
    }
}
