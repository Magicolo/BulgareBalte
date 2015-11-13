using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Pseudo;

public abstract class BulletWeapon : WeaponBase, ICopyable<BulletWeapon>
{
	public BulletBase BulletPrefab;

	public override void Fire()
	{
		BulletBase bullet = CreateBullet();
		bullet.Owner = this;
	}

	public virtual BulletBase CreateBullet()
	{
		return Pools.BehaviourPool.CreateCopy(BulletPrefab, CachedTransform.position, CachedTransform);
	}

	public void Copy(BulletWeapon reference)
	{
		base.Copy(reference);

		BulletPrefab = reference.BulletPrefab;
	}
}
