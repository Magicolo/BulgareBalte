using UnityEngine;
using System.Collections.Generic;
using Pseudo;
using System;

[Serializable, EntityRequires(typeof(TimeComponent))]
public class BurstLeveledSpawner : SpawnerBase, IUpdateable
{
	public PEntity ToSpawn;

	[Disable]
	public int CurrentLevel = -1;
	public int[] NbSpawned;
	public float[] Interval;

	public override bool IsDone { get { return spawnRemainning == 0; } }

	float nextSpawnTime;
	int spawnRemainning;

	[SerializeField, Button]
	protected bool spawn;

	public override void Spawn()
	{
		CurrentLevel++;
		spawnRemainning = NbSpawned[CurrentLevel];
		nextSpawnTime = Entity.GetComponent<TimeComponent>().Time + Interval[CurrentLevel];
	}


	public float UpdateRate { get { return 0f; } }

	public virtual void Update()
	{
		if (ShouldSpawn())
			RealSpawn();
	}

	protected virtual bool ShouldSpawn()
	{
		return spawnRemainning > 0 && Entity.GetComponent<TimeComponent>().Time >= nextSpawnTime;
	}

	void RealSpawn()
	{
		nextSpawnTime = Entity.GetComponent<TimeComponent>().Time + Interval[CurrentLevel];
		spawnRemainning--;
		var spawned = PrefabPoolManager.Create(ToSpawn);
		spawned.CachedTransform.position = Entity.Transform.position;
	}
}

