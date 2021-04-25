using UnityEngine;

namespace Gameplay
{
    [RequireComponent(typeof(Rigidbody))]
    public class PinController : MonoBehaviour
    {
        private bool isDown = false;
        private Vector3 originalPosition;
        private Quaternion originalRotation;

        private Rigidbody _body;
       
        void Start()
        {
            originalPosition = transform.position;
            originalRotation = transform.rotation;

            _body = GetComponent<Rigidbody>();
        }

        public void MoveTo(Vector3 position)
        {
            _body.velocity = Vector3.zero;
            _body.angularVelocity = Vector3.zero;
            transform.position = position;
        }
        
        public void ResetState()
        {
            _body.velocity = Vector3.zero;
            _body.angularVelocity = Vector3.zero;
            transform.rotation = originalRotation;
            transform.position = originalPosition;
        }

        public bool IsDown() => isDown;
        public bool HasBeenDowned()
        {
            if (isDown) return false;
            isDown = originalRotation != transform.rotation;
            return isDown;
        }
    }
}
