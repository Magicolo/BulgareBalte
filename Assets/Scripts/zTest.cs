using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Pseudo;
using UnityEditor;

public class zTest : PMonoBehaviour
{
	public MonoScript Script;

	[Button]
	public bool test;
	void Test()
	{
		var scripts = MonoImporter.GetAllRuntimeMonoScripts();

		for (int i = 0; i < scripts.Length; i++)
		{
			PDebug.Log(scripts[i].GetClass());
		}
	}
}