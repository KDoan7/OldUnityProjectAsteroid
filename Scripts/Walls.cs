using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Asteroid
{
    public class Walls : MonoBehaviour
    {
        [SerializeField] float _scale;
        [SerializeField] bool _horizontal;
        [SerializeField] bool _topWall;
        [SerializeField] bool _leftWall;
        Vector2 _screenBoundary;
        float _width;
        float _height;
        // Start is called before the first frame update
        void Start()
        {
            if (_horizontal)
            {
                _screenBoundary = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
                _width = Camera.main.ViewportToWorldPoint(new Vector2(1, 1)).x * _scale;
                transform.localScale = new Vector3(_width, .5f);
                if (_topWall)
                {
                    transform.position = new Vector3(0, _screenBoundary.y + 0.25f);
                }
                else
                {
                    transform.position = new Vector3(0, -_screenBoundary.y - 0.25f);
                }
            }
            else
            {
                _screenBoundary = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
                _height = Camera.main.ViewportToWorldPoint(new Vector2(1, 1)).y * _scale;
                transform.localScale = new Vector3(.5f, _height);
                if (_leftWall)
                {
                    transform.position = new Vector3(-_screenBoundary.x - 0.25f, 0);
                }
                else
                {
                    transform.position = new Vector3(_screenBoundary.x + 0.25f, 0);
                }
            }
        }
    }
}
