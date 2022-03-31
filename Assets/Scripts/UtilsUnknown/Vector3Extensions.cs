using UnityEngine;

namespace UtilsUnknown.Extensions
{
    public static class Vector3Extensions
    {

        public static float MinValue(this Vector3 vector3)
        {
            return Mathf.Min(vector3.x, Mathf.Min(vector3.y, vector3.z));
        }

        public static float MaxValue(this Vector3 vector3)
        {
            return Mathf.Max(vector3.x, Mathf.Max(vector3.y, vector3.z));
        }

        public static float Sum(this Vector3 vector3)
        {
            return vector3.x + vector3.y + vector3.z;
        }

        public static Vector3 Rounded(this Vector3 vector3)
        {
            return new Vector3(Mathf.Round(vector3.x), Mathf.Round(vector3.y), Mathf.Round(vector3.z));
        }
    }

    public static class Vector3IntExtensions
    {

        public static int MinValue(this Vector3Int vector3)
        {
            return Mathf.Min(vector3.x, Mathf.Min(vector3.y, vector3.z));
        }

        public static int MaxValue(this Vector3Int vector3)
        {
            return Mathf.Max(vector3.x, Mathf.Max(vector3.y, vector3.z));
        }

        public static int Sum(this Vector3Int vector3)
        {
            return vector3.x + vector3.y + vector3.z;
        }

    }
}
