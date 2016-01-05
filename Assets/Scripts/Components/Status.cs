using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Pseudo;

[Serializable, ComponentCategory("General")]
public class Status : ComponentBase, IStartable
{
	public float Health = 100f;

	public bool Alive { get { return CurrentHealth > 0; } }
	public float CurrentHealth { get; set; }

	public void Start()
	{
		CurrentHealth = Health;
	}

	protected virtual void OnDamaged(DamageData damage)
	{
		CurrentHealth -= damage.Damage;

		if (CurrentHealth <= 0)
			Entity.SendMessage(EntityMessages.OnDie);
	}
}
