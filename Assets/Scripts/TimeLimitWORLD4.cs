using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TimeLimitWORLD4 : MonoBehaviour
{
    [SerializeField] private Timer timer1;
    [SerializeField] private Timer timer2;
    [SerializeField] private Timer timer3;
    [SerializeField] private Timer timer4;

    [SerializeField] private GameObject Hundehütte;
    [SerializeField] private GameObject ui;

    [SerializeField] private Vector3 spawnPoint;

    // Start is called before the first frame update
    void Start()
    {
        spawnPoint = Hundehütte.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        TimerPaused();
        TimerFinished();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "StartTimer")
        {
            ui.SetActive(true);

            timer1
            .SetDuration(60)
            .OnEnd(() => Debug.Log("Timer 1 ended"))
            .Begin();


            timer2
            .SetDuration(60)
            .OnEnd(() => Debug.Log("Timer 2 ended"))
            .Begin();


            timer3
            .SetDuration(60)
            .OnEnd(() => Debug.Log("Timer 3 ended"))
            .Begin();

            timer4
            .SetDuration(60)
            .OnEnd(() => Debug.Log("Timer 4 ended"))
            .Begin();

        }
        if (other.gameObject.tag == "StopTimer")
        {
            timer1
            .OnDestroy();

            timer2
            .OnDestroy();

            timer3
            .OnDestroy();

            timer4
            .OnDestroy();

            ui.SetActive(false);
        }
    }

    private void TimerPaused()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            timer1.SetPaused(!timer1.IsPaused);
            timer2.SetPaused(!timer2.IsPaused);
            timer3.SetPaused(!timer3.IsPaused);
            timer4.SetPaused(!timer4.IsPaused);
        }
    }

    private void TimerFinished()
    {
        if (timer1.finished)
        {
            Debug.Log(timer1.finished);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
