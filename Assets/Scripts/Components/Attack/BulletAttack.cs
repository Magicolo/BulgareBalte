using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Pseudo;

public class BulletAttack : AttackBase
{
	[EntityRequires(CanBeNull = false)]
	public PEntity Bullet;

	public override void Attack()
	{
		var bullet = PrefabPoolManager.Create(Bullet);
		bullet.GetComponent<BulletDamager>().SetDamageData(Damager.GetDamageData());
		bullet.CachedTransform.position = Entity.Transform.position;
		bullet.CachedTransform.eulerAngles = Entity.Transform.eulerAngles;
	}
}
