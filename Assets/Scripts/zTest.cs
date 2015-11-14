using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Pseudo;
using System.Reflection;
using System.Threading;
using System.Runtime.InteropServices;
using System.ComponentModel;
using Pseudo.Internal;

public class zTest : PMonoBehaviour
{
	public zTest2 Reference;

	Dummy dummy;
	ReflectionPool<zTest2> reflectionPool;
	BehaviourPool<zTest2> behaviourPool;
	const int iterations = 1000;

	[Button]
	public bool test;
	public void Test()
	{
		FieldInfo field = GetType().GetField("test", ReflectionExtensions.AllFlags);
		PDebug.LogTest("Get", () => field.GetValue(this), 1000000);
		PDebug.LogTest("Set", () => field.SetValue(this, false), 1000000);

		//MethodInfo method = GetType().GetMethod("Empty", ReflectionExtensions.AllFlags);
		//PDebug.LogTest("Reflection invoke", () => method.Invoke(this, null), 1000);
		//PDebug.LogTest("Normal invoke", () => Empty(), 1000);
		//PDebug.Log(typeof(zTest2).GetFields(ReflectionExtensions.AllFlags).Convert(field => field.Name + " " + field.FieldType.Name));
	}

	void Empty()
	{

	}

	void Start()
	{
		behaviourPool = new BehaviourPool<zTest2>(Reference);
		reflectionPool = new ReflectionPool<zTest2>(Reference);
	}

	void Update()
	{
		if (test)
		{
			for (int i = 0; i < iterations; i++)
			{
				ReflectionTest();
				//BehaviourPoolTest();
				//UnityTest();
			}
		}
	}

	void ReflectionTest()
	{
		reflectionPool.Recycle(reflectionPool.Create());
	}

	void BehaviourPoolTest()
	{
		behaviourPool.Recycle(behaviourPool.CreateCopy(Reference));
	}

	void UnityTest()
	{
		Instantiate(Reference).gameObject.Destroy();
	}
}

public class Dummy
{
	public Vector3 Vector;
}

public class ReflectionPool<T> where T : PMonoBehaviour
{
	readonly T reference;
	readonly Queue<T> pool = new Queue<T>();
	readonly Queue<T> toInitialize = new Queue<T>();
	readonly FieldInfo[] fields;
	readonly object[] defaultValues;

	public ReflectionPool(T reference)
	{
		this.reference = reference;

		fields = reference.GetType().GetFields();
		defaultValues = new object[fields.Length];

		for (int i = 0; i < fields.Length; i++)
			defaultValues[i] = fields[i].GetValue(reference);

		Thread initializeThread = new Thread(InitializeLoop);
		initializeThread.Start();
	}

	void InitializeLoop()
	{
		while (true)
		{
			if (toInitialize.Count > 0)
			{
				lock (toInitialize)
				{
					T item = toInitialize.Dequeue();

					for (int i = 0; i < fields.Length; i++)
						fields[i].SetValue(item, defaultValues[i]);

					lock (pool) { pool.Enqueue(item); }
				}
			}

			Thread.Sleep(10);
		}
	}

	public T Create()
	{
		T item;

		if (pool.Count > 0)
			item = pool.Dequeue();
		else
			item = UnityEngine.Object.Instantiate(reference);

		item.CachedGameObject.SetActive(true);

		return item;
	}

	public void Recycle(T item)
	{
		if (item == null)
			return;

		item.CachedGameObject.SetActive(false);
		toInitialize.Enqueue(item);
	}
}