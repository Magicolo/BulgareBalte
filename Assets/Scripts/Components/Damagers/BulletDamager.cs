using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Pseudo;

[Serializable]
public class BulletDamager : ModifierDamager
{
	[DoNotInitialize]
	public ParticleEffect HitEffect;

	public override void SetDamageData(DamageData damage)
	{
		base.SetDamageData(damage);

		this.damage.Sources = damage.Sources;
		this.damage.Types = damage.Types;
	}

	void OnTriggerEnter2D(Collider2D collision)
	{
		var entity = collision.GetEntity();
		var damageable = entity == null ? null : entity.GetComponent<Damageable>();

		if (damageable != null)
			Damage(damageable);
		else
			Entity.SendMessage(EntityMessages.OnCollide, collision);

		if (HitEffect != null)
			ParticleManager.Instance.Create(HitEffect, Entity.Transform.position);
	}
}
