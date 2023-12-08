using Godot;

public partial class HumidityBar : TextureProgressBar
{
	[Export]
	public Plant _plant;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_plant.HumidityChanged += UpdateValue; 

    }

	private void UpdateValue(float value)
	{
		base.Value = value;
	}

}
