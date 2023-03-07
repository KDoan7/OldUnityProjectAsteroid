using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace Asteroid
{
    public class PlayerHP : MonoBehaviour
    {
        [SerializeField] Image fill;
        [SerializeField] Text textTimer;
        EndGame endGame;
        public Slider HP;
        public Text HPText;
        public Slider Speed;
        private float seconds;
        private int minutes;
        private float timer = 0f;
        bool colorCanChange = true;
        private int _maxHP;

        void Start()
        {
            endGame = gameObject.AddComponent<EndGame>();
            minutes = 0;
            seconds = 50;
            _maxHP = (int)HP.value;
        }

        void Update()
        {
            if (HP != null && HPText != null)
            {
                HPValues();
                TimerValue();
            }
        }

        void HPValues()
        {
            //switch (HP.value)
            //{
            //    case float n when (n <= 0):
            //        StartCoroutine(endGame.GameOver());
            //        break;
            //    case 3:
            //        fill.color = Color.green;
            //        break;
            //    case 2:
            //        if (timer > 1f && colorCanChange == false)
            //        {
            //            timer = 0f;
            //            colorCanChange = true;
            //        }
            //        fill.color = Color.Lerp(Color.green, Color.yellow, timer);
            //        timer += Time.deltaTime * 1f;
            //        break;
            //    case 1:
            //        if (timer > 1f && colorCanChange == true)
            //        {
            //            timer = 0f;
            //            colorCanChange = false;
            //        }
            //        fill.color = Color.Lerp(Color.yellow, Color.red, timer);
            //        timer += Time.deltaTime * 1f;
            //        break;
            //}

            if((int)HP.value % _maxHP >= 6 || (int)HP.value == _maxHP)
            {
                fill.color = Color.green;
            } else if((int)HP.value % _maxHP >= 3 && (int)HP.value % _maxHP < 6)
            {
                fill.color = Color.Lerp(Color.green, Color.yellow, timer);
                timer += Time.deltaTime * 1f;
            } else if((int) HP.value % _maxHP < 3)
            {
                fill.color = Color.Lerp(Color.yellow, Color.red, timer);
                timer += Time.deltaTime * 1f;
            }
        }

        void TimerValue()
        {
            HPText.text = "HP :" + HP.value;
            seconds += Time.deltaTime;

            if ((int)seconds / 59 == 1)
            {
                minutes++;
                seconds = 0;
            }

            textTimer.text = string.Format("Time: {0:00}:{1:00}", minutes, seconds);
        }
    }
}