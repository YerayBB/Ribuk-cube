using UnityEngine;
using System.Collections;

namespace UtilsUnknown
{
    public static class RandomExtra
    {
        public static Vector3 RandomAxis()
        {
            int aux = Random.Range(0, 6);
            switch (aux)
            {
                case 0:
                    return Vector3.right;
                case 1:
                    return Vector3.left;
                case 2:
                    return Vector3.up;
                case 3:
                    return Vector3.down;
                case 4:
                    return Vector3.forward;
                default:
                    return Vector3.back;
            }
        }

        public static bool RandomBool()
        {
            int aux = Random.Range(0, 100);
            return aux < 50;
        }
    }
}