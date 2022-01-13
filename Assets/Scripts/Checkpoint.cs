using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public GameObject Hundehütte;
    Vector3 spawnPoint;
    public AudioSource checkpointAudio;
    public AudioClip checkpointSound;

    private void Start()
    {
        spawnPoint = gameObject.transform.position;
        checkpointAudio = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (gameObject.transform.position.y < -100f)
        {
            gameObject.transform.position = spawnPoint;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Checkpoint"))
        {
            checkpointAudio.PlayOneShot(checkpointSound, 0.7f);
            spawnPoint = Hundehütte.transform.position;
        }
    }
}
