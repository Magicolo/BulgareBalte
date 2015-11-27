﻿using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Pseudo;

public class Status : PComponent
{
	public float Health = 100f;

	public bool Alive { get { return CurrentHealth > 0; } }
	public float CurrentHealth { get; set; }

	protected virtual void OnDamaged(DamageData damage)
	{
		CurrentHealth -= damage.Damage;

		if (CurrentHealth <= 0)
			SendMessage("OnDie", SendMessageOptions.DontRequireReceiver);
	}

	public override void OnCreate()
	{
		base.OnCreate();

		CurrentHealth = Health;
	}
}