using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class ForceBarController : MonoBehaviour
    {
        [SerializeField] private Slider force;

        public void OnForceUpdate(float value, float maxValue)
        {
            force.value = value / maxValue;
        }
    }
}
