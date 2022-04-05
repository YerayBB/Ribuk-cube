using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ribuk
{
    [RequireComponent(typeof(BoxCollider))]
    public class CubeFace : MonoBehaviour
    {
        public static float completeSpinTime = 0.2f;
        public static bool locked { get; private set; } = false;

        private Transform _transform;
        private BoxCollider _boxCollider;

        private List<Transform> _contacts = new List<Transform>();
        private Vector3 _originRotation;
        private Vector3 _targetRotation;
        private bool _spinning = false;
        private float _spinTime = 0;


        #region MonoBehaviourCalls

        private void Awake()
        {
            _transform = transform;
            _boxCollider = GetComponent<BoxCollider>();
            _targetRotation = _transform.localEulerAngles;
            _originRotation = _targetRotation;
        }

        private void FixedUpdate()
        {
            if (_spinning)
            {
                if (_spinTime < completeSpinTime && completeSpinTime > 0)
                {
                    _spinTime += Time.deltaTime;
                    _transform.localEulerAngles = Vector3.Lerp(_originRotation, _targetRotation, _spinTime / completeSpinTime);
                }
                else
                {
                    _transform.localEulerAngles = _targetRotation;
                    _spinning = false;
                    DetachChildren();
                }
            }
        }

        private void OnTriggerEnter(Collider collider)
        {
            _contacts.Add(collider.transform);
        }

        private void OnTriggerExit(Collider collider)
        {
            _contacts.Remove(collider.transform);
        }

        #endregion

        public IEnumerator SpinCoroutine(bool clockwise)
        {
            if (!locked)
            {
                locked = true;
                _boxCollider.enabled = true;
                yield return new WaitForFixedUpdate();
                Spin(clockwise);
            }
        }

        private void Spin(bool clockwise)
        {
            GrabChilds();
            _originRotation = _transform.localEulerAngles;
            _targetRotation = _originRotation + Vector3.forward * (clockwise ? 90 : -90);
            _spinTime = 0;
            _spinning = true;
        }

        private void GrabChilds()
        {
            for(int i = 0; i< _contacts.Count; ++i)
            {
                _contacts[i].parent = _transform;
            }
            _boxCollider.enabled = false;
        }

        private void DetachChildren()
        {
            while(_transform.childCount > 0)
            {
                _transform.GetChild(0).parent = _transform.parent;
            }
            _contacts.Clear();
            locked = false;
        }

    }
}
