using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Score : MonoBehaviour {

    [SerializeField] private Text scoreText;
    [SerializeField] private Text highScore;
    [SerializeField] private Pistol pistol;

    int score;

    private void Start()
    {
        if(SceneManager.GetActiveScene().buildIndex == 2)
        {
            scoreText.text = "Score: " + PlayerPrefs.GetInt("LastScore", 0).ToString();
        }
        highScore.text = "High Score: " + PlayerPrefs.GetInt("HighScore", 0).ToString();
    }

    void Update ()
    {
        score = pistol.GetScore();
        scoreText.text = "Score: " + score.ToString();

        //Updating highscore
        if (score > PlayerPrefs.GetInt("HighScore", 0))
        {
            PlayerPrefs.SetInt("HighScore", score);
            highScore.text = "High Score: " + score.ToString();
        }
        else
        {
            PlayerPrefs.SetInt("LastScore", score);
        }
	}

    public void Reset()
    {
        PlayerPrefs.DeleteKey("HighScore");
        PlayerPrefs.DeleteKey("LastScore");
    }
}
