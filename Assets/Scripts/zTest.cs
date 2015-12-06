using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Pseudo;
using System.Threading;
using Pseudo.Internal.Pool;
using Pseudo.Internal.Entity;
using System.Collections.Specialized;
using System.Reflection;
using UnityEngine.Events;

public class zTest : PComponent
{
	public DamageData Damage;
	public Damageable Damageable;

	[Button]
	public bool test;
	void Test()
	{
		PDebug.Log(Damageable.CanBeDamagedBy(Damage));
	}
}