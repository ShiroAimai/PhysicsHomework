using System.Collections;
using Manager;
using UnityEngine;

namespace Gameplay
{

    public class PinController : MonoBehaviour
    {
        private bool isDown = false;
        private Quaternion originalRotation;

        void Start()
        {
            originalRotation = transform.rotation;
        }

        public bool HasBeenDowned()
        {
            return originalRotation != transform.rotation;
        }

        private void OnCollisionEnter(Collision other)
        {
            if(!isDown)
                StartCoroutine(EvaluateStateOnCollision());
        }

        private IEnumerator EvaluateStateOnCollision()
        {
            yield return new WaitForSeconds(0.25f);
            if (!HasBeenDowned()) yield break;
            isDown = true;
            GameManager.Instance.TryToClearStage();
        }
    }
}
