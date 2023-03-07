using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Asteroid
{
    public class SpawnPowerUps : MonoBehaviour
    {
        [SerializeField] GameObject weaponToSpawn;
        GameObject checkIfExists;
        private Vector2 _screenBoundaries;
        private float respawnTimer = 3f;
        public static float timeHit;
        private float _powerUpHeight;
        private float _powerUpWidth;
        // Start is called before the first frame update
        void Start()
        {
            _screenBoundaries = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
            _powerUpHeight = weaponToSpawn.transform.GetComponentInChildren<SpriteRenderer>().bounds.size.y;
            _powerUpWidth = weaponToSpawn.transform.GetComponentInChildren<SpriteRenderer>().bounds.size.x;
        }

        // Update is called once per frame
        void Update()
        {
            checkIfExists = GameObject.FindGameObjectWithTag("PowerUps");
            if (Time.time > timeHit + respawnTimer && checkIfExists == null)
            {
                Instantiate(weaponToSpawn, new Vector3(Random.Range(-_screenBoundaries.x + (_powerUpWidth / 2), _screenBoundaries.x - (_powerUpWidth / 2)), Random.Range(-_screenBoundaries.y + (_powerUpHeight / 2), _screenBoundaries.y - (_powerUpHeight / 2))), Quaternion.identity);
            } else if (checkIfExists != null)
            {
                timeHit = Time.time;
                Destroy(checkIfExists, 3f);
            }
        }
    }
}
