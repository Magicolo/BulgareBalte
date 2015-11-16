using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Pseudo;

[RequireComponent(typeof(TimeComponent))]
public class IntervalSpawner : SpawnerBase
{
	[Min]
	public float SpawnInterval;
	public PMonoBehaviour ToSpawn;

	float nextSpawnTime;

	readonly CachedValue<TimeComponent> cachedTime;
	public TimeComponent CachedTime { get { return cachedTime; } }

	public IntervalSpawner()
	{
		cachedTime = new CachedValue<TimeComponent>(GetComponent<TimeComponent>);
	}

	public override void Spawn()
	{
		PMonoBehaviour spawn = PoolManager.Create(ToSpawn);
		spawn.CachedTransform.position = CachedTransform.position;
		nextSpawnTime = CachedTime.Time + SpawnInterval;
	}

	protected override bool ShouldSpawn()
	{
		return CachedTime.Time >= nextSpawnTime;
	}
}
