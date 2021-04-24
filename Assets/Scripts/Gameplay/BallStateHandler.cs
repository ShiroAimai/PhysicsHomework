using System.Collections.Generic;
using UnityEngine;

namespace Gameplay
{
    public class BallStateHandler : MonoBehaviour
    {
        public static bool isLaunched = false;
    
        private readonly List<BaseBallController> ballControllers = new List<BaseBallController>();

        private void Start()
        {
            ballControllers.AddRange( GetComponents<BaseBallController>());
        }
    
        public void ResetBallState()
        {
        
        }

        public void OnBallLaunched()
        {
            isLaunched = true;
            ballControllers.ForEach(controller => controller.IsEnabled = false);
        }
    }
}
