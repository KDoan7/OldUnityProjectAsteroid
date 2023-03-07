using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Asteroid
{
    public class Scaler : MonoBehaviour
    {
        [SerializeField] float _scale;
        float _width;
        float _height;
        // Start is called before the first frame update
        void Start()
        {

            Vector2 topRightCorner = new Vector2(1, 1);
            _width = Camera.main.ViewportToWorldPoint(topRightCorner).x * _scale;
            _height = Camera.main.ViewportToWorldPoint(topRightCorner).y * _scale;
            transform.localScale = Vector3.one * _width * _height;

        }
    }
}