using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Pseudo;

public abstract class SpawnerBase : PComponent
{
	protected virtual void Update()
	{
		if (ShouldSpawn())
			Spawn();
	}

	public abstract void Spawn();
	protected abstract bool ShouldSpawn();
}
