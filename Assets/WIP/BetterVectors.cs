using System;
using TrigHelper;
using UnityEngine;

namespace BetterVectors
{
    public struct Vector3
    {
        public Vector3(float x, float y, float z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public float x;
        public float y;
        public float z;

        public UnityEngine.Vector3 unity { get => this; set => this = value; }

        public Vector3 normalized => this / magnitude;
        public void Normalize() => this = normalized;

        public float magnitude => ((x * x) + (y * y) + (z * z)).SQRT();
        public float sqrMagnitude => (x * x) + (y * y) + (z * z);

        public static implicit operator UnityEngine.Vector3(Vector3 v)
        {
            UnityEngine.Vector3 result;
            result.x = v.x;
            result.y = v.y;
            result.z = v.z;
            return result;
        }
        public static implicit operator Vector3(UnityEngine.Vector3 v)
        {
            Vector3 result;
            result.x = v.x;
            result.y = v.y;
            result.z = v.z;
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

        public static Vector3 upRight = new(1, 1, 0);
        public static Vector3 frontRight = new(0, 1, 1);
        public static Vector3 upFront = new(0, 1, 1);
        public static Vector3 downLeft = new(-1, -1, 0);
        public static Vector3 backLeft = new(0, -1, -1);
        public static Vector3 downBack = new(0, -1, -1);

        public static Vector3 inf = new(float.PositiveInfinity, float.PositiveInfinity, float.PositiveInfinity);
        public static Vector3 nInf = new(float.NegativeInfinity, float.NegativeInfinity, float.NegativeInfinity);

        public Vector3 xz => new(x, 0, z);
        public Vector3 xy => new(x, y, 0);
        public Vector3 yz => new(0, y, z);

        public void Squash(Vector3 v) => this *= one - v.normalized;
        public Vector3 Squashed(Vector3 v) => this * (one - v.normalized);

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
        public Vector3 upTurn => Rotated(Eular.aroundTurn);
        public Vector3 downTurn => Rotated(Eular.upTurn);
        public Vector3 backTurn => Rotated(Eular.downTurn);



    }
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

        public static implicit operator UnityEngine.Vector3(Eular v) => new(v.x, v.y, v.z);
        public static implicit operator Eular(UnityEngine.Vector3 v) => new(v.x, v.y, v.z);

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

        public static Eular operator -(Eular v)
        {
            Eular result;
            result.x = -v.x;
            result.y = -v.y;
            result.z = -v.z;
            return result;
        }
        public static Eular operator ++(Eular v)
        {
            Eular result;
            result.x = v.x + 1;
            result.y = v.y + 1;
            result.z = v.z + 1;
            return result;
        }
        public static Eular operator --(Eular v)
        {
            Eular result;
            result.x = v.x - 1;
            result.y = v.y - 1;
            result.z = v.z - 1;
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
    public struct Scale
    {
        public Scale(float x, float y, float z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public float x;
        public float y;
        public float z;

        public UnityEngine.Vector3 unity => (UnityEngine.Vector3)this;

        public static implicit operator UnityEngine.Vector3(Scale v) => new(v.x, v.y, v.z);
        public static implicit operator Scale(UnityEngine.Vector3 v) => new(v.x, v.y, v.z);

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

        public static Scale operator -(Scale v)
        {
            Scale result;
            result.x = -v.x;
            result.y = -v.y;
            result.z = -v.z;
            return result;
        }
        public static Scale operator ++(Scale v)
        {
            Scale result;
            result.x = v.x + 1;
            result.y = v.y + 1;
            result.z = v.z + 1;
            return result;
        }
        public static Scale operator --(Scale v)
        {
            Scale result;
            result.x = v.x - 1;
            result.y = v.y - 1;
            result.z = v.z - 1;
            return result;
        }

        public override bool Equals(object obj) => obj is Scale scale && x == scale.x && y == scale.y && z == scale.z;
        public override int GetHashCode() => HashCode.Combine(x, y, z);
    }



















    public static class Vector3ExtensionMethods
    {
        public static Vector3 Better(this UnityEngine.Vector3 v) => v;
    }

}

