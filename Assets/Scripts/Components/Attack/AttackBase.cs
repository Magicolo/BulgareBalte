using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Pseudo;

[ComponentCategory("Attack"), EntityRequires(typeof(DamagerBase))]
public abstract class AttackBase : ComponentBase
{
	public abstract void Attack();
}
