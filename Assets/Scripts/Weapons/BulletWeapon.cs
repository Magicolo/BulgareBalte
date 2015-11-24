using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Pseudo;

public class BulletWeapon : WeaponBase
{
	public Bullet BulletPrefab;

	public override void Fire(DamageData damage)
	{
		damage.Damage *= DamageModifier;

		Bullet bullet = CreateBullet();
		bullet.Initialize(damage);
		bullet.Transform.position = Transform.position;
		bullet.Transform.eulerAngles = Transform.eulerAngles;
	}

	public virtual Bullet CreateBullet()
	{
		Bullet bullet = PoolManager.Create(BulletPrefab);
		bullet.Transform.position = Transform.position;

		return bullet;
	}
}
