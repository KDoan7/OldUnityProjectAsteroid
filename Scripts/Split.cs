using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Asteroid
{
    public class Split : MonoBehaviour
    {
        [SerializeField] float speed;
        [SerializeField] float laserSpeed;
        [SerializeField] Canvas playerHp;

        void Start()
        {
            playerHp = FindObjectOfType<Canvas>();
            
            if(gameObject.tag == "Enemy")
            {
                Destroy(gameObject, 2.5f);
            } 
            else if(gameObject.tag == "Laser")
            {
                Destroy(gameObject, 1.5f);
            }
        }

        void Update()
        {
            if(gameObject.tag == "Enemy")
            {
                transform.Translate(Vector2.up * speed * Time.deltaTime);
            } 
            else if(gameObject.tag == "Laser")
            {
                transform.Translate(Vector2.up * laserSpeed * Time.deltaTime);
            }
            
        }

        private void OnTriggerEnter(Collider other)
        {
            if (gameObject.tag == "Enemy")
            {
                if (other.gameObject.tag == "Player")
                {
                    Destroy(gameObject);
                    playerHp.GetComponent<PlayerHP>().HP.value -= 1f;
                }
                else if (other.gameObject.tag == "Laser")
                {
                    Destroy(gameObject);
                }
            } 
            else if (gameObject.tag == "Laser")
            {
                Destroy(gameObject);
            }
        }
    }
}
