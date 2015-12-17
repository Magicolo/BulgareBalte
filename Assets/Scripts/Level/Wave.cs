using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Pseudo;

public class Wave : PMonoBehaviour
{
	public WaveNode[] Nodes;

	public bool IsCompleted { get { return waitingNodes.Count == 0; } }

	readonly List<WaveNode> waitingNodes = new List<WaveNode>();

	float counter;

	void OnEnable()
	{
		counter = 0;
		waitingNodes.AddRange(Nodes);
	}

	void OnDisable()
	{
		waitingNodes.Clear();
	}

	void Update()
	{
		counter += TimeManager.World.DeltaTime;

		for (int i = 0; i < waitingNodes.Count; i++)
		{
			var node = waitingNodes[i];

			if (counter >= node.Delay)
			{
				node.Spawn();
				waitingNodes.RemoveAt(i--);
			}
		}
	}
}