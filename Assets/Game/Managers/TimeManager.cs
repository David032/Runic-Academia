using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;
using Runic.UI;

namespace Runic.Managers
{
    public enum Cycle
    {
        Day,
        Night
    }
    public enum Passage 
    {
        AM,
        PM
    }

    public class TimeManager : MonoSingleton<TimeManager>
    {
        public TextMeshProUGUI AmPmSpot;

        int days;
        public TextMeshProUGUI daySpot;

        [Range(0, 10)]
        int minutes;
        public TextMeshProUGUI minuteSpot;

        [Range(0,60)]
        int seconds;
        public TextMeshProUGUI secondsSpot;

        public float timer;

        [Range(1, 10)]
        public int timeRate = 1;

        public Cycle timeCycle = Cycle.Day;
        public Passage timePassage = Passage.AM;

        public UnityEvent DayChange = new UnityEvent();
        public UnityEvent CycleChange = new UnityEvent();

        GameObject player;
        bool isconfigured = false;
        private void Start()
        {
            Invoke("SetUpUI", 5f);
        }

        void SetUpUI() 
        {
            player = GameObject.FindGameObjectWithTag("Player");
            UIPassthrough positions = player.GetComponentInChildren<UIPassthrough>();
            AmPmSpot = positions.AmPmSpot;
            daySpot = positions.daySpot;
            minuteSpot = positions.minuteSpot;
            secondsSpot = positions.secondsSpot;
        }

        void Update()
        {
            if (!isconfigured)
            {
                return;
            }
            Time.timeScale = timeRate;
            timer += Time.deltaTime;

            minutes = Mathf.RoundToInt(timer) / 60;
            seconds = Mathf.RoundToInt(timer) - (minutes * 60);
            if (minutes >= 24)
            {
                days += 1;
                timer -= 600;
                DayChange.Invoke();
            }

            updateDisplay();
            UpdateIndicators();
        }

        void updateDisplay() 
        {
            secondsSpot.text = seconds.ToString();
            minuteSpot.text = minutes.ToString();
            daySpot.text = "Day: " + days.ToString();
            if (timePassage == Passage.AM)
            {
                AmPmSpot.text = "AM";
            }
            else if (timePassage == Passage.PM)
            {
                AmPmSpot.text = "PM";
            }
        }

        void UpdateIndicators() 
        {
            if (minutes <= 6)
            {
                timeCycle = Cycle.Night;
                CycleChange.Invoke();
            }
            else if (minutes > 6 && minutes <= 18)
            {
                timeCycle = Cycle.Day;
                CycleChange.Invoke();
            }
            else if (minutes > 18)
            {
                timeCycle = Cycle.Night;
                CycleChange.Invoke();
            }
        }

        public float getRawTime() 
        {
            float rawTime = timer + (days * 600);
            return rawTime;
        }

        public int GetMinutes() { return minutes; }
        public int GetSeconds() { return seconds; }
    }
}

