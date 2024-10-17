using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager singleton;
    public int best;
    public int score;
    public int currentStage = 0;
    public GameObject ball2;

    private void Awake()
    {
        if (singleton == null)
            singleton = this;
        else if (singleton != this)
            Destroy(gameObject);

        // Load the saved highscore
        best = PlayerPrefs.GetInt("Highscore");
    }

    public void NextLevel()
    {
        if ((currentStage + 1) < FindObjectOfType<HelixController>().allStages.Count)
        {
            currentStage++;
            FindObjectOfType<BallController>().ResetBall();
            FindObjectOfType<HelixController>().LoadStage(currentStage);

            //make 2nd ball visible
            if (currentStage == 1)
            {
                ball2.SetActive(true);
                ball2.transform.position = new Vector3(0.35f, 4, -1.33f);
            }
        }
    }

    public void RestartLevel()
    {
        Debug.Log("Restarting Level");
        // Show Adds Advertisement.Show();
        singleton.score = 0;
        FindObjectOfType<BallController>().ResetBall();
        FindObjectOfType<HelixController>().LoadStage(currentStage);

        //reset ball2 if visible
        if (currentStage == 1 && ball2.activeSelf)
        {
            ball2.SetActive(false);
            ball2.SetActive(true);
            Vector3 pos = FindObjectOfType<BallController>().startPos;
            ball2.transform.position = new Vector3(pos.x, pos.y, pos.z);
        }
    }

    public void AddScore(int scoreToAdd)
    {
        score += scoreToAdd;

        if (score > best)
        {
            PlayerPrefs.SetInt("Highscore", score);
            best = score;
        }
    }


}
