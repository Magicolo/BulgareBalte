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
	public SpriteRenderer Renderer;

	[Button]
	public bool test;
	void Test()
	{
		Renderer.sortingLayerName = "Boba Fett";

		PDebug.Log(Renderer.sortingLayerName);
	}
}