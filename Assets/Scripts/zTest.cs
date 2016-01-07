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
	public PEntity Entity2;

	[Button]
	public bool test;
	void Test()
	{
		StartCoroutine(RecycleAfterDelay(PrefabPoolManager.Create(Entity), 2f));
		StartCoroutine(RecycleAfterDelay(PrefabPoolManager.Create(Entity2), 2f));
	}

	IEnumerator RecycleAfterDelay(object toRecycle, float delay)
	{
		for (float counter = 0; counter < delay; counter += Time.deltaTime)
			yield return null;

		PrefabPoolManager.Recycle(toRecycle);
	}
}