using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    private GameController gameController;

    public float moveSpeed;
    public float rotationSpeed;

    private Rigidbody2D rb2d;

    private Animator anim;

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
        rb2d = GetComponent<Rigidbody2D>();
    }


    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        transform.Rotate(0, 0, -moveHorizontal * Time.deltaTime * rotationSpeed);
        transform.Translate(Vector3.up * moveVertical * Time.deltaTime * moveSpeed);
     }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag != "Bullet")
        {
            if (gameController.invincible == false)
            {
                GetComponent<Rigidbody2D>().
                    velocity = new Vector3(0, 0, 0);

                StartCoroutine(Invulnerabilityanim());
                gameController.Lives();
            }
        }
    }
    IEnumerator Invulnerabilityanim()
    {
        anim.SetInteger("Death", 1);
        yield return new WaitForSeconds(0.5f);
        anim.SetInteger("Death", 0);
        anim.SetInteger("Invincible", 1);
        yield return new WaitForSeconds(gameController.invincibilityTime - 0.5f);
        anim.SetInteger("Invincible", 0);
    }
}
