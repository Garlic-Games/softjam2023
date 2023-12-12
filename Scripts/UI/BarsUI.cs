using Godot;
using System;
using Softjam2023.Scripts.StateMachine;
using Softjam2023.Scripts.UI;

public partial class BarsUI : CanvasLayer
{
	
	[ExportCategory("Elements data")]
	[Export]
	public Plant _plant;
	
	[Export]
	public Temperatura _temperature;
	
	[Export]
	public MainPlayerController _mainPlayerController;

	[Export]
	public GameTime _gameTime;

	[ExportCategory("UI Elements")]
	[Export]
	public TemperatureBar _temperatureBar;
	[Export]
	public HumidityBar _humidityBar;
	[Export]
	public GrowthBar _growthBar;
	[Export]
	public AmmoBar _ammoBar;
	
	[Export]
	public TimeText _timeShower;
	
	[Export]
	public Panel _pausePanel;
	
	public override void _Ready()
	{
		if (_plant != null) {
			_humidityBar?._Init();
			_growthBar?._Init();
		} else {
			_humidityBar.Visible = false;
			_growthBar.Visible = false;
		}
		if (_temperature != null) {
			_temperatureBar?._Init();
		} else {
			_temperatureBar.Visible = false;
		}
		if (_mainPlayerController != null) {
			_ammoBar?._Init();
		} else {
			_ammoBar.Visible = false;
		}

		if (_gameTime != null) {
			_timeShower?._Init();
		} else {
			_timeShower.Visible = false;
		}
	}
	
	

	public void GamePauseStateChange(bool newState) {
		_pausePanel.Visible = newState;
	}

}
