using System;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay
{
    public class BallStateHandler : MonoBehaviour
    {
        public Action OnBallLaunch;
        
        private readonly List<BaseBallController> ballControllers = new List<BaseBallController>();

        private Vector3 originalPosition;
        private Quaternion originalRotation;
        
        private void Start()
        {
            ballControllers.AddRange( GetComponents<BaseBallController>());
            originalPosition = transform.position;
            originalRotation = transform.rotation;
        }
    
        public void ResetBallState()
        {
            transform.rotation = originalRotation;
            transform.position = originalPosition;
            SetBallState(false);
        }

        public void OnBallLaunched()
        {
            SetBallState(true);
            OnBallLaunch?.Invoke();
        }

        private void SetBallState(bool _isLaunched)
        {
            ballControllers.ForEach(controller => controller.IsEnabled = !_isLaunched);
        }
    }
}
