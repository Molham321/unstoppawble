using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeLimitWORLD4 : MonoBehaviour
{
    [SerializeField] Timer timer1;
    [SerializeField] Timer timer2;
    [SerializeField] Timer timer3;
    [SerializeField] Timer timer4;

    public GameObject ui;

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

        //timer1
        //.SetDuration(6)
        //.OnEnd(() => Debug.Log("Timer 1 ended"))
        //.Begin();

        //timer2
        //.SetDuration(10)
        //.OnEnd(() => Debug.Log("Timer 2 ended"))
        //.Begin();

        //timer3
        //.SetDuration(15)
        //.OnEnd(() => Debug.Log("Timer 3 ended"))
        //.Begin();

        //timer4
        //.SetDuration(25)
        //.OnEnd(() => Debug.Log("Timer 4 ended"))
        //.Begin();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            timer1.SetPaused(!timer1.IsPaused);
            timer2.SetPaused(!timer2.IsPaused);
            timer3.SetPaused(!timer3.IsPaused);
            timer4.SetPaused(!timer4.IsPaused);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            //textDisplay.GetComponent<Text>().text = "00:" + secondsLeft;
            Debug.Log("wir sind drin!");
            ui.SetActive(true);

            timer1
            .SetDuration(6)
            .OnEnd(() => Debug.Log("Timer 1 ended"))
            .Begin();


            timer2
            .SetDuration(10)
            .OnEnd(() => Debug.Log("Timer 2 ended"))
            .Begin();


            timer3
            .SetDuration(15)
            .OnEnd(() => Debug.Log("Timer 3 ended"))
            .Begin();

            timer4
            .SetDuration(25)
            .OnEnd(() => Debug.Log("Timer 4 ended"))
            .Begin();

            if (takingAway == false && secondsLeft > 0)
            {
                //StartCoroutine(TimerTake());

                if (takingAway == true && secondsLeft <= 0)
                {
                    playerRb.transform.position = spawnPoint;
                }
            }

        }
    }

    //private void OnTriggerstay(Collider other)
    //{ 
    //    if(other.gameObject.tag == "Player")
    //    {
    //        if (takingAway == false && secondsLeft > 0)
    //        {
    //            StartCoroutine(TimerTake());

    //             if (takingAway == true && secondsLeft <= 0)
    //            {
    //                playerRb.transform.position = spawnPoint;
    //            }
    //        }
    //    }
    //}

    //IEnumerator TimerTake()
    //{
    //    takingAway = true;
    //    yield return new WaitForSeconds(1);
    //    secondsLeft -= 1;
    //    textDisplay.GetComponent<Text>().text = "" + secondsLeft;
    //    takingAway = false;
    //}
}
