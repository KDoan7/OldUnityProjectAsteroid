using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Asteroid
{
    public class Enemy : MonoBehaviour
    {
        [SerializeField] internal float _speed;
        [SerializeField] public float HP;
        [SerializeField] GameObject smallOnes;
        [SerializeField] float _distanceToKnockback;
        GameObject followThis;
        ColorChange changeColors;
        bool _preventOverSpawn;
        public bool _multipleCollisionColorChange;

        void Start()
        {
            followThis = GameObject.FindGameObjectWithTag("Player");
            changeColors = GetComponent<ColorChange>();
            InvokeRepeating("AllowColorChange", 0, 0.05f);
        }

        void Update()
        {
            Death();
            Move();
        }

        void Move()
        {
            transform.LookAt(followThis.transform);
            transform.position += transform.forward * _speed * Time.deltaTime;
            transform.Translate(Vector3.forward * _speed * Time.deltaTime);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == "Player")
            {
                Destroy(gameObject);
            }

            if (other.gameObject.tag == "Laser" || other.gameObject.tag == "Rocket Explosion")
            {
                HP -= followThis.GetComponentInChildren<Fire_Laser>()._weaponDamage;

                if (!changeColors.enemyIsRunning && !_multipleCollisionColorChange)
                {
                    _multipleCollisionColorChange = true;                     
                    StartCoroutine(changeColors.FlashingSpriteRendererColor(gameObject, Color.blue));
                }
            }

            if(other.gameObject.tag == "Enemy" || other.gameObject.tag == "Tank")
            {
                Vector3 moveDirection = gameObject.transform.position - other.gameObject.transform.position;
                other.attachedRigidbody.AddForce(moveDirection.normalized * -_distanceToKnockback);

            }
        }

        private void Death()
        {
            if (HP <= 0)
            {
                if(gameObject.tag == "Enemy")
                {
                    changeColors.enemyIsRunning = false;
                    GameObject.FindGameObjectWithTag("MainCamera").GetComponent<RespawnEnemies>()._howManyAlive--;
                    Destroy(gameObject);
                }
                else if (gameObject.tag == "Tank")
                {
                    changeColors.enemyIsRunning = false;
                    GameObject.FindGameObjectWithTag("MainCamera").GetComponent<RespawnEnemies>()._howManyAlive--;
                    Split();
                    Destroy(gameObject);
                }
            }
        }
        private void Split()
        {
            int ranNum = (int)Random.Range(1f, 5f);
            float z = 0f;

            switch (ranNum)
            {
                case 1:
                    z += 90f;
                    for (int i = 0; i < 4; i++)
                    {
                        Instantiate(smallOnes, gameObject.transform.position, Quaternion.Euler(0f, 0, z));
                        z += 90f;
                    }
                    break;

                case 2:
                    z += 45f;
                    for (int i = 0; i < 8; i++)
                    {
                        Instantiate(smallOnes, gameObject.transform.position, Quaternion.Euler(0f, 0, z));
                        z += 45f;
                    }
                    break;

                case 3:
                    z += 45f;
                    for (int i = 0; i < 4; i++)
                    {
                        Instantiate(smallOnes, gameObject.transform.position, Quaternion.Euler(0f, 0, z));
                        z += 90f;
                    }
                    break;

                case 4:
                    for (int i = 0; i < 3; i++)
                    {
                        Instantiate(smallOnes, gameObject.transform.position, Quaternion.Euler(0f, 0, z));
                        z += 135f;
                    }
                    break;
            }
        }

        private void AllowColorChange()
        {
            if(_multipleCollisionColorChange == true)
            {
                _multipleCollisionColorChange = false;
            }
        }
    }
}
