using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UtilsUnknown.Extensions;

namespace Ribuk
{
    public class RubikCube : MonoBehaviour
    {
        private Transform _transform;
        private CubeFace[] _faces = new CubeFace[6];

        private uint _activeFace = 1;
        private static readonly Dictionary<Vector3, int> _faceMapping = new Dictionary<Vector3, int>()
        {
            {Vector3.forward, 5 },
            {Vector3.back, 0 },
            {Vector3.up, 3 },
            {Vector3.down, 2 },
            {Vector3.left, 4 },
            {Vector3.right, 1 }
        };




        private void Awake()
        {
            _transform = transform;

            for (int i = 0; i < 6; ++i)
            {
                _faces[i] = _transform.GetChild(i).GetComponent<CubeFace>();
            }

            foreach (var face in _faces)
            {
                face.gameObject.SetActive(false);
            }
        }

        private void OnEnable()
        {
            Vector3 f = _transform.worldToLocalMatrix * Vector3.back;
            Debug.Log($"pre {f}");
            Debug.Log(f.x);
            Debug.Log(f.y);
            Debug.Log(f.z);
            f = f.Rounded();
            Debug.Log($"post {f}");
            Debug.Log(f.x);
            Debug.Log(f.y);
            Debug.Log(f.z);
            Debug.Log(_faceMapping[f]);
        }

    }
}
