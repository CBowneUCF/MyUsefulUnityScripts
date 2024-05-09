using System;
using TrigHelper;
using UnityEditor;
using UnityEngine;

namespace BetterVectors
{
	[Serializable]
	public struct Vector3
	{

		#region Basic Data

		public Vector3(float x, float y, float z)
		{
			this.x = x;
			this.y = y;
			this.z = z;
		}
		public Vector3(float one)
		{
			x = one;
			y = one;
			z = one;
		}

		public float x;
		public float y;
		public float z;

		public UnityEngine.Vector3 unity { get => this; set => this = value; }

		public Vector3 normalized => this / magnitude;
		public void Normalize() => this = normalized;

		public float magnitude => ((x * x) + (y * y) + (z * z)).SQRT();
		public float sqrMagnitude => (x * x) + (y * y) + (z * z);

		#endregion

		#region Operators

		public static implicit operator UnityEngine.Vector3(Vector3 @in)
		{
			UnityEngine.Vector3 result;
			result.x = @in.x;
			result.y = @in.y;
			result.z = @in.z;
			return result;
		}
		public static implicit operator Vector3(UnityEngine.Vector3 @in)
		{
			Vector3 result;
			result.x = @in.x;
			result.y = @in.y;
			result.z = @in.z;
			return result;
		}

		public static bool operator ==(Vector3 l, Vector3 r) => l == r;
		public static bool operator !=(Vector3 l, Vector3 r) => l != r;

		public static Vector3 operator +(Vector3 l, Vector3 r)
		{
			Vector3 result;
			result.x = l.x + r.x;
			result.y = l.y + r.y;
			result.z = l.z + r.z;
			return result;
		}
		public static Vector3 operator -(Vector3 l, Vector3 r)
		{
			Vector3 result;
			result.x = l.x - r.x;
			result.y = l.y - r.y;
			result.z = l.z - r.z;
			return result;
		}
		public static Vector3 operator *(Vector3 l, Vector3 r)
		{
			Vector3 result;
			result.x = l.x * r.x;
			result.y = l.y * r.y;
			result.z = l.z * r.z;
			return result;
		}
		public static Vector3 operator /(Vector3 l, Vector3 r)
		{
			Vector3 result;
			result.x = l.x / r.x;
			result.y = l.y / r.y;
			result.z = l.z / r.z;
			return result;
		}
		public static Vector3 operator +(Vector3 l, float r)
		{
			Vector3 result;
			result.x = l.x + r;
			result.y = l.y + r;
			result.z = l.z + r;
			return result;
		}
		public static Vector3 operator -(Vector3 l, float r)
		{
			Vector3 result;
			result.x = l.x - r;
			result.y = l.y - r;
			result.z = l.z - r;
			return result;
		}
		public static Vector3 operator *(Vector3 l, float r)
		{
			Vector3 result;
			result.x = l.x * r;
			result.y = l.y * r;
			result.z = l.z * r;
			return result;
		}
		public static Vector3 operator /(Vector3 l, float r)
		{
			Vector3 result;
			result.x = l.x / r;
			result.y = l.y / r;
			result.z = l.z / r;
			return result;
		}

		public static Vector3 operator -(Vector3 v)
		{
			Vector3 result;
			result.x = -v.x;
			result.y = -v.y;
			result.z = -v.z;
			return result;
		}
		public static Vector3 operator ++(Vector3 v)
		{
			Vector3 result;
			result.x = v.x + 1;
			result.y = v.y + 1;
			result.z = v.z + 1;
			return result;
		}
		public static Vector3 operator --(Vector3 v)
		{
			Vector3 result;
			result.x = v.x - 1;
			result.y = v.y - 1;
			result.z = v.z - 1;
			return result;
		}

		public override bool Equals(object obj) => obj is Vector3 vector && x == vector.x && y == vector.y && z == vector.z;
		public override int GetHashCode() => HashCode.Combine(x, y, z, unity);

		#endregion

		#region Directions

		public static Vector3 up = new(0, 1, 0);
		public static Vector3 down = new(0, -1, 0);
		public static Vector3 left = new(-1, 0, 0);
		public static Vector3 right = new(1, 0, 0);
		public static Vector3 front = new(0, 0, 1);
		public static Vector3 forwards = new(0, 0, 1);
		public static Vector3 back = new(0, 0, -1);

		public static Vector3 zero = new(0, 0, 0);
		public static Vector3 one = new(1, 1, 1);
		public static Vector3 two = new(2, 2, 2);
		public static Vector3 five = new(5, 5, 5);
		public static Vector3 ten = new(10, 10, 10);
		public static Vector3 nOne = new(-1, -1, -1);

		#region Combos

		public static Vector3 upRight = new(1, 1, 0);
		public static Vector3 frontRight = new(1, 0, 1);
		public static Vector3 downRight = new(1, -1, 0);
		public static Vector3 backRight = new(1, 0, -1);

		public static Vector3 upLeft = new(-1, 1, 0);
		public static Vector3 frontLeft = new(-1, 0, 1);
		public static Vector3 downLeft = new(-1, -1, 0);
		public static Vector3 backLeft = new(-1, 0, -1);

		public static Vector3 upFront = new(0, 1, 1);
		public static Vector3 upBack = new(0, 1, -1);
		public static Vector3 downFront = new(0, 1, -1);
		public static Vector3 downBack = new(0, -1, -1);

		#endregion

		public static Vector3 inf = new(float.PositiveInfinity, float.PositiveInfinity, float.PositiveInfinity);
		public static Vector3 nInf = new(float.NegativeInfinity, float.NegativeInfinity, float.NegativeInfinity);

		#endregion

		#region Squashing

		public Vector3 xz => new(x, 0, z);
		public Vector3 xy => new(x, y, 0);
		public Vector3 yz => new(0, y, z);

		public void Squash(Vector3 v) => this *= one - v.normalized;
		public Vector3 Squashed(Vector3 v) => this * (one - v.normalized);

		#endregion

		#region Rotation

		public Quaternion ToQuaternion() => Quaternion.LookRotation(((UnityEngine.Vector3)this).normalized);

		public void Rotate(float amount, Vector3 axis) => this = Quaternion.AngleAxis(amount, axis) * this;
		public Vector3 Rotated(float amount, Vector3 axis) => Quaternion.AngleAxis(amount, axis) * this;
		public void Rotate(Eular eularAngle) => this = Quaternion.Euler(eularAngle) * this;
		public Vector3 Rotated(Eular eularAngle) => Quaternion.Euler(eularAngle) * this;

		public void RotateTo(Vector3 towards) => this = Quaternion.FromToRotation(this, towards) * this;
		public void RotateTo(Vector3 towards, Vector3 reference) => this = Quaternion.FromToRotation(reference, towards) * this;
		public Vector3 RotatedTo(Vector3 towards) => Quaternion.FromToRotation(this, towards) * this;
		public Vector3 RotatedTo(Vector3 towards, Vector3 reference) => Quaternion.FromToRotation(reference, towards) * this;

		public Eular EularRotation() => Quaternion.LookRotation(normalized).eulerAngles;
		public Eular EularRotation(Vector3 up) => Quaternion.LookRotation(RotatedTo(normalized, up)).eulerAngles;

		public Vector3 rightTurn => Rotated(Eular.rightTurn);
		public Vector3 leftTurn => Rotated(Eular.leftTurn);
		public Vector3 upTurn => Rotated(Eular.upTurn);
		public Vector3 downTurn => Rotated(Eular.downTurn);
		public Vector3 aroundTurn => Rotated(Eular.aroundTurn);

		#endregion

		#region Randomization

		public static Vector3 Randomize()
		{
			Vector3 result = new();
			result.x.Random(-1, 1);
			result.y.Random(-1, 1);
			result.z.Random(-1, 1);
			return result;
		}
		public static Vector3 Randomize(float min, float max)
		{
			Vector3 result = new();
			result.x.Random(min, max);
			result.y.Random(min, max);
			result.z.Random(min, max);
			return result;
		}
		public static Vector3 Randomize(Vector3 min, Vector3 max)
		{
			Vector3 result = new();
			result.x.Random(min.x, max.x);
			result.y.Random(min.y, max.y);
			result.z.Random(min.z, max.z);
			return result;
		}
		public static Vector3 Randomize(Vector3 max)
		{
			Vector3 result = new();
			result.x.Random(0, max.x);
			result.y.Random(0, max.y);
			result.z.Random(0, max.z);
			return result;
		}
		public static Vector3 Randomize(float x, float y, float z)
		{
			Vector3 result = new();
			result.x.Random(0, x);
			result.y.Random(0, y);
			result.z.Random(0, z);
			return result;
		}

		#endregion

	}
	[Serializable]
	public struct Eular
	{
		public Eular(float x, float y, float z)
		{
			this.x = x;
			this.y = y;
			this.z = z;
		}

		public Degree x;
		public Degree y;
		public Degree z;

		public UnityEngine.Vector3 unity => (UnityEngine.Vector3)this;

		public static implicit operator UnityEngine.Vector3(Eular @in) => new(@in.x, @in.y, @in.z);
		public static implicit operator Eular(UnityEngine.Vector3 @in) => new(@in.x, @in.y, @in.z);
		public static explicit operator Eular(Vector3 @in) => new(@in.x, @in.y, @in.z);
		public static explicit operator Vector3(Eular @in) => new(@in.x, @in.y, @in.z);

		public static bool operator ==(Eular l, Eular r) => l == r;
		public static bool operator !=(Eular l, Eular r) => l != r;

		public static Eular operator +(Eular l, Eular r)
		{
			Eular result;
			result.x = l.x + r.x;
			result.y = l.y + r.y;
			result.z = l.z + r.z;
			return result;
		}
		public static Eular operator -(Eular l, Eular r)
		{
			Eular result;
			result.x = l.x - r.x;
			result.y = l.y - r.y;
			result.z = l.z - r.z;
			return result;
		}
		public static Eular operator *(Eular l, Eular r)
		{
			Eular result;
			result.x = l.x * r.x;
			result.y = l.y * r.y;
			result.z = l.z * r.z;
			return result;
		}
		public static Eular operator /(Eular l, Eular r)
		{
			Eular result;
			result.x = l.x / r.x;
			result.y = l.y / r.y;
			result.z = l.z / r.z;
			return result;
		}
		public static Eular operator +(Eular l, float r)
		{
			Eular result;
			result.x = l.x + r;
			result.y = l.y + r;
			result.z = l.z + r;
			return result;
		}
		public static Eular operator -(Eular l, float r)
		{
			Eular result;
			result.x = l.x - r;
			result.y = l.y - r;
			result.z = l.z - r;
			return result;
		}
		public static Eular operator *(Eular l, float r)
		{
			Eular result;
			result.x = l.x * r;
			result.y = l.y * r;
			result.z = l.z * r;
			return result;
		}
		public static Eular operator /(Eular l, float r)
		{
			Eular result;
			result.x = l.x / r;
			result.y = l.y / r;
			result.z = l.z / r;
			return result;
		}

		public static Eular operator -(Eular @in)
		{
			Eular result;
			result.x = -@in.x;
			result.y = -@in.y;
			result.z = -@in.z;
			return result;
		}
		public static Eular operator ++(Eular @in)
		{
			Eular result;
			result.x = @in.x + 1;
			result.y = @in.y + 1;
			result.z = @in.z + 1;
			return result;
		}
		public static Eular operator --(Eular @in)
		{
			Eular result;
			result.x = @in.x - 1;
			result.y = @in.y - 1;
			result.z = @in.z - 1;
			return result;
		}

		public override bool Equals(object obj) => obj is Eular eular && x == eular.x && y == eular.y && z == eular.z;
		public override int GetHashCode() => HashCode.Combine(x, y, z);

		public Eular ClampToCircle()
		{
			Eular result;
			result.x = x.ClampToCircle();
			result.y = y.ClampToCircle();
			result.z = z.ClampToCircle();
			return result;
		}
		public Eular ClampToCircleMirrored()
		{
			Eular result;
			result.x = x.ClampToCircleMirrored();
			result.y = y.ClampToCircleMirrored();
			result.z = z.ClampToCircleMirrored();
			return result;
		}
		public Eular ClampToHalfCircleMirrored()
		{
			Eular result;
			result.x = x.ClampToHalfCircleMirrored();
			result.y = y.ClampToHalfCircleMirrored();
			result.z = z.ClampToHalfCircleMirrored();
			return result;
		}
		public void ClampedToCircle()
		{
			x = x.ClampToCircle();
			y = y.ClampToCircle();
			z = z.ClampToCircle();
		}
		public void ClampedToCircleMirrored()
		{
			x = x.ClampToCircleMirrored();
			y = y.ClampToCircleMirrored();
			z = z.ClampToCircleMirrored();
		}
		public void ClampedToHalfCircleMirrored()
		{
			x = x.ClampToHalfCircleMirrored();
			y = y.ClampToHalfCircleMirrored();
			z = z.ClampToHalfCircleMirrored();
		}

		public static Eular rightTurn = new(0, 90, 0);
		public static Eular leftTurn = new(0, -90, 0);
		public static Eular aroundTurn = new(0, 180, 0);
		public static Eular upTurn = new(90, 0, 0);
		public static Eular downTurn = new(-90, 0, 0);

	}
	[Serializable]
	public struct Scale
	{
		public Scale(float x, float y, float z)
		{
			this.x = x;
			this.y = y;
			this.z = z;
		}
		public Scale(float @in)
		{
			x = @in;
			y = @in;
			z = @in;
		}

		public float x;
		public float y;
		public float z;

		public UnityEngine.Vector3 unity => (UnityEngine.Vector3)this;

		public static implicit operator UnityEngine.Vector3(Scale @in) => new(@in.x, @in.y, @in.z);
		public static implicit operator Scale(UnityEngine.Vector3 @in) => new(@in.x, @in.y, @in.z);
		public static explicit operator Scale(Vector3 @in) => new(@in.x, @in.y, @in.z);
		public static explicit operator Vector3(Scale @in) => new(@in.x, @in.y, @in.z);

		public static bool operator ==(Scale l, Scale r) => l == r;
		public static bool operator !=(Scale l, Scale r) => l != r;

		public static Scale operator +(Scale l, Scale r)
		{
			Scale result;
			result.x = l.x + r.x;
			result.y = l.y + r.y;
			result.z = l.z + r.z;
			return result;
		}
		public static Scale operator -(Scale l, Scale r)
		{
			Scale result;
			result.x = l.x - r.x;
			result.y = l.y - r.y;
			result.z = l.z - r.z;
			return result;
		}
		public static Scale operator *(Scale l, Scale r)
		{
			Scale result;
			result.x = l.x * r.x;
			result.y = l.y * r.y;
			result.z = l.z * r.z;
			return result;
		}
		public static Scale operator /(Scale l, Scale r)
		{
			Scale result;
			result.x = l.x / r.x;
			result.y = l.y / r.y;
			result.z = l.z / r.z;
			return result;
		}
		public static Scale operator +(Scale l, float r)
		{
			Scale result;
			result.x = l.x + r;
			result.y = l.y + r;
			result.z = l.z + r;
			return result;
		}
		public static Scale operator -(Scale l, float r)
		{
			Scale result;
			result.x = l.x - r;
			result.y = l.y - r;
			result.z = l.z - r;
			return result;
		}
		public static Scale operator *(Scale l, float r)
		{
			Scale result;
			result.x = l.x * r;
			result.y = l.y * r;
			result.z = l.z * r;
			return result;
		}
		public static Scale operator /(Scale l, float r)
		{
			Scale result;
			result.x = l.x / r;
			result.y = l.y / r;
			result.z = l.z / r;
			return result;
		}

		public static Scale operator -(Scale @in)
		{
			Scale result;
			result.x = -@in.x;
			result.y = -@in.y;
			result.z = -@in.z;
			return result;
		}
		public static Scale operator ++(Scale @in)
		{
			Scale result;
			result.x = @in.x + 1;
			result.y = @in.y + 1;
			result.z = @in.z + 1;
			return result;
		}
		public static Scale operator --(Scale @in)
		{
			Scale result;
			result.x = @in.x - 1;
			result.y = @in.y - 1;
			result.z = @in.z - 1;
			return result;
		}

		public override bool Equals(object obj) => obj is Scale scale && x == scale.x && y == scale.y && z == scale.z;
		public override int GetHashCode() => HashCode.Combine(x, y, z);
	}

	[Serializable]
	public struct Vector2
	{
		#region Basic Data

		#endregion

		#region Operators

		#endregion

		#region Directions

		#endregion

		#region Squashing

		#endregion

		#region Rotation

		#endregion

		#region Randomization

		#endregion


	}

















	public static class Vector3ExtensionMethods
	{
		public static Vector3 Better(this UnityEngine.Vector3 v) => v;

		public static Vector3 Randomize(this ref Vector3 V)
		{
			Vector3 result = new();
			result.x.Random(-1, 1);
			result.y.Random(-1, 1);
			result.z.Random(-1, 1);
			V = result;
			return result;
		}
		public static Vector3 Randomize(this ref Vector3 V, float min, float max)
		{
			Vector3 result = new();
			result.x.Random(min, max);
			result.y.Random(min, max);
			result.z.Random(min, max);
			V = result;
			return result;
		}
		public static Vector3 Randomize(this ref Vector3 V, Vector3 min, Vector3 max)
		{
			Vector3 result = new();
			result.x.Random(min.x, max.x);
			result.y.Random(min.y, max.y);
			result.z.Random(min.z, max.z);
			V = result;
			return result;
		}
		public static Vector3 Randomize(this ref Vector3 V, Vector3 max)
		{
			Vector3 result = new();
			result.x.Random(0, max.x);
			result.y.Random(0, max.y);
			result.z.Random(0, max.z);
			V = result;
			return result;
		}
		public static Vector3 Randomize(this ref Vector3 V, float x, float y, float z)
		{
			Vector3 result = new();
			result.x.Random(0, x);
			result.y.Random(0, y);
			result.z.Random(0, z);
			V = result;
			return result;
		}


	}







	//Drawers

	[CustomPropertyDrawer(typeof(Vector3))]
	public class Vector3Drawer : PropertyDrawer
	{
		// Draw the property inside the given rect
		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
		{
			// Using BeginProperty / EndProperty on the parent property means that
			// prefab override logic works on the entire property.
			EditorGUI.BeginProperty(position, label, property);

			// Draw label
			position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

			// Don't make child fields be indented
			int indent = EditorGUI.indentLevel;
			EditorGUI.indentLevel = 0;

			float size = position.width;

			// Calculate rects
			Rect xRect = new(position.x, position.y, size / 3, position.height);
			Rect yRect = new(position.x + size / 3, position.y, size / 3, position.height);
			Rect zRect = new(position.x + size/3*2, position.y, position.width - size / 3, position.height);

			GUIContent xLabel = new("x");
			GUIContent yLabel = new("x");
			GUIContent zLabel = new("z");


			// Draw fields - pass GUIContent.none to each so they are drawn without labels
			EditorGUI.PropertyField(xRect, property.FindPropertyRelative("x"), GUIContent.none);
			EditorGUI.PropertyField(yRect, property.FindPropertyRelative("y"), GUIContent.none);
			EditorGUI.PropertyField(zRect, property.FindPropertyRelative("z"), GUIContent.none);

			// Set indent back to what it was
			EditorGUI.indentLevel = indent;

			EditorGUI.EndProperty();
		}
	}





}










