using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Pseudo;

public class InputContinuousAttack : AttackBase
{
	public InputManager.Players Input;

	protected override bool ShouldAttack()
	{
		return InputManager.Instance.GetKey(Input, "Attack");
	}
}
