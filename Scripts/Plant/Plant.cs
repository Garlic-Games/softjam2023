using Godot;

public partial class Plant : MeshInstance3D
{
    [Signal]
    public delegate void HumidityChangedEventHandler(float humidityPercentage);


    public void EmitHumidity(float humidityPercentage)
    {
        EmitSignal(SignalName.HumidityChanged, humidityPercentage);
    }
}
