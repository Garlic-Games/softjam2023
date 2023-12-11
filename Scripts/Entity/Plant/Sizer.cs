using Godot;

public partial class Sizer : Node3D
{

    private Plant _parent;

    [Export]
    private float _sizeGrowth = 0.1f;
    [Export]
    private float _currentSize = 0;
    [Export]
    private float _maxSize = 10;

    public override void _Ready()
	{
		_parent = GetNode<Plant>("../..");
	}

    public void Grow(float growthRate)
    {
        if(_currentSize >= _maxSize)
        {
            _currentSize = _maxSize;
            _parent.EmitMaxSize();
            return;
        }
        _currentSize += _sizeGrowth * growthRate;

        _parent.Translate(Vector3.Up * _sizeGrowth * growthRate);
        _parent.EmitGrowth((_currentSize / _maxSize) * 100);
    }
}
