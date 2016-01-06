using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Pseudo;

[Serializable, EntityRequires(typeof(TimeComponent))]
public class TouchDamager : SourceDamager
{
	public float DamageInterval = 0.5f;
	public DamageTypes Type = DamageTypes.Physical;

	float nextDamageTime;

	public Animator Animator;
	public string AttackTrigger;

	public override DamageData GetDamageData()
	{
		var damage = base.GetDamageData();
		damage.Types = Type;

		return damage;
	}

	void OnTriggerStay2D(Collider2D collision)
	{
		var time = Entity.GetComponent<TimeComponent>();

		if (time.Time < nextDamageTime)
			return;

		var entity = collision.GetEntity();
		var damageable = entity == null ? null : entity.GetComponent<Damageable>();

		if (damageable != null)
		{
			nextDamageTime = time.Time + DamageInterval;
			Damage(damageable);
			Animator.SetTrigger(AttackTrigger);
		}
	}
}
