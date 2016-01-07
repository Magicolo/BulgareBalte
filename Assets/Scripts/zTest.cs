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
	public PEntity Entity;

	const int iterations = 1000;

	[Button]
	public bool test;
	void Test()
	{
		Entity.SendMessage(EntityMessages.OnDie);
	}
}