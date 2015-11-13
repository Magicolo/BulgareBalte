using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Pseudo;

[Serializable, Copy]
public class CharacterEquipment : IPoolable, ICopyable<CharacterEquipment>
{
	public static readonly Pool<CharacterEquipment> Pool = new Pool<CharacterEquipment>(() => new CharacterEquipment());
	public static readonly CharacterEquipment Default = new CharacterEquipment();

	public WeaponBase Weapon;

	public void OnCreate() { }

	public void OnRecycle() { }

	public void Copy(CharacterEquipment reference)
	{
		Weapon = reference.Weapon;
	}
}
