using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Pseudo;

public abstract class BulletWeapon : WeaponBase
{
	public BulletBase BulletPrefab;

	public override void Fire()
	{
		BulletBase bullet = CreateBullet();
		bullet.Owner = this;
	}

	public virtual BulletBase CreateBullet()
	{
		BulletBase bullet = PoolManager.Create(BulletPrefab);
		bullet.CachedTransform.parent = CachedTransform;
		bullet.CachedTransform.position = CachedTransform.position;

		return bullet;
	}
}
