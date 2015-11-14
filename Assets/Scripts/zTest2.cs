using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Pseudo;

[Copy]
public class zTest2 : PMonoBehaviour, ICopyable<zTest2>
{
	public bool test1;
	public Vector2 test2;
	public Rect test3;
	public zTest test4;
	public Transform test5;
	public GameObject test6;
	public int test7;
	public float test8;
	public double test9;

	public void Copy(zTest2 reference)
	{
		test1 = reference.test1;
		test2 = reference.test2;
		test3 = reference.test3;
		test4 = reference.test4;
		test5 = reference.test5;
		test6 = reference.test6;
		test7 = reference.test7;
		test8 = reference.test8;
		test9 = reference.test9;
	}
}
