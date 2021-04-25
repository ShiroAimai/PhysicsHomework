using System;
using System.Collections;
using System.Collections.Generic;
using Gameplay;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace Manager
{
    [Serializable]
    public class InfoUpdateEvent : UnityEvent<String> {}
    
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance = null;

        public InfoUpdateEvent onPinsDownUpdate;
        public InfoUpdateEvent onLaunchUpdate;
        public InfoUpdateEvent onEndGame;
        
        [SerializeField] private float clearStageAfterSeconds = 5f;
        [SerializeField] private Transform pinsRestPositionInWorld;
        
        [SerializeField] private BallStateHandler ball;
        [SerializeField] private List<PinController> pins;
        private int pinsDown = 0;
        
        [SerializeField] private int playerRounds = 2;
        private int currentPlayerRound = 1;
        
        private void Awake()
        {
            if (Instance == null) {
                Instance = this;
            } else {
                Destroy(gameObject);
            }
        }

        private void Start()
        {
            if(ball)
                ball.OnBallLaunch += ClearOnLaunchBall;
            
            UpdateGameInfos();
        }

        private void OnDestroy()
        {
            if (ball != null)
            {
                ball.OnBallLaunch -= ClearOnLaunchBall;
            }
        }

        public void Restart()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        private void ClearOnLaunchBall()
        {
            StartCoroutine(ClearStage());
        }
        
        private IEnumerator ClearStage()
        {
            yield return new WaitForSeconds(clearStageAfterSeconds);
            
            currentPlayerRound++;
            foreach (var pin in pins)
            {
                if (!pin.HasBeenDowned()) continue;
                pinsDown++;
                pin.MoveTo(pinsRestPositionInWorld.position);
            }

            if (currentPlayerRound > playerRounds || pinsDown == pins.Count)
            {
                onEndGame?.Invoke(ProduceEndGameText());
            }
            else
            {
                ball.ResetBallState();
                UpdateGameInfos();
            }
        }

        private void UpdateGameInfos()
        {
            onLaunchUpdate?.Invoke(currentPlayerRound.ToString());
            onPinsDownUpdate?.Invoke(pinsDown.ToString());
        }

        private string ProduceEndGameText()
        {
            if (currentPlayerRound > playerRounds)
            {
                if (pinsDown < pins.Count)
                    return $"{pinsDown} PINS DOWNED";
                
                return "SPARE!";
            }

            return "STRIKE!";
        }
    }
}
