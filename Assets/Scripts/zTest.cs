using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Pseudo;
using System.Threading;
using Pseudo.Internal;
using Pseudo.Internal.Pool;
using Pseudo.Internal.Entity;

[Serializable]
public class zTest : PMonoBehaviour
{
	public PEntity Entity;
	public EntityMatch Match;

	[Button]
	public bool test;
	void Test()
	{
		var matcher = new EntityAllGroupMatcher();
		PDebug.Log(matcher.Matches(Entity.Group, Match.Group));
	}
}