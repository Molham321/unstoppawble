using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TriggerEnding : MonoBehaviour
{
    //public AudioSource checkpointAudio;
    //public AudioClip checkpointSound;

    //private void Start()
    //{
    //    checkpointAudio = GetComponent<AudioSource>();
    //}

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Player"))
        {
            SceneManager.LoadScene(4);
        }
    }

}
