using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn_Small : MonoBehaviour
{
    [SerializeField] GameObject _enemyToSpawn;
    [SerializeField] float _respawnTime = 5f;
    private Vector2 _screenBoundary;
    private float _spawnTime = -1f;
    private int _ranNum;
    private int _spawnAngle;
    private float _topBotScreenTracking;
    private float _leftRightScreenTracking;
    // Start is called before the first frame update
    void Start()
    {
        _screenBoundary = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
    }

    // Update is called once per frame
    void Update()
    {
        _ranNum = Random.Range(1, 5);
        _topBotScreenTracking = Random.Range(-(_screenBoundary.x - (_screenBoundary.y / 2)), _screenBoundary.x + (_screenBoundary.y / 2));
        _leftRightScreenTracking = Random.Range(-(_screenBoundary.y + (_screenBoundary.y / 2)), _screenBoundary.y + (_screenBoundary.y / 2));

        if (Time.time > _spawnTime)
        {
            _spawnTime = Time.time + _respawnTime;
            switch (_ranNum)
            {
                case 1: //Top of Screen
                    {
                        if(_topBotScreenTracking >= 9)
                        {
                            _spawnAngle = Random.Range(-190, -250);

                        } else if(_topBotScreenTracking <= -9)
                        {
                            _spawnAngle = Random.Range(-110, -170);
                        }
                        else
                        {
                            _spawnAngle = Random.Range(160, 210);
                        }
                        Instantiate(_enemyToSpawn, new Vector3(_topBotScreenTracking, _screenBoundary.y + (_screenBoundary.y / 2)), Quaternion.Euler(0,0, _spawnAngle));
                    }
                    break;
                case 2: //Bottom of Screen
                    {
                        if (_topBotScreenTracking >= 9)
                        {
                            _spawnAngle = Random.Range(-280, -330);

                        }
                        else if (_topBotScreenTracking <= -9)
                        {
                            _spawnAngle = Random.Range(-40, -80);
                        }
                        else
                        {
                            _spawnAngle = Random.Range(-30, 45);
                        }
                        Instantiate(_enemyToSpawn, new Vector3(_topBotScreenTracking, -_screenBoundary.y - (_screenBoundary.y / 2)), Quaternion.Euler(0,0,_spawnAngle));
                    }
                    break;
                case 3: //Right of Screen
                    {
                        if(_leftRightScreenTracking >= 5)
                        {
                            _spawnAngle = Random.Range(-210, -250);
                        } 
                        else if(_leftRightScreenTracking <= -5)
                        {
                            _spawnAngle = Random.Range(-290, -340);
                        }
                        else
                        {
                            _spawnAngle = Random.Range(-240, -300);
                        }
                        Instantiate(_enemyToSpawn, new Vector3(_screenBoundary.x + (_screenBoundary.y / 2),_leftRightScreenTracking), Quaternion.Euler(0,0,_spawnAngle));
                    }
                    break;
                case 4: //Left of Screen
                    {
                        if (_leftRightScreenTracking >= 5)
                        {
                            _spawnAngle = Random.Range(-110, -150);
                        }
                        else if (_leftRightScreenTracking <= -5)
                        {
                            _spawnAngle = Random.Range(-20, -70);
                        }
                        else
                        {
                            _spawnAngle = Random.Range(-60, -120);
                        }
                        Instantiate(_enemyToSpawn, new Vector3(-_screenBoundary.x - (_screenBoundary.y / 2), _leftRightScreenTracking), Quaternion.Euler(0,0,_spawnAngle));
                    }
                    break;
            }
        }
    }
}
