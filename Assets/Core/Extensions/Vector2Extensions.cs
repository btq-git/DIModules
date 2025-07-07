using UnityEngine;

namespace BTQ.Core.Extensions
{
    public static class Vector2Extensions
    {
        public static Vector3 X_Y(this Vector2 vec)
        {
            return new Vector3(vec.x, 0, vec.y);
        }

        public static Vector2 With(this Vector2 vec, float? x = null, float? y = null)
        {
            return new Vector2(x ?? vec.x, y ?? vec.y);
        }

        public static Vector2 Add(this Vector2 vec, float x = 0, float y = 0)
        {
            return new Vector2(vec.x + x, vec.y + y);
        }
    }
}