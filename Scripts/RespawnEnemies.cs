using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Asteroid
{
    public class RespawnEnemies : MonoBehaviour
    {
        [SerializeField] GameObject objectToSpawn;
        [SerializeField] GameObject tankSpawn;
        [SerializeField] float _respawnTime = 5f;
        [SerializeField] int _maxSpawn;
        [HideInInspector] public int _howManyAlive;
        private Vector2 _screenBoundary;
        private float spawnTime = -1f;
        int ranNum;
        int tankSpawnChance;

        void Start()
        {
            _screenBoundary = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
            StartCoroutine(FasterSpawningOverTime());
        }

        void Update()
        {
            ranNum = Random.Range(1, 5);
            tankSpawnChance = Random.Range(1, 20);
            SpawnEnemy();
        }

        void SpawnEnemy()
        {
            if (Time.time > spawnTime && _howManyAlive < _maxSpawn)
            {
                spawnTime = Time.time + _respawnTime;
                switch (ranNum)
                {
                    case 1:
                        if (tankSpawnChance == 1)
                        {
                            Instantiate(tankSpawn, new Vector3(Random.Range(-(_screenBoundary.x - (_screenBoundary.y / 2)), _screenBoundary.x + (_screenBoundary.y / 2)), _screenBoundary.y + (_screenBoundary.y / 2)), Quaternion.identity);
                            _howManyAlive++;
                        }
                        else
                        {
                            Instantiate(objectToSpawn, new Vector3(Random.Range(-(_screenBoundary.x - (_screenBoundary.y / 2)), _screenBoundary.x + (_screenBoundary.y / 2)), _screenBoundary.y + (_screenBoundary.y / 2)), Quaternion.identity);
                            _howManyAlive++;
                        }
                        break;
                    case 2:
                        if (tankSpawnChance == 1)
                        {
                            Instantiate(tankSpawn, new Vector3(Random.Range(-(_screenBoundary.x - (_screenBoundary.y / 2)), _screenBoundary.x + (_screenBoundary.y / 2)), -_screenBoundary.y - (_screenBoundary.y / 2)), Quaternion.identity);
                            _howManyAlive++;
                        }
                        else
                        {
                            Instantiate(objectToSpawn, new Vector3(Random.Range(-(_screenBoundary.x - (_screenBoundary.y / 2)), _screenBoundary.x + (_screenBoundary.y / 2)), -_screenBoundary.y - (_screenBoundary.y / 2)), Quaternion.identity);
                            _howManyAlive++;
                        }
                        break;
                    case 3:
                        if (tankSpawnChance == 1)
                        {
                            Instantiate(tankSpawn, new Vector3(_screenBoundary.x + (_screenBoundary.y / 2), Random.Range(-(_screenBoundary.y + (_screenBoundary.y / 2)), _screenBoundary.y + (_screenBoundary.y / 2))), Quaternion.identity);
                            _howManyAlive++;
                        }
                        else
                        {
                            Instantiate(objectToSpawn, new Vector3(_screenBoundary.x + (_screenBoundary.y / 2), Random.Range(-(_screenBoundary.y + (_screenBoundary.y / 2)), _screenBoundary.y + (_screenBoundary.y / 2))), Quaternion.identity);
                            _howManyAlive++;
                        }
                        break;
                    case 4:
                        if (tankSpawnChance == 1)
                        {
                            Instantiate(tankSpawn, new Vector3(-_screenBoundary.x - (_screenBoundary.y / 2), Random.Range(-(_screenBoundary.y + (_screenBoundary.y / 2)), _screenBoundary.y + (_screenBoundary.y / 2))), Quaternion.identity);
                            _howManyAlive++;
                        }
                        else
                        {
                            Instantiate(objectToSpawn, new Vector3(-_screenBoundary.x - (_screenBoundary.y / 2), Random.Range(-(_screenBoundary.y + (_screenBoundary.y / 2)), _screenBoundary.y + (_screenBoundary.y / 2))), Quaternion.identity);
                            _howManyAlive++;
                        }
                        break;
                }
            }
        }

        IEnumerator FasterSpawningOverTime()
        {
            while (_respawnTime > 0.35f)
            {
                yield return new WaitForSeconds(10f);
                _respawnTime -= .1f;
            }
        }
    }
}
