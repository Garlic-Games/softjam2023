using Godot;
using NathanHoad;
using System;

public partial class MusicManager : Node
{
	[Export]
	private AudioStream _calmMusic = ResourceLoader.Load<AudioStream>("res://Sounds/Song/Tema con intro.wav");
	[Export]
	private AudioStream _warningMusic = ResourceLoader.Load<AudioStream>("res://Sounds/Song/Tema Loop Tension 1.wav");
	[Export]
	private AudioStream _dangerMusic = ResourceLoader.Load<AudioStream>("res://Sounds/Song/Tema Loop Tension 2.wav");
	[Export]
	private AudioStream _transitionMusic = ResourceLoader.Load<AudioStream>("res://Sounds/Song/tension 1 sin transicion.wav");

	[Export]
    private AudioStream[] _footsteps;

    public static MusicManager Instance { get; private set; }

    public override void _Ready()
    {
        Instance = this;
    }

    public void PlayCalmLoop()
	{
		SoundManager.PlayMusicFromPosition(_calmMusic, 20f, 2);
	}

    public void PlayIntro()
    {
        SoundManager.PlayMusic(_calmMusic, 2);
    }

	public void PlayWarning()
	{
        SoundManager.PlayMusic(_warningMusic, 2);
    }

    public void PlayBackFromDanger()
    {
        SoundManager.PlayMusic(_transitionMusic, 2);
    }

    public void PlayDanger()
    {
        SoundManager.PlayMusic(_dangerMusic, 2);
    }
}
