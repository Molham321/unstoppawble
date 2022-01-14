using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour
{

    public void RestartButton()
    {
        SceneManager.LoadScene("mainScene");
    }

    public void ExitButton()
    {
        SceneManager.LoadScene("Menu");
    }
}
