using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class LodingMenu : MonoBehaviour
{
    // Update is called once per frame
    void Start()
    {
        Invoke("Loding", 12);
    }
    public void Loding()
    {
        SceneManager.LoadScene("Menu");
    }
}
