using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cardinal.Analyser.UI
{
    public class AnalysisDisplay : MonoBehaviour
    {
        public GameObject DisplayWindow;

        [Header("Display Elements")]
        public HexadTypeDisplay Philanthropist;
        public HexadTypeDisplay Socialiser;
        public HexadTypeDisplay FreeSpirit;
        public HexadTypeDisplay Achiever;
        public HexadTypeDisplay Disruptor;
        public HexadTypeDisplay Player;

        Analyser Analyser;

        private void Start()
        {
            Analyser = Analyser.Instance;
        }

        private void Update()
        {
            //Why unity no support dicts in editor :(
            Philanthropist.TypeValue.text = Analyser.PhilanthropistValue.ToString();
            Socialiser.TypeValue.text = Analyser.SocialiserValue.ToString();
            FreeSpirit.TypeValue.text = Analyser.FreeSpiritValue.ToString();
            Achiever.TypeValue.text = Analyser.AchieverValue.ToString();
            Disruptor.TypeValue.text = Analyser.DisruptorValue.ToString();
            Player.TypeValue.text = Analyser.PlayerValue.ToString();
        }

        public void ToggleDisplayWindow() 
        {
            DisplayWindow.SetActive(!DisplayWindow.activeSelf);
        }
    }
}

