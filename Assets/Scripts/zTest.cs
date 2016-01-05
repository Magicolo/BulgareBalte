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
using System.Reflection;
using UnityEngine.Events;
using Pseudo.Internal;
using System.IO;
using UnityEngine.SceneManagement;
using System.Text;

public class zTest : PMonoBehaviour
{
	[Button]
	public bool test;
	void Test()
	{
		PDebug.Log(JsonUtility.ToJson(new Damageable()));
	}
}