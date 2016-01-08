using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Pseudo;

[Serializable, ComponentCategory("Spawner")]
public class SpawnOnStart : ComponentBase, IStartable
{
	public void Start()
	{
		Entity.SendMessage(EntityMessages.Spawn);
	}
}
