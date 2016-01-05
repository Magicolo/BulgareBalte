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
	public float Interval = 1f;
	public PEntity ToSpawn;

	float nextSpawnTime;

	readonly CachedValue<TimeComponent> cachedTime;
	public TimeComponent CachedTime { get { return cachedTime; } }

	public IntervalSpawner()
	{
		cachedTime = new CachedValue<TimeComponent>(Entity.GameObject.GetComponent<TimeComponent>);
	}

	protected virtual void Update()
	{
		if (ShouldSpawn())
			Spawn();
	}

	public override void Spawn()
	{
		PMonoBehaviour spawn = PrefabPoolManager.Create(ToSpawn);
		spawn.CachedTransform.position = Entity.Transform.position;
		nextSpawnTime = CachedTime.Time + Interval;
	}

	protected virtual bool ShouldSpawn()
	{
		return CachedTime.Time >= nextSpawnTime;
	}
}
