using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.SceneManagement;
using Pseudo;
using Pseudo.Internal.Input;

public class zTest : PMonoBehaviour
{
	public UnityEngine.Object Scene;
	public TextAsset Scene1;

	[Button]
	public bool test;
	void Test()
	{
	}

	void Update()
	{
		PDebug.Log(InputUtility.GetPressedKeys());
	}
}