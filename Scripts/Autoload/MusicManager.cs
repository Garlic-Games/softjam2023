using Godot;
using NathanHoad;
using System;

public partial class MusicManager : Node
{
    private AudioStream _calmMusic = ResourceLoader.Load<AudioStream>("res://Sounds/Song/Tema con intro.wav");
    private AudioStream _warningMusic = ResourceLoader.Load<AudioStream>("res://Sounds/Song/Tema Loop Tension 1.wav");
    private AudioStream _dangerMusic = ResourceLoader.Load<AudioStream>("res://Sounds/Song/Tema Loop Tension 2.wav");
    private AudioStream _transitionMusic = ResourceLoader.Load<AudioStream>("res://Sounds/Song/tension 1 sin transicion.wav");
    private AudioStream _gameOverMusic = ResourceLoader.Load<AudioStream>("res://Sounds/Song/game over.wav");

    private AudioStream _footstep1 = ResourceLoader.Load<AudioStream>("res://Sounds/Sounds/step1.wav");
    private AudioStream _footstep2 = ResourceLoader.Load<AudioStream>("res://Sounds/Sounds/step2.wav");

    private AudioStream _jump = ResourceLoader.Load<AudioStream>("res://Sounds/Sounds/jump.wav");
    private AudioStream _reload = ResourceLoader.Load<AudioStream>("res://Sounds/Sounds/reload.wav");
    private AudioStream _shoot = ResourceLoader.Load<AudioStream>("res://Sounds/Sounds/shoot.wav");

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

    public void PlayGameOver()
    {
        SoundManager.PlayMusic(_gameOverMusic);
    }

    public void PlayJump()
    {
        SoundManager.PlaySound(_jump);
    }

    public void PlayReload()
    {
        SoundManager.PlaySound(_reload);
    }

    public void PlayShoot()
    {
        SoundManager.PlaySound(_shoot);
    }

    public void PlaySteps()
    {
        if (GD.Randf() > 0.5f)
        {
            SoundManager.PlaySound(_footstep2);
        }
        else
        {
            SoundManager.PlaySound(_footstep1);
        }
    }
}
