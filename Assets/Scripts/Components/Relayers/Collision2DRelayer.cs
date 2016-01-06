using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Pseudo;

public class Collision2DRelayer : PMonoBehaviour
{
	readonly CachedValue<PEntity> cachedEntity;
	public PEntity CachedEntity { get { return cachedEntity.Value; } }

	public Collision2DRelayer()
	{
		cachedEntity = new CachedValue<PEntity>(GetComponent<PEntity>);
	}

	void OnCollisionEnter2D(Collision2D collision)
	{
		CachedEntity.SendMessage(EntityMessages.OnCollisionEnter2D, collision);
	}

	void OnCollisionStay2D(Collision2D collision)
	{
		CachedEntity.SendMessage(EntityMessages.OnCollisionStay2D, collision);
	}

	void OnCollisionExit2D(Collision2D collision)
	{
		CachedEntity.SendMessage(EntityMessages.OnCollisionExit2D, collision);
	}
}
