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
	public enum TestEnum : byte
	{
		A = 6,
		B = 53,
		C_D = 2,
		C_E = 8,
	}

	[EnumFlags(typeof(TestEnum))]
	public ByteFlag Flag1;
	public ByteFlag Flag2;
	public EntityMatch Groups;

	[Button]
	public bool test;
	void Test()
	{

	}

	void Update()
	{

	}
}