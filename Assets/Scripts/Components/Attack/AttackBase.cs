using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Pseudo;

public abstract class AttackBase : PComponent
{
	public DamagerBase Damager;

	public abstract void Attack();
}
