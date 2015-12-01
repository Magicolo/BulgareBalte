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

[Serializable]
public class zTest : PMonoBehaviour
{
	public EntityMatch M;

	public readonly float b;

	[Button]
	public bool test;
	void Test()
	{
		GetType().GetField("b").SetValue(this, PRandom.Range(3f, 23409f));
		PDebug.Log(b);
	}

	void Update()
	{
	}
}