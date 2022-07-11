using UnityEngine;

namespace Essentials.Extensions
{

    public static class VectorExtensions
    {

        public static float Random(this Vector2 vec)
        {
            return UnityEngine.Random.Range(vec.x, vec.y);
        }

        public static int Random(this Vector2Int vec)
        {
            return UnityEngine.Random.Range(vec.x, vec.y + 1);
        }

        public static Vector3 GetRandomPoint(this Bounds bounds)
        {
            return
                new Vector3(
                    UnityEngine.Random.Range(bounds.min.x, bounds.max.x),
                    UnityEngine.Random.Range(bounds.min.y, bounds.max.y),
                    UnityEngine.Random.Range(bounds.min.z, bounds.max.z)
                );
        }

        public static Vector2 GetRandomPoint(this Vector2 areaSize)
        {
            return
                new Vector2(
                    UnityEngine.Random.Range(-areaSize.x, areaSize.x),
                    UnityEngine.Random.Range(-areaSize.y, areaSize.y)
                );
        }

        public static Vector3 GetRandomPoint(this Vector3 areaSize)
        {
            return
                new Vector3(
                    UnityEngine.Random.Range(-areaSize.x, areaSize.x),
                    UnityEngine.Random.Range(-areaSize.y, areaSize.y),
                    UnityEngine.Random.Range(-areaSize.z, areaSize.z)
                );
        }

        public static Vector3 GetRandomPoint(this Vector3 areaSize, float y)
        {
            return
                new Vector3(
                    UnityEngine.Random.Range(-areaSize.x, areaSize.x),
                    y,
                    UnityEngine.Random.Range(-areaSize.z, areaSize.z)
                );
        }

        public static Vector2 Multiply(this Vector2Int v1, Vector2 v2)
        {
            return new Vector2(
                v1.x * v2.x,
                v1.y * v2.y
            );
        }

        public static Vector3 Multiply(this Vector3Int v1, Vector3 v2)
        {
            return new Vector3(
                v1.x * v2.x,
                v1.y * v2.y,
                v1.z * v2.z
            );
        }

        public static Vector2 Multiply(this Vector2 v1, Vector2 v2)
        {
            return new Vector2(
                v1.x * v2.x,
                v1.y * v2.y
            );
        }

        public static Vector3 Multiply(this Vector3 v1, Vector3 v2)
        {
            return new Vector3(
                v1.x * v2.x,
                v1.y * v2.y,
                v1.z * v2.z
            );
        }

        public static Vector3 Tangent(this Vector3 v, Vector3 forward, Vector3 up)
        {
            Vector3 tangent;
            Vector3 t1 = Vector3.Cross(v, forward);
            Vector3 t2 = Vector3.Cross(v, up);
            if (t1.magnitude > t2.magnitude)
            {
                tangent = t1;
            }
            else
            {
                tangent = t2;
            }

            return tangent;
        }

        public static bool IsBetween(this float value, Vector2 range)
        {
            return range.x < value && range.y > value;
        }

    }

}
