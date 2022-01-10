using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public GameObject Hundehütte;
    Vector3 spawnPoint;

    private void Start()
    {
        spawnPoint = gameObject.transform.position;
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
            spawnPoint = Hundehütte.transform.position;
        }
    }
}
