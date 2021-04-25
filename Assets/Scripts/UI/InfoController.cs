using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class InfoController : MonoBehaviour
    {
        [SerializeField] private Text info;

        public void OnInfoUpdated(string value)
        {
            info.text = value;
        }
    }
}
