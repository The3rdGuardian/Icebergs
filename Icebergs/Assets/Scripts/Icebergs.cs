using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Icebergs : MonoBehaviour
{
    public GameObject medIceberg;
    public GameObject smallIceberg;
    public Transform LIceSpawn1;
    public Transform LIceSpawn2;
    
    private GameController gameController;

  void Start()
    {
        GameObject gameControllerObject = GameObject.FindWithTag("GameController");

        if (gameControllerObject != null)
        {
            gameController = gameControllerObject.GetComponent<GameController>();
        }
        if (gameController == null)
        {
            Debug.Log("Cannot find GameController script");
        }

        GetComponent<Rigidbody2D>().AddForce(transform.up * Random.Range(-50.0f, 150.0f)*2);
        GetComponent<Rigidbody2D>().angularVelocity = Random.Range(5f, 90.0f)*2;
        transform.position = new Vector2(Random.Range(1, 9), Random.Range(1, 9));
    }
    void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject.CompareTag("Bullet"))
        {
            Destroy(other.gameObject);

            if (gameObject.CompareTag("Large Iceberg"))
            {
                gameController.SplitIceberg();
                gameController.score += 10;
                gameController.Score();
                Destroy(gameObject);

                Instantiate(medIceberg, new Vector3(transform.position.x + 1f,
                        transform.position.y + 1f, 0),
                        Quaternion.Euler(0, 0, 90));

                Instantiate(medIceberg, new Vector3(transform.position.x + 1f,
                        transform.position.y + 1, 0),
                        Quaternion.Euler(0, 0, 0));

            }
            else if (gameObject.CompareTag("Medium Iceberg"))
            {
                gameController.SplitIceberg();
                gameController.score += 15;
                gameController.Score();
                Destroy(gameObject);

                Instantiate(smallIceberg,
                    new Vector3(transform.position.x - 1f,
                        transform.position.y - 1f, 0),
                        Quaternion.Euler(0, 0, 180));

                Instantiate(smallIceberg,
                    new Vector3(transform.position.x + 1f,
                        transform.position.y + 1f, 0),
                        Quaternion.Euler(0, 0, 270));


            }
            else if (gameObject.CompareTag("Small Iceberg"))
            {
                gameController.Icebergtotal();
                gameController.score += 30;
                gameController.Score();
                Destroy(gameObject);
            }
        }
    }
}
