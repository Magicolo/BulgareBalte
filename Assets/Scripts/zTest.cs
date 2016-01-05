using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Pseudo;
using UnityEditor;
using Pseudo.Internal.Input;

public class zTest : PMonoBehaviour
{
	public MonoScript Script;

	[Button]
	public bool test;
	void Test()
	{
		PDebug.Log(2 / 3);
	}

	void Update()
	{
		PDebug.Log(InputUtility.GetPressedKeys());
	}
}