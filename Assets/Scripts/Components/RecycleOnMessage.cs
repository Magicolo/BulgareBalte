using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Pseudo;

[Serializable, ComponentCategory("General")]
public class RecycleOnMessage : ComponentBase, ILateUpdateable
{
	[EnumFlags(typeof(EntityMessages))]
	public ByteFlag RecycleMessages;

	bool shouldRecycle;

	public float LateUpdateRate
	{
		get { return 0f; }
	}

	public void LateUpdate()
	{
		if (shouldRecycle)
			PrefabPoolManager.Recycle(Entity);
	}

	protected virtual void OnDamaged()
	{
		shouldRecycle |= RecycleMessages[(byte)EntityMessages.OnDamaged];
	}

	protected virtual void OnDamage()
	{
		shouldRecycle |= RecycleMessages[(byte)EntityMessages.OnDamage];
	}

	protected virtual void OnDie()
	{
		shouldRecycle |= RecycleMessages[(byte)EntityMessages.OnDie];
	}

	protected virtual void OnCollide()
	{
		shouldRecycle |= RecycleMessages[(byte)EntityMessages.OnCollide];
	}

	protected virtual void OnTriggerEnter2D(Collider2D collision)
	{
		shouldRecycle |= RecycleMessages[(byte)EntityMessages.OnTriggerEnter2D];
	}

	protected virtual void OnTriggerStay2D(Collider2D collision)
	{
		shouldRecycle |= RecycleMessages[(byte)EntityMessages.OnTriggerStay2D];
	}

	protected virtual void OnTriggerExit2D(Collider2D collision)
	{
		shouldRecycle |= RecycleMessages[(byte)EntityMessages.OnTriggerExit2D];
	}
}
