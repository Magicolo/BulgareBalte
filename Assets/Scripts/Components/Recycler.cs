﻿using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Pseudo;

public class Recycler : PComponent
{
	[Flags]
	public enum RecycleMessages
	{
		OnDamaged = 1,
		OnDamage = 2,
		OnDie = 4,
		OnCollide = 8
	}

	public RecycleMessages RecycleMessage
	{
		get { return (RecycleMessages)recycleMessage; }
		set { recycleMessage = (int)value; }
	}

	[SerializeField, EnumFlags(typeof(RecycleMessages))]
	int recycleMessage;

	public void Recycle()
	{
		PrefabPoolManager.Recycle(Entity);
	}

	protected virtual void OnDamaged()
	{
		if ((RecycleMessage & RecycleMessages.OnDamaged) != 0)
			Recycle();
	}

	protected virtual void OnDamage()
	{
		if ((RecycleMessage & RecycleMessages.OnDamage) != 0)
			Recycle();
	}

	protected virtual void OnDie()
	{
		if ((RecycleMessage & RecycleMessages.OnDie) != 0)
			Recycle();
	}

	protected virtual void OnCollide()
	{
		if ((RecycleMessage & RecycleMessages.OnCollide) != 0)
			Recycle();
	}
}
