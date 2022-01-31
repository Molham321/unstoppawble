using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowballENEMY : MonoBehaviour
{
    private Rigidbody snowballRb;
    public Vector3 snowballStartPos;

    public float minY = 40f;
    public float spawnDelay = 0f;

    private void Awake()
    {
        snowballRb = GetComponent<Rigidbody>();
    }
    void Start()
    {
        snowballStartPos = transform.position;

        if(spawnDelay > 0)
        {
            StartCoroutine(SnowballStartDelay());
        }
    }


    private void Update()
    {
        FellDown();
        Debug.Log(transform.position.y);
    }
    void FellDown()
    {
        if (transform.position.y < 40f)
        {
            transform.position = snowballStartPos;

            snowballRb.velocity = new Vector3(0, 0, 0);
        }

    }

    IEnumerator SnowballStartDelay()
    {
        snowballRb.isKinematic = true;
        yield return new WaitForSeconds(spawnDelay);
        snowballRb.isKinematic = false;
    }
}
    


