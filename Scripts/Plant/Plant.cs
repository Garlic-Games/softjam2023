using Godot;

public partial class Plant : MeshInstance3D
{
    [Signal]
    public delegate float HumidityChangedEventHandler();


    public void EmitHumidity(float humidityPercentage)
    {
        EmitSignal(SignalName.HumidityChanged, humidityPercentage);
    }
}
