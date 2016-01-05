using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Pseudo;

[Serializable]
public class RecycleOnMessage : ComponentBase
{
	[EnumFlags(typeof(EntityMessages))]
	public ByteFlag RecycleMessages;

	public void Recycle()
	{
		PrefabPoolManager.Recycle(Entity);
	}

	protected virtual void OnDamaged()
	{
		if (RecycleMessages[(byte)EntityMessages.OnDamaged])
			Recycle();
	}

	protected virtual void OnDamage()
	{
		if (RecycleMessages[(byte)EntityMessages.OnDamage])
			Recycle();
	}

	protected virtual void OnDie()
	{
		if (RecycleMessages[(byte)EntityMessages.OnDie])
			Recycle();
	}

	protected virtual void OnCollide()
	{
		if (RecycleMessages[(byte)EntityMessages.OnCollide])
			Recycle();
	}
}
