using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Pseudo;
using System.Threading;
using Pseudo.Internal;
using Pseudo.Internal.Pool;

[Serializable]
public class zTest : PMonoBehaviour
{
	public PEntity[] Entities;

	[Button]
	public bool test;
	void Test()
	{

	}

	void Update()
	{

	}

	void PoolTest()
	{
		for (int i = 0; i < Entities.Length; i++)
			PrefabPoolManager.Recycle(PrefabPoolManager.Create(Entities[i]));
	}
}