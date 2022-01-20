using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlattform : MonoBehaviour
{
    public Rigidbody rb;
    Vector3 startPos;

    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine("fallDown");
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.tag == "Bounds")
        {
            rb.isKinematic = true;
            rb.velocity = new Vector3(0, 0, 0);
            transform.position = startPos;
        }
    }

    IEnumerator fallDown()
    {
        yield return new WaitForSeconds(0.05f);
        rb.isKinematic = false;
    }
}
