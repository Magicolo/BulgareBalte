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
	public PEntity ToSpawn;

	float nextSpawnTime;

	readonly CachedValue<TimeComponent> cachedTime;
	public TimeComponent CachedTime { get { return cachedTime; } }

	public IntervalSpawner()
	{
		cachedTime = new CachedValue<TimeComponent>(GetComponent<TimeComponent>);
	}

	public override void Spawn()
	{
		PMonoBehaviour spawn = PrefabPoolManager.Create(ToSpawn);
		spawn.Transform.position = Transform.position;
		nextSpawnTime = CachedTime.Time + SpawnInterval;
	}

	protected override bool ShouldSpawn()
	{
		return CachedTime.Time >= nextSpawnTime;
	}
}
