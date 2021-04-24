using System;
using UnityEngine;
using UnityEngine.Events;

namespace Gameplay
{
    [Serializable]
    public class ForceUpdateEvent : UnityEvent<float, float> {}
    [RequireComponent(typeof(Rigidbody))]
    public class BallForceController : BaseBallController
    {
        public UnityEvent onBallLaunched;
        public ForceUpdateEvent onForceUpdate;
    
        private Rigidbody _body = null;
    
        [Header("Direction Config")]
        [SerializeField] private GameObject marker;
    
        [Header("Force Config")]
        [SerializeField] private float incrementalForceStep = 0f;
        [SerializeField] private float maxForce = 100f;
        private float minForce = 0f;
        private float force = 0f;

        private bool _canFireTheBall = false;
        private bool _isCharging = false;

        private void Start()
        {
            _body = GetComponent<Rigidbody>();
        }

        private void Update()
        {
            if (!IsEnabled) return;
            UpdateForce(Time.deltaTime);
        }

        private void FixedUpdate()
        {
            if (!IsEnabled || _isCharging || !_canFireTheBall) return;
            Vector3 forward = marker?.transform.forward ?? transform.forward;
            _body.AddForce(forward * force, ForceMode.Impulse);
            force = 0f;
            _canFireTheBall = false;
            onBallLaunched?.Invoke();
            onForceUpdate?.Invoke(force, maxForce);
        }

    
        private void UpdateForce(float elapsed)
        {
            if (Input.GetButtonDown("Fire1"))
                _canFireTheBall = true;

            _isCharging = _canFireTheBall && Input.GetButton("Fire1");
            if (!_isCharging) return;
            
            float forceStep = incrementalForceStep * elapsed;

            force += forceStep;
            force = Mathf.Clamp(force , minForce, maxForce);

            if (force >= maxForce)
            {
                force = 0;
                _isCharging = false;
                _canFireTheBall = false;
            }

            onForceUpdate?.Invoke(force, maxForce);
        }
    }
}
