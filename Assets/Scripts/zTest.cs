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
using Pseudo2;

public class zTest : PMonoBehaviour
{
	[Button]
	public bool test;
	void Test()
	{

	}
}

[Serializable, ComponentCategory("Motion")]
public class PositionComponent : ComponentBase
{
	public PositionComponent() { }

	public sbyte SBYTE;
	public byte BYTE;
	public short SHORT;
	public ushort USHORT;
	public int INT;
	public uint UINT;
	public long LONG;
	public ulong ULONG;
	public float FLOAT;
	public double DOUBLE;
	public decimal DECIMAL;
	public Vector2 VECTOR2;
	public Vector3 VECTOR3;
	public Vector4 VECTOR4;
	public Quaternion QUATERNION;
	public Color COLOR;
	public Rect RECT;
	public Bounds BOUNDS;
	public string STRING;
	public char CHAR;
	public AnimationCurve ANIMATIONCURVE;
	public CustomData DATA;
	public GameObject GAMEOBJECT;
	public AudioClipLoadType ENUM;
}

[Serializable]
public class SomeComponent1 : ComponentBase { }
[Serializable, RequireComponent(typeof(SomeComponent1))]
public class SomeComponent2 : ComponentBase
{
	public float F;
	public UnityEngine.Object Obj;
}
[Serializable, ComponentCategory("Special")]
public class SomeComponent3 : ComponentBase
{
	public Vector3 V;
}

public sealed class ComponentCategoryAttribute : Attribute
{
	public readonly string Category;

	public ComponentCategoryAttribute(string category)
	{
		Category = category;
	}
}

[Serializable]
public class CustomData
{
	public float SomeThin;
	public string Str;
}