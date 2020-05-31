using System.Globalization;
using Code.Events.Core;
using TMPro;
using UnityEngine;

namespace Code
{
    public class Timer : MonoBehaviour
    {
            
        [SerializeField] public TextMeshProUGUI[] counterTextUIs;

        [SerializeField] private string onZeroText = "0";

        [SerializeField] private bool runOnAwake;

        [SerializeField] private bool showCountDownInUI;

        private float timeLeft;

        [SerializeField] private AtomicEvent timerDoneEvent;

        private bool timerIsRunning;
        
        public bool IsRunning
        {
            get { return timerIsRunning; }
        }

        [SerializeField] private float timerLength;

        private bool uiDisabled;

        private void Awake()
        {
            if (runOnAwake) StartTimer();
        }

        private void Update()
        {
            if (timerIsRunning) CountDownTime();
        }

        public void StartTimer()
        {
            timeLeft = timerLength;
            timerIsRunning = true;
        }
        
        public void StopTimer()
        {
            timerIsRunning = false;
        }
        
        private void disableUI()
        {
            uiDisabled = true;
            UpdateTextUI();
        }

        private void CountDownTime()
        {
            timeLeft -= Time.deltaTime;
            var timeLeftInt = (int) timeLeft;

            if (showCountDownInUI && timeLeftInt > 0)
                UpdateTextUI(timeLeftInt.ToString(CultureInfo.CurrentCulture));
            else if (timeLeftInt == 0) UpdateTextUI(onZeroText);

            if (timeLeftInt < 0)
            {
                UpdateTextUI();
                timerIsRunning = false;
                timerDoneEvent.Trigger();
            }
        }

        private void UpdateTextUI(string message = "")
        {
            foreach (var counterTextUI in counterTextUIs)
            {
                if (counterTextUI == null) continue;
                
                if (!uiDisabled)
                    counterTextUI.text = message;
                else
                    counterTextUI.text = "";
            }
           
        }
    }
}