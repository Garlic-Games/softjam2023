using Godot;
using System;
using System.Collections.Generic;
using Softjam2023.Scripts.Autoload;
using Softjam2023.Scripts.StateMachine;

public partial class EnemySpawner : Node {
    [Export]
    public int minBirds = 4;

    [Export]
    public int maxBirds = 16;

    [Export]
    public int secondPerBird = 7;

    [Export]
    private GameTime _gameTime;

    [Export]
    private BirdSky _birdSky;

    [Export]
    private Node3D _birdRoot;

    [Export]
    private Temperatura _temperatura;

    private ObjectInstanceProviderAutoLoad _autoLoad;
    private List<Bird> _spawnedBirds = new List<Bird>();

    public override void _Ready() {
        _autoLoad = GetNode<ObjectInstanceProviderAutoLoad>("/root/ObjectInstanceProviderAutoLoad");
    }

    public override void _Process(double delta) {
        if (_spawnedBirds.Count > maxBirds) {
            return;
        }

        int howManyBirdsShouldBePresent = minBirds + Mathf.FloorToInt(_gameTime.Time / secondPerBird);
        if (_spawnedBirds.Count < howManyBirdsShouldBePresent) {
            SpawnBird();
        }
    }

    private void SpawnBird() {
        Bird bird = _autoLoad.GimmeABird(_birdSky.getRandomNode());
        bird.CooledDown += _temperatura.RemoveHeatSource;
        bird.WarmedUp += _temperatura.AddHeatSource;
        bird.BirdSky = _birdSky;
        _spawnedBirds.Add(bird);
    }
}