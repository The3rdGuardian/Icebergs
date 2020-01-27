using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishBullet : MonoBehaviour
{
    public float speed = 10f;
    public Rigidbody2D rb2d;
    public float time = 5f;

    private Transform target;
    Vector2 Direction;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        Direction = (target.transform.position - transform.position).normalized * speed;
        rb2d.velocity = new Vector2 (Direction.x, Direction.y);
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
