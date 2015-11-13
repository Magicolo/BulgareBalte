using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Pseudo;

[RequireComponent(typeof(TimeComponent)), Copy]
public abstract class CharacterBase : PMonoBehaviour, IDamageable, ICopyable<CharacterBase>, IClonable<CharacterBase>
{
	[CopyTo]
	public CharacterStats Stats;
	public Transform WeaponRoot;
	[DoNotCopy]
	public SpriteRenderer Renderer;
	public Color NormalColor = Color.white;
	public Color DamagedColor = Color.red;

	public CharacterStats CurrentStats { get; protected set; }
	public CharacterEquipment CurrentEquipment { get; protected set; }

	Color currentColor;

	readonly CachedValue<TimeComponent> cachedTime;
	public TimeComponent CachedTime { get { return cachedTime; } }

	protected CharacterBase()
	{
		cachedTime = new CachedValue<TimeComponent>(GetComponent<TimeComponent>);
	}

	protected void Awake()
	{
		CurrentStats = CharacterStats.Pool.CreateCopy(Stats);
		CurrentEquipment = CharacterEquipment.Pool.CreateCopy(CharacterEquipment.Default);
	}

	protected virtual void LateUpdate()
	{
		UpdateStatus();
	}

	protected virtual void UpdateStatus()
	{
		UpdateColor();

		if (CurrentStats.Health <= 0f)
			Kill();
	}

	protected virtual void UpdateColor()
	{
		currentColor = Color.Lerp(currentColor, NormalColor, CachedTime.DeltaTime * 3f);
		Renderer.color = currentColor;
	}

	public virtual void EquipWeapon(WeaponBase weaponPrefab)
	{
		WeaponBase weapon = weaponPrefab.Clone();
		weapon.CachedTransform.parent = WeaponRoot;
		weapon.CachedTransform.Copy(weaponPrefab.CachedTransform);
		weapon.Owner = this;

		CurrentEquipment.Weapon = weapon;
	}

	public abstract void Kill();

	public abstract bool CanBeDamagedBy(DamageSources damageSource, DamageTypes damageType);

	public virtual void Damage(DamageData data)
	{
		CurrentStats.Health -= data.Damage;
		currentColor = DamagedColor;
	}

	public override void OnCreate()
	{
		base.OnCreate();

		currentColor = NormalColor;
		CurrentStats.Copy(Stats);
		CurrentEquipment.Copy(CharacterEquipment.Default);
	}

	public void Copy(CharacterBase reference)
	{
		CopyUtility.CopyTo(reference.Stats, ref Stats);
		WeaponRoot = reference.WeaponRoot;
		NormalColor = reference.NormalColor;
		DamagedColor = reference.DamagedColor;
		currentColor = reference.currentColor;
		CurrentStats = reference.CurrentStats;
		CurrentEquipment = reference.CurrentEquipment;
	}

	public virtual CharacterBase Clone()
	{
		return Pools.BehaviourPool.CreateCopy(this);
	}
}
