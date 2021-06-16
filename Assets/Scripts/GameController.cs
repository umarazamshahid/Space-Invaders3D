using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public GameObject hazard;
    public Vector3 spawnValues;
    public int hazardCount;
    public float spawnWait;
    public float startWait;
    public float waveWait;

    public Text scoreText; 
    public Text restartText;
    public Text gameOverText;

    private bool restart;
    private bool gameOver;    
    private int score;
    // Start is called before the first frame update
    void Start()
    {
        restart = false;
        restartText.text = "";
        gameOverText.text = "";
        gameOver = false;
        score = 0;
        UpdateScore();
        StartCoroutine(SpawnWaves());
    }
    private void Update()
    {
        if (restart)
        {
            if(Input.GetKeyDown(KeyCode.R))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }
    public void AddScore(int newScore)
    {
        score += newScore;
        UpdateScore();
    }
    
    void UpdateScore()
    {
        scoreText.text = "Score: " + score;
    }

    IEnumerator SpawnWaves()
    {
        yield return new WaitForSeconds(startWait);
        while (true)
        {
            for (int i = 0; i < hazardCount; i++)
            {
                Vector3 spawnPos = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
                Quaternion spawnRot = Quaternion.identity;
                Instantiate(hazard, spawnPos, spawnRot);
                yield return new WaitForSeconds(spawnWait);
            }
            yield return new WaitForSeconds(waveWait);

            if (gameOver)
            {
                restartText.text = "Press 'R' to restart";
                restart = true;
                break;
            }
        }
    }

    public void GameOver()
    {
        gameOver = true;
        gameOverText.text = "Game Over!";
    }
}
