using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Asteroid
{
    public class EndGame : MonoBehaviour
    {
        public GameObject test;
        void Update()
        {
            EscapeToEndGame();
        }

        void EscapeToEndGame()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Application.Quit();
            }
        }

        public IEnumerator GameOver()
        {
            yield return new WaitForSeconds(3f);
            //SceneManager.LoadScene(0);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
