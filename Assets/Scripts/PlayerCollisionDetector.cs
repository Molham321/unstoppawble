using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCollisionDetector : MonoBehaviour
{

    public GameObject Lebensanzeige1, Lebensanzeige2, Lebensanzeige3;   // hier kann man game over noch schreiben
    public static int health;

    // Use this for initialization
    private void Start()
    {
        health = 3;
        Lebensanzeige1.gameObject.SetActive(true);
        Lebensanzeige2.gameObject.SetActive(true);
        Lebensanzeige3.gameObject.SetActive(true);
        // hier z.b gameover (false)
    }

    private void Update()
    {
        if (health > 3)
            health = 3;     // man darf nicht mehr als 3 leben haben;

        switch (health)
        {
            case 3:
                Lebensanzeige1.gameObject.SetActive(true);
                Lebensanzeige2.gameObject.SetActive(true);
                Lebensanzeige3.gameObject.SetActive(true);
                break;
            case 2:
                Lebensanzeige1.gameObject.SetActive(true);
                Lebensanzeige2.gameObject.SetActive(true);
                Lebensanzeige3.gameObject.SetActive(false);
                break;
            case 1:
                Lebensanzeige1.gameObject.SetActive(true);
                Lebensanzeige2.gameObject.SetActive(false);
                Lebensanzeige3.gameObject.SetActive(false);
                break;
            case 0:
                Lebensanzeige1.gameObject.SetActive(false);
                Lebensanzeige2.gameObject.SetActive(false);
                Lebensanzeige3.gameObject.SetActive(false);
                Debug.Log("GameOver!");
                break;
        }
    }

    public float counter = 3;
    private void OnTriggerEnter(Collider hit)
    {
        Debug.Log("Blablabla: " + hit.gameObject.name + " Tag: " + hit.gameObject.tag);

        if (hit.gameObject.tag == "Enemy")  // wenn player von einem Enemy getrofen wird veriert er ein leben 
        {
            health--;

            if (health <= 0)    // wenn leben = 0 ist wird das spiel neue gestartet || (gameover angezeigt)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
    }
}