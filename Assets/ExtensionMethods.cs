using System.Collections;
using UnityEngine;


public static class ExtensionMethods
{
	public static bool Toggle(this ref bool boolean) => boolean = !boolean;
	public static bool IsTrue(this bool boolean) => boolean == true;
	public static bool IsFalse(this bool boolean) => boolean == false;
	public static bool True(this ref bool boolean) => boolean = true;
	public static bool False(this ref bool boolean) => boolean = false;

	public static int Int(this bool boolean) => boolean ? 1 : 0;
	public static bool Bool(this int integral) => integral > 0;

	public static Color SetRed(this ref Color color, float set) => color =			new Color(set,              color.g,           color.b,          color.a);
	public static Color ChangeRed(this ref Color color, float change) => color =	new Color(color.r + change, color.g,           color.b,          color.a);
	public static Color SetBlue(this ref Color color, float set) => color =			new Color(color.r,          set,			   color.b,          color.a);
	public static Color ChangeBlue(this ref Color color, float change) => color =	new Color(color.r,          color.g + change,  color.b,          color.a);
	public static Color SetGreen(this ref Color color, float set) => color =		new Color(color.r,          color.g,		   set,              color.a);
	public static Color ChangeGreen(this ref Color color, float change) => color =	new Color(color.r,          color.g,           color.b + change, color.a);
	public static Color SetAlpha(this ref Color color, float set) => color =		new Color(color.r,          color.g,           color.b,          set);
	public static Color ChangeAlpha(this ref Color color, float change) => color =  new Color(color.r,          color.g,           color.b,          color.a + change);


}

public static class EasierMathExtensions
{
	public static float P(this float F) => Mathf.Pow(F, 2);
	public static float P(this float F, int power) => Mathf.Pow(F, power);
	public static float SQRT(this float F) => Mathf.Sqrt(F);
	public static float Sin(this float F) => Mathf.Sin(F);
	public static float Cos(this float F) => Mathf.Cos(F);
	public static float Tan(this float F) => Mathf.Tan(F);
	public static float ASin(this float F) => Mathf.Asin(F);
	public static float ACos(this float F) => Mathf.Acos(F);
	public static float ATan(this float F) => Mathf.Atan(F);

	public static float Clamp(this float value, float min, float max) => (value < min) ? min : (value > max) ? max : value;
	public static float Min(this float value, float min) => (value < min) ? min : value;
	public static float Max(this float value, float max) => (value > max) ? max : value;

	public static int Int(this float value) => (int)value;
	public static float Float(this int value) => (float)value;
	public static int Floor(this float value) => Mathf.FloorToInt(value);
	public static int Ceil(this float value) => Mathf.CeilToInt(value);

	public static int Sign(this float value) => (int)Mathf.Sign(value);
	public static float Abs(this float value) => Mathf.Abs(value);
	public static float Repeat(this float value, float length) => Mathf.Repeat(value, length);

	public static float Random(this float value, float min, float max) => UnityEngine.Random.Range(min, max);
	public static float Random(this float value, Vector2 input) => UnityEngine.Random.Range(input.x, input.y);
	public static int Random(this int value, int min, int max) => UnityEngine.Random.Range(min, max);
	public static int Random(this int value, Vector2Int input) => UnityEngine.Random.Range(input.x, input.y);

}

public static class MonoBehaviorHelpers
{
	public static void LateAwake(this MonoBehaviour m, BasicDelegate result) => m.StartCoroutine(LateWakeENUM(result));
	
	static IEnumerator LateWakeENUM(BasicDelegate result)
	{
		yield return WaitFor.EndOfFrame();
		result();
	}

	public static bool Unloading(this MonoBehaviour m) => m.gameObject.scene.isLoaded;

	public static void SafeDestroyers(this MonoBehaviour m, BasicDelegate SafeDestroy, BasicDelegate UnloadDestroy)
	{
		if (!m.gameObject.scene.isLoaded) SafeDestroy();
		else UnloadDestroy();
	}






}

public delegate void BasicDelegate();