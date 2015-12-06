using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Pseudo;

public class SpawnOnStart : PComponent
{
	protected override void Start()
	{
		base.Start();

		Entity.SendMessage("Spawn");
	}
}
