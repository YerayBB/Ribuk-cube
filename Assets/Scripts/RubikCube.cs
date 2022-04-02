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

        private static readonly Dictionary<Vector3, int> _faceMapping = new Dictionary<Vector3, int>()
        {
            {Vector3.forward, 5 },
            {Vector3.back, 0 },
            {Vector3.up, 3 },
            {Vector3.down, 2 },
            {Vector3.left, 4 },
            {Vector3.right, 1 }
        };

        [SerializeField]
        private float _spinAnimationTime = 0.5f;
        private bool _onAnimation = false;
        private Vector3 _originRotation = Vector3.zero;
        private Vector3 _rotation = Vector3.zero;




        private void Awake()
        {
            _transform = transform;

            for (int i = 0; i < 6; ++i)
            {
                _faces[i] = _transform.GetChild(i).GetComponent<CubeFace>();
            }

            /*foreach (var face in _faces)
            {
                face.gameObject.SetActive(false);
            }*/
        }

        public void SpinCubeHorizontal(bool clockwise)
        {
            /*if (!_onAnimation)
            {
                _originRotation = _rotation;
                Vector3 rot = _transform.worldToLocalMatrix * Vector3.up * (clockwise ? -90 : 90);
                //rot.Scale(_transform.localScale);
                _rotation += rot;
                _rotation = _rotation.Multiple(90);
                _rotation = _rotation.Mod(360);
                Debug.Log($"rot {rot}");
                StartCoroutine(SpinCube(_rotation));
            }*/
            StartCoroutine(RotateCube(Vector3.up * (clockwise ? -90 : 90)));
            return;
        }

        public void SpinCubeVertical(bool clockwise)
        {
            /*Vector3 rot = Vector3.right * (clockwise ? -90 : 90);
            //rot = Vector3.Scale(rot, _transform.localScale);
            rot += _transform.localEulerAngles;
            rot = rot.Multiple(90);*/
            /*if (!_onAnimation)
            {
                _originRotation = _rotation;
                Vector3 rot = _transform.worldToLocalMatrix * Vector3.right * (clockwise ? -90 : 90);
                //rot.Scale(_transform.localScale);
                _rotation += rot;
                _rotation = _rotation.Multiple(90);
                _rotation = _rotation.Mod(360);
                Debug.Log($"rot {rot}");
                StartCoroutine(SpinCube(rot));
            }*/
            StartCoroutine(RotateCube(Vector3.right * (clockwise ? -90 : 90)));
        }

        private IEnumerator SpinCube(Vector3 rotation)
        {
            Debug.Log(rotation);
            if (!_onAnimation)
            {
                _onAnimation = true;
                
                float time = 0f;
                while(time < _spinAnimationTime)
                {
                    time += Time.deltaTime;
                    _transform.eulerAngles = Vector3.Lerp(_originRotation, rotation, time / _spinAnimationTime);
                    yield return null;
                }
                _transform.eulerAngles = rotation;
                _onAnimation = false;
            }
            yield return null;
        }

        private IEnumerator RotateCube(Vector3 rotation)
        {
            _onAnimation = true;
            float done = 0f;
            float actual = 0;
            yield return null;
            while(done < 1)
            {
                actual = Time.deltaTime / _spinAnimationTime;
                actual = Mathf.Min(actual, 1 - done);
                _transform.Rotate(rotation * actual, Space.World);
                done += actual;
                yield return null;
            }

            yield return null;
        }

        public void SpinTopFace(bool clockwise)
        {
            Vector3 f = _transform.worldToLocalMatrix * Vector3.up;
            int index = _faceMapping[f.Rounded()];
            StartCoroutine(_faces[index].SpinCoroutine(clockwise));
        }
        public void SpinBottomFace(bool clockwise)
        {
            Vector3 f = _transform.worldToLocalMatrix * Vector3.down;
            int index = _faceMapping[f.Rounded()];
            StartCoroutine(_faces[index].SpinCoroutine(clockwise));
        }
        public void SpinLeftFace(bool clockwise)
        {
            Vector3 f = _transform.worldToLocalMatrix * Vector3.left;
            int index = _faceMapping[f.Rounded()];
            StartCoroutine(_faces[index].SpinCoroutine(clockwise));
        }
        public void SpinRightFace(bool clockwise)
        {
            Vector3 f = _transform.worldToLocalMatrix * Vector3.right;
            int index = _faceMapping[f.Rounded()];
            StartCoroutine(_faces[index].SpinCoroutine(clockwise));
            /*Debug.Log($"pre {f}");
            Debug.Log(f.x);
            Debug.Log(f.y);
            Debug.Log(f.z);
            f = f.Rounded();
            Debug.Log($"post {f}");
            Debug.Log(f.x);
            Debug.Log(f.y);
            Debug.Log(f.z);
            Debug.Log(_faceMapping[f]);*/
        }

    }
}
