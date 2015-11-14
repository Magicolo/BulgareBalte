using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Pseudo;

public abstract class SpawnerBase<T> : PMonoBehaviour where T : UnityEngine.Object
{
	protected virtual void Update()
	{
		if (ShouldSpawn())
			Spawn();
	}

	public abstract T Spawn();
	protected abstract bool ShouldSpawn();
}
