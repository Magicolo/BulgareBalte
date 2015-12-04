using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Pseudo;
using System.Threading;
using Pseudo.Internal.Pool;
using Pseudo.Internal.Entity;
using System.Collections.Specialized;
using System.Reflection;
using UnityEngine.Events;

public class zTest : PMonoBehaviour
{
	const int iterations = 1000;

	public PEntity Entity;

	[Button]
	public bool test;
	void Test()
	{
		PrefabPoolManager.Create(Entity);
	}
}