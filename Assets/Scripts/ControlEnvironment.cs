using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlEnvironment : MonoBehaviour
{
    [SerializeField] private GameObject snowParticles;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("snowEnvironment"))
        {
            snowParticles.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("snowEnvironment"))
        {
            snowParticles.SetActive(false);
        }
    }
}
