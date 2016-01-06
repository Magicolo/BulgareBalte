using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Pseudo;

[Serializable]
public class WaveNode
{
	static Predicate<PEntity> spawnersAreDone = spawner => spawner != null && spawner.GetComponent<SpawnerBase>().IsDone;

	public float Delay;
	[EntityRequires(typeof(SpawnerBase), CanBeNull = false)]
	public PEntity[] Spawners;

	public bool IsCompleted { get { return Array.TrueForAll(Spawners, spawnersAreDone); } }

	public void Spawn()
	{
		for (int i = 0; i < Spawners.Length; i++)
			Spawners[i].SendMessage(EntityMessages.Spawn);
	}
}