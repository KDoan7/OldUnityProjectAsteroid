using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Asteroid
{
    public class Find_Screen_Boundaries : MonoBehaviour
    {
        internal static Vector2 _screenBoundary;
        // Start is called before the first frame update
        void Start()
        {
            _screenBoundary = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        }
    }
}
