using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Pseudo;

[Serializable]
public class SingleSpawner : SpawnerBase
{
	public PEntity ToSpawn;

	public override bool IsDone { get { return isDone; } }

	[SerializeField, Button]
	protected bool spawn;
	bool isDone;

	public override void Spawn()
	{
		var spawned = PrefabPoolManager.Create(ToSpawn);
		spawned.CachedTransform.position = Entity.Transform.position;
		isDone = true;
	}
}
