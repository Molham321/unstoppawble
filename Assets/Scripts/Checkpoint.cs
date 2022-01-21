using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    //public AudioSource checkpointAudio;
    //public AudioClip checkpointSound;

    private GameManager gameManager;

    private void Start()
    {
       // checkpointAudio = GetComponent<AudioSource>();
        gameManager = GameObject.FindGameObjectWithTag("GM").GetComponent<GameManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("wir sind daaaaaaaaaaaaaa!");
            //checkpointAudio.PlayOneShot(checkpointSound, 0.7f);
            gameManager.lastCheckPointPos = transform.position;

        }
    }
}
