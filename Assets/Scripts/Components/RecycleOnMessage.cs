using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Pseudo;

[Serializable, ComponentCategory("General")]
public class RecycleOnMessage : ComponentBase, ILateUpdateable, IMessageable
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

	public void OnMessage(EntityMessages message)
	{
		shouldRecycle |= RecycleMessages[(byte)message];
	}
}
