﻿using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Pseudo;

[Serializable]
public class BulletAttack : AttackBase
{
	[EntityRequires(CanBeNull = false)]
	public PEntity Bullet;

	public override void Attack()
	{
		var damager = Entity.GetComponent<DamagerBase>();
		var bullet = PrefabPoolManager.Create(Bullet);
		bullet.GetComponent<BulletDamager>().SetDamageData(damager.GetDamageData());
		bullet.CachedTransform.position = Entity.Transform.position;
		bullet.CachedTransform.eulerAngles = Entity.Transform.eulerAngles;
		Entity.SendMessage(EntityMessages.OnStartAttacking);
	}
}
