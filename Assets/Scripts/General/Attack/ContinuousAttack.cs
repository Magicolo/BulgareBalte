﻿using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Pseudo;

public class ContinuousAttack : AttackBase
{
	protected override bool ShouldAttack()
	{
		return true;
	}
}
