using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    [Header("Audio")]
    [SerializeField] private AudioSource checkpointAudio;
    [SerializeField] private AudioClip checkpointSound;

    [Header("GameManager")]
    [SerializeField] private GameManager gameManager;

    private void Start()
    {
       checkpointAudio = GetComponent<AudioSource>();
       gameManager = GameObject.FindGameObjectWithTag("GM").GetComponent<GameManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Checkpoint"))
        {
            checkpointAudio.PlayOneShot(checkpointSound, 0.7f);
            gameManager.lastCheckPointPos = transform.position;

        }
    }
}
