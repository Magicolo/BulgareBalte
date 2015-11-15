using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Pseudo;

[RequireComponent(typeof(TimeComponent))]
public abstract class CharacterBase : PMonoBehaviour, IDamageable
{
	[InitializeContent]
	public CharacterStats Stats;
	public Transform WeaponRoot;
	[DoNotInitialize]
	public SpriteRenderer Renderer;
	public Color NormalColor = Color.white;
	public Color DamagedColor = Color.red;

	public CharacterStats CurrentStats { get { return currentStats; } }
	public CharacterEquipment CurrentEquipment { get { return currentEquipment; } }

	protected readonly CharacterStats currentStats = new CharacterStats();
	protected readonly CharacterEquipment currentEquipment = new CharacterEquipment();

	Color currentColor;

	readonly CachedValue<TimeComponent> cachedTime;
	public TimeComponent CachedTime { get { return cachedTime; } }

	protected CharacterBase()
	{
		cachedTime = new CachedValue<TimeComponent>(GetComponent<TimeComponent>);
	}

	protected virtual void LateUpdate()
	{
		UpdateStatus();
	}

	protected virtual void UpdateStatus()
	{
		UpdateColor();

		if (ShouldDie())
			Kill();
	}

	protected virtual void UpdateColor()
	{
		currentColor = Color.Lerp(currentColor, NormalColor, CachedTime.DeltaTime * 3f);
		Renderer.color = currentColor;
	}

	protected virtual bool ShouldDie()
	{
		return currentStats.Health <= 0;
	}

	public virtual void EquipWeapon(WeaponBase weaponPrefab)
	{
		WeaponBase weapon = PoolManager.Create(weaponPrefab);
		weapon.CachedTransform.parent = WeaponRoot;
		weapon.CachedTransform.Copy(weaponPrefab.CachedTransform);
		weapon.Owner = this;

		CurrentEquipment.Weapon = weapon;
	}

	public abstract void Kill();

	public abstract bool CanBeDamagedBy(DamageSources damageSource, DamageTypes damageType);

	public virtual void Damage(DamageData data)
	{
		currentStats.Health -= data.Damage;
		currentColor = DamagedColor;
	}

	public override void OnCreate()
	{
		base.OnCreate();

		currentColor = NormalColor;
		currentStats.Copy(Stats);
		currentEquipment.Copy(CharacterEquipment.Default);
	}
}
