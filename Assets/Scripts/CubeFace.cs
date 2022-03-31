using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ribuk
{
    public class CubeFace : MonoBehaviour
    {
        private Transform _transform;

        private Vector3 _originRotation;
        private Vector3 _targetRotation;
        private bool activa = false;
        private static float _completeSpinTime = 0.5f;
        private float _spinTime = 0;


        private void Awake()
        {
            _transform = transform;
            _targetRotation = _transform.localEulerAngles;
        }

        private void Update()
        {
            if (activa)
            {
                if (_spinTime < _completeSpinTime)
                {
                    _spinTime += Time.deltaTime;
                    _transform.localEulerAngles = Vector3.Lerp(_originRotation, _targetRotation, _spinTime / _completeSpinTime);
                }
                else
                {
                    _transform.localEulerAngles = _targetRotation;
                    activa = false;
                    DetachChildren();
                    gameObject.SetActive(false);
                }
            }
        }

        public void Spin(bool clockwise)
        {
            _originRotation = _transform.localEulerAngles;
            _targetRotation = _originRotation + Vector3.forward * (clockwise ? 90 : 270);
            _spinTime = 0;
            activa = true;
        }

        private void OnTriggerEnter(Collider collider)
        {
            Debug.Log($"Bump {collider.gameObject.name}");
            collider.transform.parent = _transform;
        }

        private void OnDisable()
        {
            StopAllCoroutines();
            //_transform.DetachChildren();
            Debug.Log("Done");
        }

        private void OnEnable()
        {
            StartCoroutine(Test());
        }

        private IEnumerator Test()
        {
            Debug.Log("wit for it");
            yield return new WaitForSeconds(1);
            Debug.Log("It begns");
            Spin(true);
        }

        private void DetachChildren()
        {
            while(_transform.childCount > 0)
            {
                _transform.GetChild(0).parent = _transform.parent;
            }
        }
    }
}
