using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket_Explosion : MonoBehaviour
{
    // Start is called before the first frame update
    public Vector3 minScale;
    public Vector3 maxScale;
    public float speed = 2f;
    public float duration = 5f;
    public bool repeatable;

    private void Awake()
    {
        transform.localScale = minScale;
        Destroy(gameObject, 0.5f);
    }
    IEnumerator Start()
    {
        minScale = transform.localScale;
        //while (repeatable)
        //{
            yield return Scale(minScale, maxScale, duration);
            //yield return Scale(maxScale, minScale, duration);
        //}
    }

    IEnumerator Scale(Vector3 a, Vector3 b, float time)
    {
        float i = 0.0f;
        float rate = (1.0f / time) * speed;
        while (i < 1.0f)
        {
            i += Time.deltaTime * rate;
            transform.localScale = Vector3.Lerp(a, b, i);
            yield return null;
        }
    }
}
