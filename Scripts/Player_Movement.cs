using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Asteroid
{
    public class Player_Movement : MonoBehaviour
    {
        [SerializeField] private float moveSpeed;
        [SerializeField] private float _speedBoostDuration;
        [SerializeField] private float _speedBoostRecharge;
        [SerializeField] GameObject _deathExplosion;
        private Vector2 _screenBounds;
        private float _playerWidth;
        private float _playerHeight;
        ColorChange changeColors;
        private Canvas getHpValue;
        float orgSpeed;

        void Start()
        {
            transform.position = new Vector3(0, 0, 0);
            _screenBounds = Find_Screen_Boundaries._screenBoundary;
            _playerHeight = transform.GetComponentInChildren<SpriteRenderer>().bounds.size.y;
            _playerWidth = transform.GetComponentInChildren<SpriteRenderer>().bounds.size.x;
            getHpValue = FindObjectOfType<Canvas>();
            changeColors = GetComponent<ColorChange>();
            orgSpeed = moveSpeed;
        }

        // Update is called once per frame
        void Update()
        {
            Death();
            Movement();
            SpeedBoost();
        }

        void Movement()
        {
            float userInputH = Input.GetAxis("Horizontal");

            float userInputV = Input.GetAxis("Vertical");

            transform.Translate(new Vector3(userInputH, userInputV, 0) * moveSpeed * Time.deltaTime);

            if (transform.position.x >= _screenBounds.x - (_playerWidth / 2)) //8.35f
            {
                transform.position = new Vector2(_screenBounds.x - (_playerWidth / 2), transform.position.y);
            }
            else if (transform.position.x <= -_screenBounds.x + (_playerWidth / 2))
            {
                transform.position = new Vector2(-_screenBounds.x + (_playerWidth / 2), transform.position.y);

            }

            if (transform.position.y >= _screenBounds.y - (_playerHeight / 2)) //4.4f
            {
                transform.position = new Vector2(transform.position.x, _screenBounds.y - (_playerWidth / 2));
            }
            else if (transform.position.y <= -_screenBounds.y + (_playerWidth / 2))
            {
                transform.position = new Vector2(transform.position.x, -_screenBounds.y + (_playerWidth / 2));
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!changeColors.isRunning)
            {
                if (other.gameObject.tag == "Enemy" || other.gameObject.tag == "Tank" || other.gameObject.tag == "EnemyShot")
                {
                    if(other.gameObject.tag == "Enemy" || other.gameObject.tag == "EnemyShot")
                    {
                        getHpValue.GetComponent<PlayerHP>().HP.value -= 1f;
                    } 
                    else if(other.gameObject.tag == "Tank")
                    {
                        getHpValue.GetComponent<PlayerHP>().HP.value -= 3f;
                    }
                   
                    StartCoroutine(changeColors.FlashingSpriteRendererColor(gameObject, Color.red));
                }
            }
        }

        private void Death()
        {
            if(getHpValue.GetComponent<PlayerHP>().HP.value <= 0)
            {
                Instantiate(_deathExplosion, transform.position, Quaternion.identity);
                Destroy(gameObject);
            }
        }
        
        void SpeedBoost()
        {
            if (Input.GetKey(KeyCode.Space) && getHpValue.GetComponentInChildren<PlayerHP>().Speed.value > 0)
            {
                getHpValue.GetComponentInChildren<PlayerHP>().Speed.value -= _speedBoostDuration * Time.deltaTime;
                moveSpeed = 15f;
            }
            else if (!Input.GetKey(KeyCode.Space) && getHpValue.GetComponentInChildren<PlayerHP>().Speed.value < 100)
            {
                getHpValue.GetComponentInChildren<PlayerHP>().Speed.value += _speedBoostRecharge * Time.deltaTime;
                moveSpeed = orgSpeed;
            }
            else
            {
                moveSpeed = orgSpeed;
            }
        }
    }
}
