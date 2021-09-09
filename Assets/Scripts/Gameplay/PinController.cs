using System.Collections;
using Manager;
using UnityEngine;

namespace Gameplay
{

    public class PinController : MonoBehaviour
    {
        [SerializeField] private float downEvaluationAngle = 15f;
        private bool isDown = false;
        private Quaternion originalRotation;
        
        void Start()
        {
            originalRotation = transform.rotation;
        }

        public bool HasBeenDowned()
        {
            return Quaternion.Angle(originalRotation, transform.rotation) >= downEvaluationAngle;
        }

        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.CompareTag("Floor")) return;
            if(!isDown)
                StartCoroutine(EvaluateStateOnCollision());
        }

        private IEnumerator EvaluateStateOnCollision()
        {
            yield return new WaitForSeconds(0.5f);
            if (!HasBeenDowned()) yield break;
            isDown = true;
            GameManager.Instance.TryToClearStage();
        }
    }
}
