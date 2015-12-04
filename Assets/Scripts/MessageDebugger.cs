using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Pseudo;

public class MessageDebugger : PComponent
{

	protected virtual void OnDamaged(DamageData damage)
	{
		PDebug.LogSingleInstance(this, "OnDamaged", damage);
	}

	protected virtual void OnDamage(Damageable damageable)
	{
		PDebug.LogSingleInstance(this, "OnDamage", damageable);
	}

	protected virtual void OnDie()
	{
		PDebug.LogSingleInstance(this, "OnDie");
	}

	protected virtual void OnCollide(Collider2D collision)
	{
		PDebug.LogSingleInstance(this, "OnCollide", collision);
	}
}
