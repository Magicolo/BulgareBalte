using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Pseudo;

public class SingleSpawner : SpawnerBase
{
	public PEntity ToSpawn;

	bool hasSpawned;

	public override void Spawn()
	{
		var spawn = PrefabPoolManager.Create(ToSpawn);
		spawn.CachedTransform.position = CachedTransform.position;
		hasSpawned = true;
	}

	protected override bool ShouldSpawn()
	{
		return !hasSpawned;
	}
}
