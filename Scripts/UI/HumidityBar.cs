using Godot;

public partial class HumidityBar : TextureProgressBar
{
	
	public void _Init()
	{
		BarsUI parent = GetParent<BarsUI>();
		parent._plant.HumidityChanged += UpdateValue;
    }

	private void UpdateValue(float value)
	{
		base.Value = value;
	}

}
