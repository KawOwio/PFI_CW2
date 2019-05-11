using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager GM;
    [SerializeField] private AudioSource AS;

    bool gameEnded = false;

    //Singleton
    void Awake()
    {
        if (GameManager.GM == null)
        {
            GameManager.GM = this;
        }
        else
        {
            if (GameManager.GM != this)
            {
                Destroy(GameManager.GM.gameObject);
                GameManager.GM = this;
            }
        }
        DontDestroyOnLoad(this.gameObject);
    }

    //Called if meteor hits the player
    public void EndGame()
    {
        if (gameEnded == false)
        {
            AS.Play();
            Debug.Log("DEATH");
            gameEnded = true;
            Restart();
        }
    }

    void Restart()
    {
        gameEnded = false;
        SceneManager.LoadScene(2);
    }
}
