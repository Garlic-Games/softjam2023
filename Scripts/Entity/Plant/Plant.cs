using Godot;
using System;
using System.Numerics;

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

    [Export]
    private Temperatura _temperatura;
    private Hidration _hidration;

    public override void _Ready()
    {
        _hidration = GetNode<Hidration>("./Trunk/Hidration");
        if (_temperatura != null)
        {
            _temperatura.TemperatureChanged += ApplyTemperature;
        }
    }

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

    public void ApplyTemperature(float percentage)
    {
        _hidration.ApplyTemperature(percentage);
    }
}
