using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Asteroid
{
    public class Weapon_Swap : MonoBehaviour
    {
        [SerializeField] Sprite regularGun;
        [SerializeField] Sprite shotGun;
        [SerializeField] Sprite machineGun;
        [SerializeField] Sprite rocket;
        int ranNum2;
        Fire_Laser firelaser;
        // Start is called before the first frame update
        void Start()
        {
            firelaser = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Fire_Laser>();
            ranNum2 = Random.Range(1, 5);
            SpriteImage();
        }

        // Update is called once per frame
        void Update()
        {
            //WeaponSwap();
        }

        void WeaponSwap()
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                firelaser._rocket = false;
                firelaser._shotGun = false;
                firelaser._machineGun = false;
                firelaser._regularGun = true;
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                firelaser._rocket = false;
                firelaser._machineGun = false;
                firelaser._regularGun = false;
                firelaser._shotGun = true;
            }
            else if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                firelaser._rocket = false;
                firelaser._regularGun = false;
                firelaser._shotGun = false;
                firelaser._machineGun = true;
            }
            else if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                firelaser._regularGun = false;
                firelaser._shotGun = false;
                firelaser._machineGun = false;
                firelaser._rocket = true;
            }
        }

        void SpriteImage()
        {
            switch (ranNum2)
            {
                case 1:
                    gameObject.GetComponentInChildren<SpriteRenderer>().sprite = regularGun;
                    break;
                case 2:
                    gameObject.GetComponentInChildren<SpriteRenderer>().sprite = shotGun;
                    break;
                case 3:
                    gameObject.GetComponentInChildren<SpriteRenderer>().sprite = machineGun;
                    break;
                case 4:
                    gameObject.GetComponentInChildren<SpriteRenderer>().sprite = rocket;
                    break;
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == "Player")
            {
                SpawnPowerUps.timeHit = Time.time;
                if (ranNum2 == 1)
                {
                    firelaser._rocket = false;
                    firelaser._shotGun = false;
                    firelaser._machineGun = false;
                    firelaser._regularGun = true;
                    Destroy(gameObject);
                }
                else if (ranNum2 == 2)
                {
                    firelaser._rocket = false;
                    firelaser._machineGun = false;
                    firelaser._regularGun = false;
                    firelaser._shotGun = true;
                    Destroy(gameObject);
                }
                else if (ranNum2 == 3)
                {
                    firelaser._rocket = false;
                    firelaser._regularGun = false;
                    firelaser._shotGun = false;
                    firelaser._machineGun = true;
                    Destroy(gameObject);
                }
                else if (ranNum2 == 4)
                {
                    firelaser._regularGun = false;
                    firelaser._shotGun = false;
                    firelaser._machineGun = false;
                    firelaser._rocket = true;
                    Destroy(gameObject);
                }
            }
        }
    }
}

