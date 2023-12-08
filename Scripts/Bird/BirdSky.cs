using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public partial class BirdSky : Node3D
{

	[Export] public Node3D pathPointsNode;

	private List<Node3D> pathPoints = new List<Node3D>();
	public override void _Ready()
	{
		foreach (Node3D child in pathPointsNode.GetChildren())
		{
			pathPoints.Add(child);
		}
	}

	public Node3D getRandomNode()
	{
		int i = Mathf.RoundToInt(GD.Randf() * (pathPoints.Count - 1));
		
		return pathPoints[i];
	}
}
