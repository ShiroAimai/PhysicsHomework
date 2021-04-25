using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class BallTimerController : MonoBehaviour
    {
        [SerializeField] private Text timer;
    
        private void Start()
        {
            timer.text = "";
        }

        public void UpdateTimer(float time)
        {
            if (time <= 0f)
            {
                timer.text = "";
                return;
            }

            int minutes = ((int) time / 60);
            int seconds = (int) time % 60;
            
            timer.text = $"{minutes:00}:{seconds:00}";
        } 
    }
}
