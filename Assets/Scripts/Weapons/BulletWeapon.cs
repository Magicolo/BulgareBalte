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
		bullet.CachedTransform.position = CachedTransform.position;
		bullet.CachedTransform.eulerAngles = CachedTransform.eulerAngles;
	}

	public virtual Bullet CreateBullet()
	{
		Bullet bullet = PoolManager.Create(BulletPrefab);
		bullet.CachedTransform.position = CachedTransform.position;

		return bullet;
	}
}
