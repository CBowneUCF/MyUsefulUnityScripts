﻿using System;
using System.Collections;
using UnityEngine;

/// <summary>
/// A code system similar to a Singleton but is not an Beheavior-Object in any scene. <br/>
/// The Staticon will be initialized the first time it is referenced by any script, and exist until the program ends.
/// </summary>
/// <typeparam name="T">The Behavior's Type</typeparam>
public abstract class Staticon<T>
{
	public static bool initialized;
	private static T _instance;
	public T Get()
	{ if (!initialized) Initialize(); return _instance; }
	public T I => Get();

	public void Initialize()
	{
		if(initialized) return;
		_instance = Activator.CreateInstance<T>();
		Awake();
		initialized = true;
	}

	public abstract void Awake();
}