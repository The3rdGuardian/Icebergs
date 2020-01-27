using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 10f;
    public Rigidbody2D rb2d;
    public float time = 5f;

    // Start is called before the first frame update
    void Start()
    {
        rb2d.velocity = transform.up * speed;
    }
    void Update()
    {
        Destroy();
    }
    void Destroy()
    {
        Destroy(gameObject, time);
    }
 /*   private void OnTriggerEnter2D(Collider2D )
    {
        Destroy(gameobject);
    }*/
}
