using UnityEngine;

namespace BTQ.Core.Extensions
{
	public static class Vector3Extensions
	{
		public static Vector2 XX(this Vector3 vec)
		{
			return new Vector2(vec.x, vec.x);
		}

		public static Vector2 XY(this Vector3 vec)
		{
			return new Vector2(vec.x, vec.y);
		}

		public static Vector2 XZ(this Vector3 vec)
		{
			return new Vector2(vec.x, vec.z);
		}

		public static Vector2 YX(this Vector3 vec)
		{
			return new Vector2(vec.y, vec.x);
		}

		public static Vector2 YY(this Vector3 vec)
		{
			return new Vector2(vec.y, vec.y);
		}

		public static Vector2 YZ(this Vector3 vec)
		{
			return new Vector2(vec.y, vec.z);
		}

		public static Vector2 ZX(this Vector3 vec)
		{
			return new Vector2(vec.z, vec.x);
		}

		public static Vector2 ZY(this Vector3 vec)
		{
			return new Vector2(vec.z, vec.y);
		}

		public static Vector2 ZZ(this Vector3 vec)
		{
			return new Vector2(vec.z, vec.z);
		}

		public static Vector3 With(this Vector3 vec, float? x = null, float? y = null, float? z = null)
		{
			return new Vector3(x ?? vec.x, y ?? vec.y, z ?? vec.z);
		}

		public static Vector3 Add(this Vector3 vec, float x = 0, float y = 0, float z = 0)
		{
			return new Vector3(vec.x + x, vec.y + y, vec.z + z);
		}
	}
}