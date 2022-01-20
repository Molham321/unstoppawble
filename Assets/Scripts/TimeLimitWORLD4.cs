using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeLimitWORLD4 : MonoBehaviour
{
    public GameObject textDisplay;

    public int secondsLeft = 10;
    public bool takingAway = false;

    public Rigidbody playerRb;
    public GameObject Hundehütte;

    Vector3 spawnPoint;

    // Start is called before the first frame update
    void Start()
    {
        spawnPoint = Hundehütte.transform.position;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            textDisplay.GetComponent<Text>().text = "00:" + secondsLeft;

            if (takingAway == false && secondsLeft > 0)
            {
                StartCoroutine(TimerTake());

                if (takingAway == true && secondsLeft <= 0)
                {
                    playerRb.transform.position = spawnPoint;
                }
            }

        }
    }

    private void OnTriggerstay(Collider other)
    { 
        if(other.gameObject.tag == "Player")
        {
            if (takingAway == false && secondsLeft > 0)
            {
                StartCoroutine(TimerTake());

                 if (takingAway == true && secondsLeft <= 0)
                {
                    playerRb.transform.position = spawnPoint;
                }
            }
        }
    }

    IEnumerator TimerTake()
    {
        takingAway = true;
        yield return new WaitForSeconds(1);
        secondsLeft -= 1;
        textDisplay.GetComponent<Text>().text = "" + secondsLeft;
        takingAway = false;
    }
}
