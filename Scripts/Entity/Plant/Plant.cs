using Godot;
using System;

public partial class Plant : Node3D
{
    [Signal]
    public delegate void HumidityChangedEventHandler(float humidityPercentage);
    [Signal]
    public delegate void PlantDeadEventHandler();
    [Signal]
    public delegate void SizeChangedEventHandler(float newSizePercentage);
    [Signal]
    public delegate void MaxSizeReachedEventHandler();


    public void EmitHumidity(float humidityPercentage)
    {
        EmitSignal(SignalName.HumidityChanged, humidityPercentage);
    }


    public void EmitGrowth(float newSizePercentage)
    {
        EmitSignal(SignalName.SizeChanged, newSizePercentage);
    }


    public void EmitMaxSize()
    {
        EmitSignal(SignalName.MaxSizeReached);
    }

    public void EmitPlantDead()
    {
        EmitSignal(SignalName.PlantDead);
    }
}
