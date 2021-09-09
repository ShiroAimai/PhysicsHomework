
using UnityEngine;

namespace Gameplay
{
    public abstract class BaseBallController : MonoBehaviour
    {
        private bool isIsEnabled = true;

        public bool IsEnabled
        {
            get => isIsEnabled;
            set
            {
                if (isIsEnabled == value) return;
                isIsEnabled = value;
                OnControllerStateChange(isIsEnabled);
            }
        }

        protected virtual void OnControllerStateChange(bool value)
        {
        }
    }
}