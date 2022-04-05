using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;

namespace UtilsUnknown.UI
{
    public class ButtonHoldDown : Selectable
    {
        [Header("Settings")]
        [SerializeField] private float _holdTime = 0.5f;

        [Header("Callbacks")]
        public UnityEvent onClick;
        public UnityEvent onHold;

        private Coroutine _coroutine = null;
        private float _holdStart = 0f;
        private bool _started = false;


        #region Pointers Handlers
        public override void OnPointerDown(PointerEventData eventData)
        {
            base.OnPointerDown(eventData);
            StartHold();
        }

        public override void OnPointerUp(PointerEventData eventData)
        {
            base.OnPointerUp(eventData);
            StopHold();
        }

        public override void OnPointerExit(PointerEventData eventData)
        {
            base.OnPointerExit(eventData);
            ResetHold();
        }
        #endregion

        #region Hold Management
        private void StartHold()
        {
            ResetHold();
            _holdStart = Time.realtimeSinceStartup;
            _coroutine = StartCoroutine(HoldTimerCoroutine());
            _started = true;
        }

        private void StopHold()
        {
            if (_started)
            {
                if (Time.realtimeSinceStartup - _holdStart > _holdTime)
                {
                    onHold.Invoke();
                }
                else
                {
                    onClick.Invoke();
                }
            }
            ResetHold();
            _holdStart = Time.realtimeSinceStartup;
        }

        private void ResetHold()
        {
            if (_coroutine != null)
            {
                StopCoroutine(_coroutine);
                _coroutine = null;
            }
            _started = false;
        }

        private IEnumerator HoldTimerCoroutine()
        {
            yield return new WaitForSecondsRealtime(_holdTime);
            StopHold();
        }

        protected override void OnDestroy()
        {
            ResetHold();
        }
        #endregion
    }
}