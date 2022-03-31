using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ribuk
{
    public class RubikCube : MonoBehaviour
    {
        private Transform _transform;
        private CubeFace[] _faces = new CubeFace[6];


        private void Awake()
        {
            _transform = transform;
            for(int i = 0; i<6; ++i)
            {
                _faces[i] = _transform.GetChild(i).GetComponent<CubeFace>();
            }

            foreach(var face in _faces)
            {
                face.gameObject.SetActive(false);
            }
        }
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
