using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Pseudo;

public class SingleSpawner : SpawnerBase
{
	public PEntity ToSpawn;

	[SerializeField, Button]
	protected bool spawn;

	public override void Spawn()
	{
		var spawned = PrefabPoolManager.Create(ToSpawn);
		spawned.CachedTransform.position = Entity.Transform.position;
	}
}
