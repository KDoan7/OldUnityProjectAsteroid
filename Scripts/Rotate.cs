using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Asteroid
{
    public class Rotate : MonoBehaviour
    {
        // Update is called once per frame
        void Update()
        {
            
            Follow();

        }

        public void Follow()
        {
            Vector3 mousePosition = Input.mousePosition;
            mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
            Vector2 direction = new Vector2(mousePosition.x  - transform.position.x, mousePosition.y - transform.position.y);
            
            transform.up = direction;
        }
    }
}