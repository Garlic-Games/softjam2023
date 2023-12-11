using Godot;

public partial class PlayerAudioManager : Node
{
    [Export]
    private float _reloadCooldownMS = 200f;
    [Export]
    private float _walkCooldownMs = 500f;
    [Export]
    private float _runCooldownMs = 200f;


    private float _lastReload = 0f;
    private float _lastStep = 0f;

    public void PlayShoot()
    {
        MusicManager.Instance.PlayShoot();
    }

    public void PlayWalk()
    {
        Steps(_walkCooldownMs);
    }

    public void PlayRun()
    {
        Steps(_runCooldownMs);
    }

    public void Jump()
    {
        MusicManager.Instance.PlayJump();
    }

    public void Reload()
    {
        if (_lastReload + _reloadCooldownMS < Time.GetTicksMsec())
        {
            _lastReload = Time.GetTicksMsec();
            MusicManager.Instance.PlayReload();
        }
    }

    private void Steps(float cooldown)
    {
        if (_lastStep + cooldown < Time.GetTicksMsec())
        {
            _lastStep = Time.GetTicksMsec();
            MusicManager.Instance.PlaySteps();
        }
    }
}
