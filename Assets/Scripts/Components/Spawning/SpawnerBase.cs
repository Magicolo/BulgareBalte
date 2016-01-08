using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Pseudo;

[ComponentCategory("Spawner")]
public abstract class SpawnerBase : ComponentBase
{
	public abstract bool IsDone { get; }

	public abstract void Spawn();
}
