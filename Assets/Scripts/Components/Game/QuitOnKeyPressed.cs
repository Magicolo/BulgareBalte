using UnityEngine;
using System.Collections.Generic;
using Pseudo;
using System;

[Serializable]
public class QuitOnKeyPressed : ComponentBase, IUpdateable
{
	public float UpdateRate { get { return 0; } }

	public KeyCode QuitKey;

	public void Update()
	{
		if (Input.GetKeyDown(QuitKey))
			Application.Quit();
	}
}

