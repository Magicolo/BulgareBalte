using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Pseudo;

public class BulletAttack : AttackBase
{
	public PEntity Bullet;

	public override void Attack()
	{
		var bullet = PrefabPoolManager.Create(Bullet);
		bullet.GetComponent<BulletDamager>().SetDamageData(Damager.GetDamageData());
		bullet.CachedTransform.position = CachedTransform.position;
		bullet.CachedTransform.eulerAngles = CachedTransform.eulerAngles;
	}
}
