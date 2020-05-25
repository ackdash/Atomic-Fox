using System.Globalization;
using Code.Events.Core;
using TMPro;
using UnityEngine;

namespace Code.GUI
{
    public class GUITimer : MonoBehaviour
    {
      
        [SerializeField]
        private AtomicEvent timerDoneEvent;

        [SerializeField]
        private bool showCountDownInUI;

        [SerializeField]
        private float timerLength;

        private float timeLeft;

        [SerializeField]
        private TextMeshProUGUI counterTextUI;

        [SerializeField]
        private string onZeroText = "0";

        private bool timerIsRunning = false;
        private bool uiDisabled = false;

        private void Awake()
        {
            StartTimer();
        }

        private void Update()
        {
            if (timerIsRunning)
            {
                CountDownTime();
            }
        }

        private void StartTimer()
        {
            timeLeft = timerLength;
            timerIsRunning = true;
        }

        private void disableUI()
        {
            uiDisabled = true;
            UpdateTextUI("");
        }

        private void CountDownTime()
        {
            timeLeft -= Time.deltaTime;
            var timeLeftInt = (int) timeLeft;

            if (showCountDownInUI && timeLeftInt > 0)
            {
                UpdateTextUI(timeLeftInt.ToString(CultureInfo.CurrentCulture));
            }
            else if (timeLeftInt == 0)
            {
                UpdateTextUI(onZeroText);
            }

            if (timeLeftInt < 0)
            {
                UpdateTextUI();
                timerIsRunning = false;
                timerDoneEvent.Trigger();
            }
        }

        private void UpdateTextUI(string message = "")
        {
            if (!uiDisabled)
            {
                counterTextUI.text = message;
            }
            else
            {
                counterTextUI.text = "";
            }
        }
    }
}
