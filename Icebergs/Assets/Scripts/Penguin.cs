using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Penguin : MonoBehaviour
{
    public Transform PenguinFish;
   

    public GameObject FishPrefab;
    public GameObject Player;

    private GameController gameController;

    private Animator anim;

    public Rigidbody2D rb2d;

    private bool cooldown;

    private Transform target;
    public float speed;

    void Start()
    {
        anim = GetComponent<Animator>();

        GameObject gameControllerObject = GameObject.FindWithTag("GameController");

        if (gameControllerObject != null)
        {
            gameController = gameControllerObject.GetComponent<GameController>();
        }
        if (gameController == null)
        {
            Debug.Log("Cannot find GameController script");
        }

        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        if (cooldown == false)
        {
            StartCoroutine (Fire()) ;
        }
        
    }

    IEnumerator Fire()
    {
        anim.SetInteger("Throw", 1);
        cooldown = true;
        yield return new WaitForSeconds(0.5f);
        anim.SetInteger("Throw", 0);
        Instantiate(FishPrefab,transform.position, Quaternion.identity);
        yield return new WaitForSeconds(3.5f);
        cooldown = false;
    }

    void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject.CompareTag("Bullet"))
        {
            Destroy(other.gameObject);
            gameController.score += 30;
            gameController.Score();
            Destroy(gameObject);
        }
    }

}
