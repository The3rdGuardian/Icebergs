using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    
    public GameObject Player;
    public GameObject Iceberg;
    public GameObject Penguin;
    public int lives;

    public Text ScoreText;
    public Text LivesText;
    public Text RemainText;
    public Text TotalText;
    public Text LevelText;

    public int score;
    public int increase = 1;
    private int waves;
    private int remaining;
    private int total;
    private int penguins = 2;

    public bool invincible = false;
    private bool end = true;
    public float invincibilityTime = 3f;


    void Start()
    {
        score = 0;
        lives = 3;
        waves = 1;

        LivesText.text = "Lives:  " + lives;
        ScoreText.text = "Score: " + score;
        TotalText.text = "Total:";
        LevelText.text = "Level 1";

        SpawnIcebergs();
        total = (remaining * 160);
        total = total + (total * 2);
        total = total + (penguins * 30);
        TotalText.text = "Total: " + total;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            SceneManager.LoadScene("Menu");
        }

        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }
    }

    void SpawnIcebergs()
    {
        Begin();

        remaining = (waves * increase);


        for (int i = 0; i < remaining; i++)
            {
                Instantiate(Iceberg,
                    new Vector3(Random.Range(-9.0f, 9.0f),
                        Random.Range(-6.0f, 6.0f), 0),
                    Quaternion.Euler(0, 0, Random.Range(-0.0f, 359.0f)));
            }
    }

    IEnumerator PenguinSpawn()
    {
        end = false;

        for (int i = 0; i < penguins; i++)
        {
            Instantiate(Penguin,
                    new Vector3(Random.Range(-9.0f, 9.0f),
                        Random.Range(-6.0f, 6.0f), 0),
                    Quaternion.Euler(0, 0, 0));
            yield return new WaitForSeconds(2f);
        }
        penguins++;
        end = true;
    }

    void Begin()
    {
        GameObject[] Fish =
               GameObject.FindGameObjectsWithTag("Fish");

            foreach (GameObject current in Fish)
            {
            GameObject.Destroy(current);
            }

        GameObject[] LIcebergs =
              GameObject.FindGameObjectsWithTag("Large Iceberg");

            foreach (GameObject current in LIcebergs)
            {
                GameObject.Destroy(current);
            }

        GameObject[] mIcebergs =
              GameObject.FindGameObjectsWithTag("Medium Iceberg");

            foreach (GameObject current in mIcebergs)
            {
                GameObject.Destroy(current);
            }

        GameObject[] sIcebergs =
              GameObject.FindGameObjectsWithTag("Small Iceberg");

            foreach (GameObject current in sIcebergs)
            {
            GameObject.Destroy(current);
            }

        GameObject[] Penguins =
              GameObject.FindGameObjectsWithTag("Penguin");

            foreach (GameObject current in Penguins)
            {
                GameObject.Destroy(current);
            }
    }
    

    public void Icebergtotal()
    {
        remaining--;
    }
    public void SplitIceberg()
    {
        remaining ++;
    }
    public void Lives()
    {
        if (!invincible)
        {
            lives = lives - 1;
            LivesText.text = "Lives:  " + lives;
            StartCoroutine(Invulnerability());
        }

        if (lives < 1)
            GameOver();
    }

    public void Score()
    {
        ScoreText.text = "Score: " + score;
        TotalText.text = "Total: " + total;

        if (remaining ==0 && score != total)
        {
            waves++;
            SpawnIcebergs();
            StartCoroutine(PenguinSpawn());
            LevelText.text = "Level 2";
            Player.transform.position = new Vector2(0,0);
        }

        if (score == total && remaining == 0)
        {
            GameOver();
        }


    }
    
    IEnumerator Invulnerability()
    {
        invincible = true;
        yield return new WaitForSeconds(invincibilityTime);
        invincible = false;
    }

    void GameOver()
    {
        if(lives < 1)
        {
            SceneManager.LoadScene("Defeat");
        }

        if (score >= total)
        {
            SceneManager.LoadScene("Victory");
        }
    }
}

