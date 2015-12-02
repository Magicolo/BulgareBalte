using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Pseudo;

[RequireComponent(typeof(Rigidbody2D))]
public class BulletDamager : ModifierDamager
{
	[DoNotInitialize]
	public ParticleEffect HitEffect;

	public override void SetDamageData(DamageData damage)
	{
		base.SetDamageData(damage);

		this.damage.Source = damage.Source;
		this.damage.Type = damage.Type;
	}

	void OnTriggerEnter2D(Collider2D collision)
	{
		var entity = collision.GetEntity();
		var damageable = entity == null ? null : entity.GetComponent<Damageable>();

		if (damageable != null)
			Damage(damageable);
		else
			SendMessage("OnCollide", collision, SendMessageOptions.DontRequireReceiver);

		if (HitEffect != null)
			ParticleManager.Instance.Create(HitEffect, CachedTransform.position);
	}
}
