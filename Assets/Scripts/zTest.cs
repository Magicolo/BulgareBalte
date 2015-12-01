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

[Serializable]
public class zTest : PMonoBehaviour
{
	public BitFlag Flag1;
	public BitFlag Flag2;

	[Button]
	public bool test;
	void Test()
	{

	}

	void Update()
	{

	}
}

[Serializable]
public struct BitFlag
{
	[SerializeField]
	ulong flag1;
	[SerializeField]
	ulong flag2;
	[SerializeField]
	ulong flag3;
	[SerializeField]
	ulong flag4;

	public static BitFlag Empty
	{
		get { return new BitFlag(0uL, 0uL, 0uL, 0uL); }
	}

	public static BitFlag Max
	{
		get { return new BitFlag(ulong.MaxValue, ulong.MaxValue, ulong.MaxValue, ulong.MaxValue); }
	}

	public BitFlag(byte[] indices) : this()
	{
		for (int i = 0; i < indices.Length; i++)
			AddBit(indices[i]);
	}

	public BitFlag(bool[] values) : this()
	{
		int count = Math.Min(values.Length, 255);

		for (byte i = 0; i < count; i++)
			Set(i, values[i]);
	}

	BitFlag(ulong flag1, ulong flag2, ulong flag3, ulong flag4)
	{
		this.flag1 = flag1;
		this.flag2 = flag2;
		this.flag3 = flag3;
		this.flag4 = flag4;
	}

	public bool Get(byte index)
	{
		if (index < 64)
			return (flag1 & (1uL << index)) != 0;
		else if (index < 128)
			return (flag2 & (1uL << (index - 64))) != 0;
		else if (index < 192)
			return (flag3 & (1uL << (index - 128))) != 0;
		else
			return (flag4 & (1uL << (index - 192))) != 0;
	}

	public void Set(byte index, bool value)
	{
		if (value)
			AddBit(index);
		else
			RemoveBit(index);
	}

	public byte[] ToIndices()
	{
		List<byte> indices = new List<byte>();

		for (byte i = 0; i < 255; i++)
		{
			if (Get(i))
				indices.Add(i);
		}

		return indices.ToArray();
	}

	public bool[] ToValues()
	{
		bool[] values = new bool[255];

		for (byte i = 0; i < 255; i++)
			values[i] = Get(i);

		return values;
	}

	void AddBit(byte value)
	{
		if (value < 64)
			flag1 |= 1uL << value;
		else if (value < 128)
			flag2 |= 1uL << (value - 64);
		else if (value < 192)
			flag3 |= 1uL << (value - 128);
		else
			flag4 |= 1uL << (value - 192);
	}

	void RemoveBit(byte value)
	{
		if (value < 64)
			flag1 &= ~(1uL << value);
		else if (value < 128)
			flag2 &= ~(1uL << (value - 64));
		else if (value < 192)
			flag3 &= ~(1uL << (value - 128));
		else
			flag4 &= ~(1uL << (value - 192));
	}

	public override int GetHashCode()
	{
		return base.GetHashCode();
	}

	public override bool Equals(object obj)
	{
		if (!(obj is BitFlag))
			return false;

		BitFlag flag = (BitFlag)obj;

		return flag1.Equals(flag.flag1) && flag2.Equals(flag.flag2) && flag3.Equals(flag.flag3) && flag4.Equals(flag.flag4);
	}

	public override string ToString()
	{
		var log = new System.Text.StringBuilder();
		log.Append(GetType().Name + "(");
		bool first = true;

		for (byte i = 0; i < 255; i++)
		{
			if (Get(i))
			{
				if (first)
					first = false;
				else
					log.Append(", ");

				log.Append(i);
			}
		}

		log.Append(")");

		return log.ToString();
	}

	public static BitFlag operator ~(BitFlag a)
	{
		return new BitFlag(~a.flag1, ~a.flag2, ~a.flag3, ~a.flag4);
	}

	public static BitFlag operator |(BitFlag a, BitFlag b)
	{
		return new BitFlag(a.flag1 | b.flag1, a.flag2 | b.flag2, a.flag3 | b.flag3, a.flag4 | b.flag4);
	}

	public static BitFlag operator &(BitFlag a, BitFlag b)
	{
		return new BitFlag(a.flag1 & b.flag1, a.flag2 & b.flag2, a.flag3 & b.flag3, a.flag4 & b.flag4);
	}

	public static BitFlag operator ^(BitFlag a, BitFlag b)
	{
		return new BitFlag(a.flag1 ^ b.flag1, a.flag2 ^ b.flag2, a.flag3 ^ b.flag3, a.flag4 ^ b.flag4);
	}

	public static bool operator ==(BitFlag a, BitFlag b)
	{
		return a.flag1 == b.flag1 && a.flag2 == b.flag2 && a.flag3 == b.flag3 && a.flag4 == b.flag4;
	}

	public static bool operator !=(BitFlag a, BitFlag b)
	{
		return a.flag1 != b.flag1 || a.flag2 != b.flag2 || a.flag3 != b.flag3 || a.flag4 != b.flag4;
	}
}