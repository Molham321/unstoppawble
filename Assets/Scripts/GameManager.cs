using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject popup;
    bool gameHasEnded = false;

    private static GameManager instance;
    public Vector3 lastCheckPointPos;

    public void EndGame()
    {
        if (gameHasEnded == false)
        {
            gameHasEnded = true;
            Debug.Log("GameOver");
            GameOverMenu();
        }
    }
    void GameOverMenu()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); //name
    }

    public void EndUI()
    {
        Invoke("HideUI", 3);
    }

    public void HideUI()
    {
        popup.SetActive(false);
    }

    public void ShowUI()
    {
        popup.SetActive(true);
    }

    private void Awake()
    {
        if(instance == null)
        {
            Debug.Log("Neu instance!!!!!!!!!!");
            instance = this;
            DontDestroyOnLoad(instance);
        }
        else
        {
            Debug.Log("instance Destroy!!!!!!!!!!");
            Destroy(gameObject);
        }
    }

}
