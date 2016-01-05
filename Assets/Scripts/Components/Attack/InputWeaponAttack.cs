using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Pseudo;

[Serializable]
public class InputWeaponAttack : WeaponAttack, IUpdateable
{
	public InputManager.Players Input;

	public float UpdateRate
	{
		get { return 0f; }
	}

	public virtual void Update()
	{
		var time = Entity.GetComponent<TimeComponent>();

		if (weapon != null && InputManager.Instance.GetKey(Input, "Attack") && time.Time - lastAttackTime > 1f / GetAttackSpeed())
			Attack();
	}
}
