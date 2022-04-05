using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UtilsUnknown.Extensions;
using UtilsUnknown;

namespace Ribuk
{
    public class RubikCube : MonoBehaviour
    {
        [SerializeField]
        private int _undoCapacity = 10;
        [SerializeField]
        private float _spinAnimationTime = 0.2f;

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

        private DropOutStack<Move> _movesDone;

        private struct Move: System.ICloneable
        {
            public int face;
            public bool clockwise;

            public Move(int face, bool clockwise)
            {
                this.face = face;
                this.clockwise = clockwise;
            }

            public object Clone()
            {
                return new Move(this.face, this.clockwise);
            }
        }

        #region MonoBehaviourCalls

        private void Awake()
        {
            _transform = transform;
            _movesDone = new DropOutStack<Move>(_undoCapacity);

            for (int i = 0; i < 6; ++i)
            {
                _faces[i] = _transform.GetChild(i).GetComponent<CubeFace>();
            }
        }

        #endregion

        public void SpinCubeHorizontal(bool clockwise)
        {
            StartCoroutine(RotateCube(Vector3.up * (clockwise ? -90 : 90)));
        }

        public void SpinCubeVertical(bool clockwise)
        {
            StartCoroutine(RotateCube(Vector3.right * (clockwise ? -90 : 90)));
        }

        public void SpinTopFace(bool clockwise)
        {
            Vector3 f = _transform.worldToLocalMatrix * Vector3.up;
            int index = _faceMapping[f.Rounded()];
            if (!CubeFace.locked)
            {
                _movesDone.Push(new Move(index, clockwise));
                StartCoroutine(_faces[index].SpinCoroutine(clockwise));
            }
        }

        public void SpinBottomFace(bool clockwise)
        {
            Vector3 f = _transform.worldToLocalMatrix * Vector3.down;
            int index = _faceMapping[f.Rounded()];
            if (!CubeFace.locked)
            {
                _movesDone.Push(new Move(index, clockwise));
                StartCoroutine(_faces[index].SpinCoroutine(clockwise));
            }
        }

        public void SpinLeftFace(bool clockwise)
        {
            Vector3 f = _transform.worldToLocalMatrix * Vector3.left;
            int index = _faceMapping[f.Rounded()];
            if (!CubeFace.locked)
            {
                _movesDone.Push(new Move(index, clockwise));
                StartCoroutine(_faces[index].SpinCoroutine(clockwise));
            }
        }

        public void SpinRightFace(bool clockwise)
        {
            Vector3 f = _transform.worldToLocalMatrix * Vector3.right;
            int index = _faceMapping[f.Rounded()];
            if (!CubeFace.locked)
            {
                _movesDone.Push(new Move(index, clockwise));
                StartCoroutine(_faces[index].SpinCoroutine(clockwise));
            }
        }

        public void Undo()
        {
            if (_movesDone.Count > 0)
            {
                if (!CubeFace.locked)
                {
                    Move move = _movesDone.Pop();
                    StartCoroutine(_faces[move.face].SpinCoroutine(!move.clockwise));
                }
            }
        }

        private IEnumerator RotateCube(Vector3 rotation)
        {
            float done = 0f;
            float actual = 0;
            yield return null;
            while(done < 1)
            {
                actual = _spinAnimationTime > 0 ? Time.deltaTime / _spinAnimationTime : 1;
                actual = Mathf.Min(actual, 1 - done);
                _transform.Rotate(rotation * actual, Space.World);
                done += actual;
                yield return null;
            }

            yield return null;
        }

        public void Randomize()
        {
            StartCoroutine(RandomizeCoroutine(50));
        }

        private IEnumerator RandomizeCoroutine(int spins)
        {
            float faceTime = CubeFace.completeSpinTime;
            float cubeTime = _spinAnimationTime;
            CubeFace.completeSpinTime = 0;
            _spinAnimationTime = 0;
            for (int i = 0; i < spins; ++i)
            {
                yield return new WaitWhile(() => CubeFace.locked);
                StartCoroutine(_faces[_faceMapping[RandomExtra.RandomAxis()]].SpinCoroutine(RandomExtra.RandomBool()));
            }
            CubeFace.completeSpinTime = faceTime;
            _spinAnimationTime = cubeTime;
            _movesDone.Clear();
        }
    }
}
