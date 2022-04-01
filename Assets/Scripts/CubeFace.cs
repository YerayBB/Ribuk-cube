using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ribuk
{
    public class CubeFace : MonoBehaviour
    {
        private Transform _transform;
        private Rigidbody _rigidbody;
        private BoxCollider _boxCollider;

        private Vector3 _originRotation;
        private Vector3 _targetRotation;
        private bool activa = false;
        public static float completeSpinTime = 0.5f;
        private static bool locked = false;
        private float _spinTime = 0;
        private List<Transform> _contacts = new List<Transform>();


        private void Awake()
        {
            _transform = transform;
            _rigidbody = GetComponent<Rigidbody>();
            _boxCollider = GetComponent<BoxCollider>();
            _targetRotation = _transform.localEulerAngles;
            _originRotation = _targetRotation;
        }

        private void FixedUpdate()
        {
            if (activa)
            {
                if (_spinTime < completeSpinTime)
                {
                    _spinTime += Time.deltaTime;
                    _transform.localEulerAngles = Vector3.Lerp(_originRotation, _targetRotation, _spinTime / completeSpinTime);
                }
                else
                {
                    _transform.localEulerAngles = _targetRotation;
                    activa = false;
                    DetachChildren();
                    //gameObject.SetActive(false);
                }
            }
        }

        public void Spin(bool clockwise)
        {
            GrabChilds();
            _originRotation = _transform.localEulerAngles;
            _targetRotation = _originRotation + Vector3.forward * (clockwise ? 90 : -90);
            _spinTime = 0;
            activa = true;
        }

        private void OnTriggerEnter(Collider collider)
        {
            _contacts.Add(collider.transform);
        }

        private void OnTriggerExit(Collider collider)
        {
            _contacts.Remove(collider.transform);
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
    }
}
