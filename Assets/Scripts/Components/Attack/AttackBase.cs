using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Pseudo;

public abstract class AttackBase : ComponentBase
{
	public DamagerBase Damager;

	public abstract void Attack();
}
