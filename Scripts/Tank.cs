using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Asteroid
{
    public class Tank : MonoBehaviour
    {
        [SerializeField] internal float _tankSpeed;
        [SerializeField] public float _tankHP;
        [SerializeField] float _distanceToKnockback;
        GameObject _tankFollowThis;
        bool _shouldGoStraight = true;
        bool _multipleCollisionColorChange;
        ColorChange changeColors;

        void Start()
        {
            _tankFollowThis = GameObject.FindGameObjectWithTag("Player");
            changeColors = GetComponent<ColorChange>();
            InvokeRepeating("AllowColorChange", 0, 0.05f);
            StartCoroutine(ChangeToFollow());
            StartCoroutine(EnableCollider());
        }

        void Update()
        {
            Death();
            if (_shouldGoStraight)
            {
                transform.Translate(Vector2.up * _tankSpeed * Time.deltaTime);
            }
            if (!_shouldGoStraight)
            {
                Move();
            }
        }

        void Move()
        {
            transform.LookAt(_tankFollowThis.transform);
            transform.position += transform.forward * _tankSpeed * Time.deltaTime;
            transform.Translate(Vector3.forward * _tankSpeed * Time.deltaTime);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == "Player")
            {
                Destroy(gameObject);
            }

            if (other.gameObject.tag == "Laser" || other.gameObject.tag == "Rocket Explosion")
            {
                    _tankHP -= _tankFollowThis.GetComponentInChildren<Fire_Laser>()._weaponDamage;

                    if (!changeColors.enemyIsRunning && !_multipleCollisionColorChange)
                    {
                        _multipleCollisionColorChange = true;
                        StartCoroutine(changeColors.FlashingSpriteRendererColor(gameObject, Color.red));
                    }           
            }

            if(other.gameObject.tag == "Enemy" || other.gameObject.tag == "Tank")
            {
                Vector3 moveDirection = gameObject.transform.position - other.gameObject.transform.position;
                other.attachedRigidbody.AddForce(moveDirection.normalized * -_distanceToKnockback);
            }
        }

        void Death()
        {
            if (_tankHP <= 0)
            {
                changeColors.enemyIsRunning = false;
                Destroy(gameObject);
            }
        }

        private void AllowColorChange()
        {
            if (_multipleCollisionColorChange == true)
            {
                _multipleCollisionColorChange = false;
            }
        }

        IEnumerator ChangeToFollow()
        {
            yield return new WaitForSeconds(2.5f);
            gameObject.GetComponentInChildren<SpriteRenderer>().transform.rotation *= Quaternion.Euler(0, -90, 0);
            _shouldGoStraight = false;
        }

        IEnumerator EnableCollider()
        {
            yield return new WaitForSeconds(2.5f);
            gameObject.GetComponent<SphereCollider>().enabled = true;
        }
    }
}
