using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Pseudo;

[Serializable]
public class WaveNode
{
	public float Delay;
	[EntityRequires(typeof(SpawnerBase), CanBeNull = false)]
	public PEntity[] Spawners;

	public void Spawn()
	{
		for (int i = 0; i < Spawners.Length; i++)
			Spawners[i].SendMessage("Spawn");
	}
}