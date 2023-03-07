using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Asteroid
{
    public class Projectiles : MonoBehaviour
    {
        [SerializeField] float speed;
        [SerializeField] float laserSpeed;
        [SerializeField] Canvas playerHp;
        Fire_Laser fireLaser;
        bool _canBounce;

        void Start()
        {
            playerHp = FindObjectOfType<Canvas>();
            fireLaser = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Fire_Laser>();
            _canBounce = fireLaser._canBounce;

            if(gameObject.tag == "EnemyShot")
            {
                gameObject.GetComponent<SphereCollider>().enabled = false;
                StartCoroutine(SpawnInvincibility(.5f));
                Destroy(gameObject, 8f);
            }
            else if(gameObject.tag == "Laser")
            {
                if (_canBounce)
                {
                    Destroy(gameObject, 8f);
                }
                else if (fireLaser._shotGun)
                {
                    Destroy(gameObject, .5f);
                }
                else
                {
                    Destroy(gameObject, 2.5f);
                }
            }
        }

        void Update()
        {
            if(gameObject.tag == "EnemyShot")
            {
                transform.Translate(Vector2.up * speed * Time.deltaTime);
            } 
            else if(gameObject.tag == "Laser" || gameObject.tag == "Rocket")
            {
                transform.Translate(Vector2.up * laserSpeed * Time.deltaTime);
            } 
            
        }

        private void OnTriggerEnter(Collider other)
        {
            if (gameObject.tag == "EnemyShot")
            {
                if (other.gameObject.tag == "Player" || other.gameObject.tag == "Laser" || other.gameObject.tag == "Rocket Explosion")
                {
                    Destroy(gameObject);
                }
            } 
            else if (gameObject.tag == "Laser")
            {
                if (fireLaser._shotGun)
                {
                    Vector3 moveDirection = gameObject.transform.position - other.gameObject.transform.position;
                    other.attachedRigidbody.AddForce(moveDirection.normalized * -100f);
                }
                if (!_canBounce)
                {
                    Destroy(gameObject);
                }
                else if (_canBounce)
                {
                    if(other.gameObject.name == "TopWall" || other.gameObject.name == "BotWall")
                    {
                        gameObject.transform.Rotate(0, 0, 180);
                        gameObject.transform.rotation = Quaternion.Inverse(gameObject.transform.rotation);
                        _canBounce = false;
                    }
                    else if(other.gameObject.tag == "EnemyToporBot")
                    {
                        gameObject.transform.Rotate(0, 0, 180);
                        gameObject.transform.rotation = Quaternion.Inverse(gameObject.transform.rotation);
                    }
                    else if(other.gameObject.tag == "Wall")
                    {
                        gameObject.transform.rotation = Quaternion.Inverse(gameObject.transform.rotation);
                        _canBounce = false;
                    }
                    else
                    {
                        gameObject.transform.rotation = Quaternion.Inverse(gameObject.transform.rotation);
                    }
                }
            }
        }

        IEnumerator SpawnInvincibility(float invlunTime)
        {
            yield return new WaitForSeconds(invlunTime);
            gameObject.GetComponent<SphereCollider>().enabled = true;
        }
    }
}
