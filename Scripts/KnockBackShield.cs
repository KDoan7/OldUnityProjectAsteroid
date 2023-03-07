using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Asteroid
{
    public class KnockBackShield : MonoBehaviour
    {
        [SerializeField] GameObject player;
        [SerializeField] float speed;
        [SerializeField] float knockBack;
        [SerializeField] float _size;
        Vector3 movedirection;

        // Start is called before the first frame update
        void Start()
        {
            player = GameObject.FindGameObjectWithTag("Player");
            gameObject.transform.SetParent(player.transform);
            Destroy(gameObject, .55f);
        }

        // Update is called once per frame
        void Update()
        {
            gameObject.transform.localScale = new Vector3(_size, _size);
            //transform.RotateAround(player.transform.position, Vector3.forward, -1f * speed * Time.deltaTime);
        }


        private void OnTriggerEnter(Collider other)
        {
            if(other.gameObject.tag != "Player" && other.gameObject.tag != "PowerUps")
            {
                movedirection = gameObject.transform.position - other.gameObject.transform.position;
                other.attachedRigidbody.AddForce(movedirection.normalized * -knockBack);
            }
        }
    }
}