using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Pseudo;

[Serializable, EntityRequires(typeof(TimeComponent))]
public class IntervalSpawner : SpawnerBase, IUpdateable
{
	[Min]
	public float Interval = 1f;
	public PEntity ToSpawn;

	float nextSpawnTime;

	public override bool IsDone { get { return false; } }
	public float UpdateRate { get { return 0f; } }

	public virtual void Update()
	{
		if (ShouldSpawn())
			Spawn();
	}

	public override void Spawn()
	{
		PMonoBehaviour spawn = PrefabPoolManager.Create(ToSpawn);
		spawn.CachedTransform.position = Entity.Transform.position;
		nextSpawnTime = Entity.GetComponent<TimeComponent>().Time + Interval;
	}

	protected virtual bool ShouldSpawn()
	{
		return Entity.GetComponent<TimeComponent>().Time >= nextSpawnTime;
	}
}
