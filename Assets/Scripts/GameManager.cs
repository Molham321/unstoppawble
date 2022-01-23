using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject popup;

    [SerializeField] private static GameManager instance;
    [SerializeField] public Vector3 lastCheckPointPos;

    // Load Game Over menu
    public void EndGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    // close dialog window after 3 seconds
    public void EndUI()
    {
        Invoke("HideUI", 3);
    }

    // close dialog window
    public void HideUI()
    {
        popup.SetActive(false);
    }

    // open dialog window
    public void ShowUI()
    {
        popup.SetActive(true);
    }

    // save Checkpoit
    public void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(instance);
        }
        else
        {
            Destroy(gameObject);
        }
    }

}
