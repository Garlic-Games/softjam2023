using Godot;
using System;

public partial class PlayerCamera : Camera3D
{
    [Export]
    private Color _calmBackground;
    [Export]
    private Color _calmAmbientLight;
    [Export]
    private Color _warningBackground;
    [Export]
    private Color _warningAmbientLight;
    [Export]
    private Color _dangerBackground;
    [Export]
    private Color _dangerAmbientLight;


    private Color _currenBackground;
    private Color _currentAmbientLight;
    private Color _nextBackground;
    private Color _nextAmbientLight;

    private bool _transitioning;
    private float _elapsed = 0;
    private float _maxTime = 2;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        Environment.BackgroundColor = _currenBackground = _calmBackground;
        Environment.AmbientLightColor = _currentAmbientLight = _calmAmbientLight;
    }


    public override void _Process(double delta)
    {
        if (_transitioning)
        {
            _elapsed += Mathf.Clamp(Convert.ToSingle(delta), 0, _maxTime);
            
            Environment.BackgroundColor = _currenBackground.Lerp(_nextBackground, _maxTime / _elapsed);
            Environment.AmbientLightColor = _currentAmbientLight.Lerp(_nextAmbientLight, _maxTime / _elapsed);
            if (_elapsed >= _maxTime)
            {
                _currenBackground = _nextBackground;
                _currentAmbientLight = _nextAmbientLight;
                _transitioning = false;
            }
        }
    }



    public void SetCalm()
    {
        StartTransition(_calmBackground, _calmAmbientLight);
    }

    public void SetWarning()
    {
        StartTransition(_warningBackground, _warningAmbientLight);
    }

    public void SetDanger()
    {
        StartTransition(_dangerBackground, _dangerAmbientLight);
    }

    private void StartTransition(Color background, Color ambientLight)
    {
        _nextBackground = background;
        _nextAmbientLight = ambientLight;
        _transitioning = true;
        _elapsed = 0;
    }
}
