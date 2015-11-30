using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Pseudo;

public class ContinuousWeaponAttack : WeaponAttack
{
	protected virtual void Update()
	{
		if (weapon != null)
			Attack();
	}
}
