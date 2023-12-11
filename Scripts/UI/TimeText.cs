using Godot;
using Softjam2023.Scripts.StateMachine;

namespace Softjam2023.Scripts.UI;

public partial class TimeText : Label {
    private GameTime _gameTime;

    public void _Init() {
        BarsUI parent = GetParent<BarsUI>();
        _gameTime = parent._gameTime;
    }

    public override void _Process(double delta) {
        string newValue = timeToString(_gameTime.Time);
        base.Text = newValue;
    }

    private string timeToString(float time) {
        if (time == null) {
            return "0:00";
        }

        int minutes = Mathf.RoundToInt(time / 60);
        int seconds = Mathf.RoundToInt(time % 60);
        return minutes + ":" + (seconds > 10 ? seconds : ("0" + seconds));
    }
}