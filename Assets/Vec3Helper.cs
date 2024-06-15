using UnityEngine;
using Vec = UnityEngine.Vector3;

public static class Vec3Helper
{

	//public static V Lambda(this V v, Func<int, float, float> lambda)=> new(lambda(0, v.x), lambda(1, v.y), lambda(2, v.z));

	public static void Scale(this Vec v, Vec other)
	{
		v.x *= other.x;
		v.y *= other.y;
		v.z *= other.z;
	}
	public static void Scale(this Vec v, float x = 1f, float y = 1f, float z = 1f)
	{
		v.x *= x;
		v.y *= y;
		v.z *= z;
	}
	public static void Scale(this Vec v, float all)
		=> v *= all;
	public static Vec Scaled(this Vec v, Vec other)
		=> new(v.x * other.x, v.y * other.y, v.z * other.z);
	public static Vec Scaled(this Vec v, float x = 1f, float y = 1f, float z = 1f)
		=> new(v.x * x, v.y * y, v.z * z);
	public static Vec Scaled(this Vec v, float all)
		=> new(v.x * all, v.y * all, v.z * all);

	public static void Divide(this Vec v, Vec other)
	{
		v.x /= other.x;
		v.y /= other.y;
		v.z /= other.z;
	}
	public static void Divide(this Vec v, float x = 1f, float y = 1f, float z = 1f)
	{
		v.x /= x;
		v.y /= y;
		v.z /= z;
	}
	public static void Divide(this Vec v, float all)
		=> v /= all;
	public static Vec Divided(this Vec v, Vec other)
		=> new(v.x / other.x, v.y / other.y, v.z / other.z);
	public static Vec Divided(this Vec v, float x = 1f, float y = 1f, float z = 1f)
		=> new(v.x / x, v.y / y, v.z / z);
	public static Vec Divided(this Vec v, float all)
		=> new(v.x / all, v.y / all, v.z / all);

	public static Vec XY(this Vec v) => new(v.x, v.y, 0f);
	public static Vec XZ(this Vec v) => new(v.x, 0f, v.z);
	public static Vec YZ(this Vec v) => new(0f, v.y, v.z);

	public static void Squash(this Vec v, Vec direction)
	{
		v.x *= (1 - direction.normalized.x);
		v.y *= (1 - direction.normalized.y);
		v.z *= (1 - direction.normalized.z);
	}
	public static Vec Squashed(this Vec v, Vec direction)
		=> new (v.x * (1-direction.normalized.x), v.y * (1 - direction.normalized.y), v.z * (1 - direction.normalized.z));

	public static Vec ToXZ(this Vector2 v) => new(v.x, 0, v.y);
	public static Vector2 ZtoY(this Vec v) => new(v.x, v.z);
	public static Vec To2(this Vector2 v) => new(v.x, v.y, 0);
	public static Vector2 To3(this Vec v) => new(v.x, v.y);

	public static void Swizzle(this Vec v)
	{
		float hold = v.y;
		v.y = v.z;
		v.z = hold;
	}
	public static Vec Swizzled(this Vec v)
		=> new(v.x, v.z, v.y);

	public static void Rotate(this Vec v, float amount, Vec axis)
		=> v = Quaternion.AngleAxis(amount, axis) * v;
	public static Vec Rotated(this Vec v, float amount, Vec axis)
		=> Quaternion.AngleAxis(amount, axis) * v;
	public static void Rotate(this Vec v, Vec eularAngle)
		=> v = Quaternion.Euler(eularAngle) * v;
	public static Vec Rotated(this Vec v, Vec eularAngle)
		=> Quaternion.Euler(eularAngle) * v;

	public static void RotateTo(this Vec v, Vec towards)
		=> v = Quaternion.FromToRotation(v, towards) * v;
	public static void RotateTo(this Vec v, Vec towards, Vec reference)
		=> v = Quaternion.FromToRotation(reference, towards) * v;
	public static Vec RotatedTo(this Vec v, Vec towards)
		=> Quaternion.FromToRotation(v, towards) * v;
	public static Vec RotatedTo(this Vec v, Vec towards, Vec reference)
		=> Quaternion.FromToRotation(reference, towards) * v;

	public static Vec EularRotation(this Vec v)
		=> Quaternion.LookRotation(v.normalized).eulerAngles;
	public static Vec EularRotation(this Vec v, Vec up)
		=> Quaternion.LookRotation(Quaternion.FromToRotation(v.normalized, up) * v).eulerAngles;

	public static Vec rightTurn(this Vec v) => Quaternion.Euler(Eular.rightTurn) * v;
	public static Vec leftTurn(this Vec v) => Quaternion.Euler(Eular.leftTurn) * v;
	public static Vec upTurn(this Vec v) => Quaternion.Euler(Eular.upTurn) * v;
	public static Vec downTurn(this Vec v) => Quaternion.Euler(Eular.downTurn) * v;
	public static Vec aroundTurn(this Vec v) => Quaternion.Euler(Eular.aroundTurn) * v;

	public static Vec Randomize(this Vec v)
	{
		v.x.Random(-1, 1);
		v.y.Random(-1, 1);
		v.z.Random(-1, 1);
		return v;
	}
	public static Vec Randomize(this Vec v, float min, float max)
	{
		v.x.Random(min, max);
		v.y.Random(min, max);
		v.z.Random(min, max);
		return v;
	}
	public static Vec Randomize(this Vec v, Vec min, Vec max)
	{
		v.x.Random(min.x, max.x);
		v.y.Random(min.y, max.y);
		v.z.Random(min.z, max.z);
		return v;
	}
	public static Vec Randomize(this Vec v, Vec max)
	{
		v.x.Random(0, max.x);
		v.y.Random(0, max.y);
		v.z.Random(0, max.z);
		return v;
	}
	public static Vec Randomize(this Vec v, float x, float y, float z)
	{
		v.x.Random(0, x);
		v.y.Random(0, y);
		v.z.Random(0, z);
		return v;
	}


	public static Vec DirToRot(this Vec value) => Quaternion.LookRotation(value.normalized).eulerAngles;
	public static Vec RotToDir(this Vec value) => Quaternion.Euler(value) * Vec.forward;




}

public static class Direction
{
	public static Vec up => Vec.up;
	public static Vec down => Vec.down;
	public static Vec left => Vec.left;
	public static Vec right => Vec.right;
	public static Vec forward => Vec.forward;
	public static Vec front => Vec.forward;
	public static Vec back => Vec.back;

	public static Vec one => Vec.one;
	public static Vec zero => Vec.zero;

	public static Vec two = Vec.one * 2;
	public static Vec five = Vec.one * 5;
	public static Vec ten = Vec.one * 10;
	public static Vec nOne = Vec.one * -1;

	public static Vec inf = Vec.one * float.PositiveInfinity;
	public static Vec nInf = Vec.one * float.NegativeInfinity;

	public static Vec upRight = Vec.right + Vec.up;
	public static Vec frontRight = Vec.right + Vec.forward;
	public static Vec downRight = Vec.right + Vec.down;
	public static Vec backRight = Vec.right + Vec.back;

	public static Vec upLeft = Vec.left + Vec.up;
	public static Vec frontLeft = Vec.left + Vec.forward;
	public static Vec downLeft = Vec.left + Vec.down;
	public static Vec backLeft = Vec.left + Vec.back;

	public static Vec upFront = Vec.up + Vec.forward;
	public static Vec upBack = Vec.up + Vec.back;
	public static Vec downFront = Vec.down + Vec.forward;
	public static Vec downBack = Vec.down + Vec.back;

}

public static class Eular
{

	public static Vec rightTurn = new(0, 90, 0);
	public static Vec leftTurn = new(0, -90, 0);
	public static Vec aroundTurn = new(0, 180, 0);
	public static Vec upTurn = new(90, 0, 0);
	public static Vec downTurn = new(-90, 0, 0);

	public const float FullCircle = 360;
	public const float HalfCircle = 180;
	public const float QuarterCircle = 90;

	public static void EularClamp(this Vec v, bool mirrored = false)
	{
		v.x = (!mirrored) ? v.x % FullCircle : (((v.x + HalfCircle) % FullCircle) - HalfCircle);
		v.y = (!mirrored) ? v.y % FullCircle : (((v.y + HalfCircle) % FullCircle) - HalfCircle);
		v.z = (!mirrored) ? v.z % FullCircle : (((v.z + HalfCircle) % FullCircle) - HalfCircle);
	}
	public static Vec EularClamped(this Vec v, bool mirrored = false)
	{
		return new(
			(!mirrored) ? v.x % FullCircle : (((v.x + HalfCircle) % FullCircle) - HalfCircle),
			(!mirrored) ? v.y % FullCircle : (((v.y + HalfCircle) % FullCircle) - HalfCircle),
			(!mirrored) ? v.z % FullCircle : (((v.z + HalfCircle) % FullCircle) - HalfCircle)
			);
	}
	public static void EularClamp(this float v, bool mirrored = false) => v = (!mirrored) ? v % FullCircle : (((v + HalfCircle) % FullCircle) - HalfCircle);
	public static float EularClamped(this float v, bool mirrored = false) => (!mirrored) ? v % FullCircle : (((v + HalfCircle) % FullCircle) - HalfCircle);
}
