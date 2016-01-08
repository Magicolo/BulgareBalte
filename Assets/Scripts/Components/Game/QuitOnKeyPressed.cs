using UnityEngine;
using System.Collections.Generic;
using Pseudo;
using System;

[Serializable, ComponentCategory("Game")]
public class QuitOnKeyPressed : ComponentBase, IUpdateable
{
	public float UpdateRate { get { return 0; } }

	public KeyCode[] QuitKeys;

	public void Update()
	{
		for (int i = 0; i < QuitKeys.Length; i++)
			if (Input.GetKeyDown(QuitKeys[i]))
				Application.Quit();
	}
}

