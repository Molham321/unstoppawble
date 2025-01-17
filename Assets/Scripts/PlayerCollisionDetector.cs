using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCollisionDetector : MonoBehaviour
{

    [SerializeField] private GameObject Lebensanzeige1, Lebensanzeige2, Lebensanzeige3;   // hier kann man game over noch schreiben
    [SerializeField] private static int health;
    [SerializeField] private AudioSource damageAudio;
    [SerializeField] private AudioClip damageSound;
    [SerializeField] private AudioClip hitSound;

    // Use this for initialization
    private void Start()
    {
        health = 3;
        Lebensanzeige1.gameObject.SetActive(true);
        Lebensanzeige2.gameObject.SetActive(true);
        Lebensanzeige3.gameObject.SetActive(true);
        damageAudio.GetComponent<AudioSource>();
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

        if (hit.gameObject.tag == "Enemy")  // wenn player von einem Enemy getrofen wird verliert er ein leben 
        {
            health--;
            damageAudio.PlayOneShot(hitSound, 0.7f);
            damageAudio.PlayOneShot(damageSound, 0.7f);


            if (health <= 0)    // wenn leben = 0 ist wird das spiel neue gestartet || (gameover angezeigt)
            {
                //----------------Restart Game----------------------------------
                PlayerPrefs.DeleteKey("Diamonds");
                FindObjectOfType<GameManager>().EndGame();
            }
        }
    }
}
