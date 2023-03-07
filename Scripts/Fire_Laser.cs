using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Asteroid
{
    public class Fire_Laser : MonoBehaviour
    {
        [SerializeField] GameObject laser;
        [SerializeField] GameObject rocket;
        [SerializeField] GameObject knockBackShield;
        [SerializeField] ParticleSystem blast;
        [SerializeField] float _fireRate = 0.5f;
        [SerializeField] float _canFire = -1f;
        [SerializeField] float _rocketCanFire = -1f;
        [SerializeField] float _shotGunCanFire = -1f;
        [SerializeField] float _shotgunFireSpeed;
        [SerializeField] float shotGunAngle = 0;
        [SerializeField] float _rocketFireSpeed;
        [SerializeField] float _offsetShield;
        public bool _canBounce;
        public bool _shotGun = false;
        public bool _machineGun = false;
        public bool _regularGun = false;
        public bool _rocket = true;
        public float _weaponDamage;
        float orgShotgunAngle;
        public bool _overheated;
        GameObject Player;
        SpriteRenderer playerSprite;
        bool _canShield = true;
        ColorChange changeColor;

        void Start()
        {
            Player = GameObject.FindWithTag("PlayerChild");
            orgShotgunAngle = shotGunAngle;
            playerSprite = GetComponent<SpriteRenderer>();
            changeColor = gameObject.GetComponentInParent<ColorChange>();
        }

        void Update()
        {
            WeaponDamage();
            Shoot();
            Overheat();
        }

        void Shoot()
        {
            if (Input.GetKeyDown(KeyCode.Mouse0) && Time.time > _canFire && _regularGun)
            {
                _canFire = Time.time + _fireRate;
                Instantiate(laser, transform.position + transform.up, Player.transform.rotation);
            }

            if (Input.GetKeyDown(KeyCode.Mouse0) && Time.time > _shotGunCanFire && _shotGun)
            {
                _shotGunCanFire = Time.time + _fireRate + _shotgunFireSpeed;
                Quaternion storePlayerRotation;
                for (int i = 0; i < 4; i++)
                {
                    storePlayerRotation = Player.transform.rotation;
                    Instantiate(laser, transform.position + transform.up, storePlayerRotation *= Quaternion.Euler(0,0,shotGunAngle));
                    shotGunAngle += 10;
                }
                shotGunAngle = orgShotgunAngle;
            }

            if (Input.GetKey(KeyCode.Mouse0) && Time.time > _canFire && !_overheated && _machineGun)
            {                            
                _canFire = Time.time + _fireRate;                
                StartCoroutine(MachineGunFire(.1f));
            } 

            if (Input.GetKeyDown(KeyCode.Mouse0) && Time.time > _rocketCanFire && _rocket)
            {
                _rocketCanFire = Time.time + _fireRate + _rocketFireSpeed;
                if (!GameObject.FindGameObjectWithTag("Rocket"))
                { 
                    Instantiate(rocket, transform.position + transform.up, Player.transform.rotation);
                }
            }
            else if(Input.GetKeyDown(KeyCode.Mouse0) &&  _rocket)
            {
                if (GameObject.FindGameObjectWithTag("Rocket"))
                {
                    StartCoroutine(RocketExplosion());
                }                         
            }
            if (Input.GetKeyDown(KeyCode.Mouse1) && _canShield == true)
            {
                _canShield = false;
                StartCoroutine(EnableShield());
            }
        }


        void WeaponDamage()
        {
            if (_machineGun || _canBounce)
            {
                _weaponDamage = .25f;
            }
            else if (_rocket)
            {
                _weaponDamage = 5f;
            }
            else if(_regularGun)
            {
                _weaponDamage = 3.5f;
            }
            else
            {
                _weaponDamage = 1f;
            }
        }

        void Overheat()
        {
            if (playerSprite.color.g <= 0)
            {
                _overheated = true;
            }
            else if (playerSprite.color.g >= 1)
            {
                _overheated = false;
            }
            changeColor.HoldButtonToSlowlyChangeSpriteRendererColorDarker(gameObject, Input.GetKey(KeyCode.Mouse0) && !_overheated && _machineGun && playerSprite.color.g > 0);
            changeColor.SlowlyChangeSpriteRendererColorBrighter(gameObject,!Input.GetKey(KeyCode.Mouse0) || _overheated, _machineGun || _regularGun || _shotGun || _rocket, playerSprite.color.g < 1, !changeColor.isRunning);
        }


        IEnumerator MachineGunFire(float fireRate)
        {
            for(int i = 0; i < 5; i++)
            {
                Instantiate(laser, transform.position + transform.up, Player.transform.rotation);
                yield return new WaitForSeconds(fireRate);
            }
        }

        IEnumerator RocketExplosion()
        {
            Instantiate(blast, GameObject.FindGameObjectWithTag("Rocket").transform.position, Quaternion.identity);
            Destroy(GameObject.FindGameObjectWithTag("Rocket"));
            yield return new WaitForSeconds(.5f);
            Destroy(GameObject.FindGameObjectWithTag("Rocket Explosion"));
        }

        IEnumerator EnableShield()
        {
            Instantiate(knockBackShield, transform.position + (-transform.right * _offsetShield), Player.transform.rotation * Quaternion.Euler(0,0,90));
            yield return new WaitForSeconds(3f);
            _canShield = true;
        }
    }
}