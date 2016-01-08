using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.SceneManagement;
using Pseudo;
using Pseudo.Internal.Input;
using Pseudo.Internal.Entity;
using Pseudo.Internal;

public class zTest : PMonoBehaviour
{
	readonly Dictionary<Type, ComponentGroup> groupDict = new Dictionary<Type, ComponentGroup>();
	ComponentGroup[] groups;

	const int iterations = 100000;

	[Button]
	public bool test;
	void Test()
	{
		GetGroupDict(typeof(SeekerMotion));
		GetGroupDict(typeof(SinusMotionModifier));
		GetGroupDict(typeof(InputMotion));
		GetGroupDict(typeof(LifeTime));
		GetGroupDict(typeof(MotionBase));
		GetGroup<SeekerMotion>();
		GetGroup<SinusMotionModifier>();
		GetGroup<InputMotion>();
		GetGroup<LifeTime>();
		GetGroup<MotionBase>();

		foreach (var type in TypeExtensions.GetAssignableTypes(typeof(IComponent)))
			GetGroupDict(type);

		var method = GetType().GetMethod("GetGroup", ReflectionExtensions.AllFlags);
		foreach (var type in TypeExtensions.GetAssignableTypes(typeof(IComponent)))
		{
			var generic = method.MakeGenericMethod(type);
			generic.Invoke(this, null);
		}

		PDebug.LogTest("Dictionary", () => GetGroupDict(typeof(BurstWeaponAttack)), iterations);
		PDebug.LogTest("Array", () => GetGroup<BurstWeaponAttack>(), iterations);
		PDebug.Log(groupDict.Count, groups.Length);
	}

	ComponentGroup GetGroupDict(Type type)
	{
		ComponentGroup group;

		if (!groupDict.TryGetValue(type, out group))
		{
			group = new ComponentGroup(type);
			groupDict[type] = group;
		}

		return group;
	}

	ComponentGroup GetGroup<T>()
	{
		int index = TypeIndexHolder<T>.Index;
		ComponentGroup group;

		if (groups == null)
		{
			groups = new ComponentGroup[Mathf.NextPowerOfTwo(index + 1)];
			group = new ComponentGroup(typeof(T));
			groups[index] = group;
		}
		else if (groups.Length <= index)
		{
			Array.Resize(ref groups, Mathf.NextPowerOfTwo(index + 1));
			group = new ComponentGroup(typeof(T));
			groups[index] = group;
		}
		else
		{
			group = groups[index];

			if (group == null)
			{
				group = new ComponentGroup(typeof(T));
				groups[index] = group;
			}
		}

		return group;
	}
}

public static class Component2
{
	public static int TypeCount
	{
		get { return nextIndex; }
	}

	static int nextIndex;

	public static int GetNextIndex()
	{
		return nextIndex++;
	}
}

public static class TypeIndexHolder<T>
{
	public static readonly int Index = Component2.GetNextIndex();
}