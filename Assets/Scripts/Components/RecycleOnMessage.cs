using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Pseudo;

public class RecycleOnMessage : PComponent
{
	public ByteFlag<EntityMessages> RecycleMessages
	{
		get { return recycleMessages; }
		set { recycleMessages = value; }
	}

	[SerializeField, EnumFlags(typeof(EntityMessages))]
	ByteFlag recycleMessages;

	public void Recycle()
	{
		PrefabPoolManager.Recycle(Entity);
	}

	protected virtual void OnDamaged()
	{
		if (RecycleMessages[EntityMessages.OnDamaged])
			Recycle();
	}

	protected virtual void OnDamage()
	{
		if (RecycleMessages[EntityMessages.OnDamage])
			Recycle();
	}

	protected virtual void OnDie()
	{
		if (RecycleMessages[EntityMessages.OnDie])
			Recycle();
	}

	protected virtual void OnCollide()
	{
		if (RecycleMessages[EntityMessages.OnCollide])
			Recycle();
	}
}
