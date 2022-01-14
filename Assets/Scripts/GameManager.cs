using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    bool gameHasEnded = false;

    public float restartDelay = 0f;
    public void EndGame()
    {
        if (gameHasEnded == false)
        {
            gameHasEnded = true;
            Debug.Log("GameOver");
            //Invoke("GameOverMenu", restartDelay);
            GameOverMenu();
        }
    }

    void GameOverMenu()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); //name
    }
}
