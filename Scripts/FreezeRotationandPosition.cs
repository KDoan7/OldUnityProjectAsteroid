using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezeRotationandPosition : MonoBehaviour
{
    [SerializeField] GameObject _player;
    [SerializeField] float _offSetY;
    [SerializeField] float _offSetX;
    Quaternion rotation;
    // Start is called before the first frame update
    void Awake()
    {
        rotation = transform.rotation;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.rotation = rotation;
        transform.position = _player.transform.position + new Vector3(_offSetX,_offSetY);
    }
}
