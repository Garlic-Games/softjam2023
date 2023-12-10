using Godot;

namespace Softjam2023.Scripts.UI; 

public partial class AmmoBar : TextureProgressBar {
    public void _Init()
    {
        BarsUI parent = GetParent<BarsUI>();
        InteractRifle interactRifle = parent._mainPlayerController.interactRifle;
        interactRifle.GunChargeChanged += UpdateValue;
        base.Value = interactRifle.GunCharge;
        base.MaxValue = interactRifle.MaxCharge;
    }

    private void UpdateValue(float value)
    {
        base.Value = value;
    }
}