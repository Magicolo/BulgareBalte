using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Pseudo;

[Serializable]
public class RecycleOnMessage : ComponentBase
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
		if (recycleMessages[(byte)EntityMessages.OnDamaged])
			Recycle();
	}

	protected virtual void OnDamage()
	{
		if (recycleMessages[(byte)EntityMessages.OnDamage])
			Recycle();
	}

	protected virtual void OnDie()
	{
		if (recycleMessages[(byte)EntityMessages.OnDie])
			Recycle();
	}

	protected virtual void OnCollide()
	{
		if (recycleMessages[(byte)EntityMessages.OnCollide])
			Recycle();
	}
}
