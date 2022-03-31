using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ribuk
{
    public class CubeFace : MonoBehaviour
    {
        private Transform _transform;

        private Vector3 _targetRotation;

        private void Awake()
        {
            _transform = transform;
            _targetRotation = _transform.localEulerAngles;
        }

        private void Update()
        {
            if(_transform.localEulerAngles != _targetRotation)
            {
                _transform.localEulerAngles = Vector3.Lerp(_transform.localEulerAngles, _targetRotation, Time.deltaTime);
            }
            else
            {
                /*_transform.DetachChildren();
                gameObject.SetActive(false);*/
            }
        }

        public void Spin(bool clockwise)
        {
            _targetRotation = _transform.localEulerAngles + _transform.forward * (clockwise ? 90 : -90);

        }

        private void OnTriggerEnter(Collider collider)
        {
            Debug.Log($"Bump {collider.gameObject.name}");
            collider.transform.parent = _transform;
        }

        private void OnDisable()
        {
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

    }
}
