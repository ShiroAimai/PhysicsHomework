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
    [Serializable]
    public class BallTimerUpdateEvent : UnityEvent<float> {}
    
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance = null;

        #region Events
        public InfoUpdateEvent onPinsDownUpdate;
        public InfoUpdateEvent onLaunchUpdate;
        public InfoUpdateEvent onEndGame;
        public BallTimerUpdateEvent onBallTimerUpdate;
        #endregion
        
        [Header("Round Configuration")]
        [SerializeField] private int playerRounds = 2;
        private int currentPlayerRound = 1;
        
        [SerializeField] private float clearStageAfterSeconds = 5f;
        private Coroutine clearRoutine = null;
        
        [SerializeField] private float ballTimerToReachPins = 10f;
        private float currentBallTimer = 0f;
        private bool shouldUpdateBallTimer = false;

        [SerializeField] private BallStateHandler ball;
        [SerializeField] private List<PinController> pins;
        private int pinsDown = 0;
        
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
            UpdateGameInfos();

            currentBallTimer = ballTimerToReachPins;
            
            if(ball)
                ball.OnBallLaunch += OnBallLaunched;
        }

        private void Update()
        {
            if (!shouldUpdateBallTimer) return;
           
            currentBallTimer -= Time.deltaTime;
            
            if (currentBallTimer <= 0f)
            {
                ResetBallTimer();
                ClearStage_Impl();
            }
            else
            {
                onBallTimerUpdate?.Invoke(currentBallTimer);
            }
        }

        private void OnDestroy()
        {
            if (ball != null)
                ball.OnBallLaunch -= OnBallLaunched;
        }

        public void Restart()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        private void OnBallLaunched()
        {
            shouldUpdateBallTimer = true;
        }

        private void ResetBallTimer()
        {
            shouldUpdateBallTimer = false;
            currentBallTimer = ballTimerToReachPins;
            onBallTimerUpdate?.Invoke(0f);
        }
        
        public void TryToClearStage()
        {
            ResetBallTimer();
            if(clearRoutine != null)
                StopCoroutine(clearRoutine);
            clearRoutine = StartCoroutine(ClearStage());
        }
        
        private IEnumerator ClearStage()
        {
            yield return new WaitForSeconds(clearStageAfterSeconds);
            ClearStage_Impl();
        }

        private void ClearStage_Impl()
        {
            currentPlayerRound++;
            foreach (var pin in pins)
            {
                if (!pin.HasBeenDowned() || !pin.gameObject.activeSelf) continue;
                pinsDown++;
                pin.gameObject.SetActive(false);
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
