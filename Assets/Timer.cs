using System;
using System.Collections;
using UnityEngine;














public struct Timer
{
	public Timer(float length, Action<Timer> Callback, bool unscaled = false)
	{
		currentTime = 0;
		this.length = length;
		this.Callback = Callback;
		this.unscaled = unscaled;
	}
	
	public float currentTime;
	public float length;
	public bool unscaled;
	public Action<Timer> Callback;

	void Update()
	{
		if (Time.timeScale == 0 && !unscaled) return;
		currentTime += (!unscaled)? Time.deltaTime : Time.unscaledDeltaTime;
		if(currentTime > length)
		{
			currentTime -= length;
			Callback.Invoke(this);
		}
	}








}












