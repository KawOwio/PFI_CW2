using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    bool gameEnded = false;

    public float restartDelay = 2.0f;

    public void EndGame()
    {
        if (gameEnded == false)
        {
            Debug.Log("DEATH");
            gameEnded = true;
            Invoke("Restart", restartDelay);
        }
    }

    void Restart()
    {
        SceneManager.LoadScene(2);
    }
}
