using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Pseudo;

public class InputWeaponAttack : WeaponAttack
{
	public InputManager.Players Input;

	protected virtual void Update()
	{
		if (weapon != null && InputManager.Instance.GetKey(Input, "Attack") && CachedTime.Time - lastAttackTime > 1f / GetAttackSpeed())
			Attack();
	}
}
