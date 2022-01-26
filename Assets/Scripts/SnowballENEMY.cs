using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowballENEMY : MonoBehaviour
{
    public Rigidbody snowballRb_two;
    public Rigidbody snowballRb;
    public Vector3 snowballStartPos;

// Start is called before the first frame update
void Start()
    {
        snowballStartPos = transform.position;

        StartCoroutine("snowballTwoStarter");
    }


    private void Update()
    {
        FellDown();
    }
    void FellDown()
    {
        if (snowballRb.velocity.y < -50f)
        {
            transform.position = snowballStartPos;
            Debug.Log(snowballStartPos);
            snowballRb.velocity = new Vector3(0, 0, 0);
        }

        if (snowballRb_two.velocity.y < -50f)
        {
            transform.position = snowballStartPos;
            Debug.Log(snowballStartPos);
            snowballRb_two.velocity = new Vector3(0, 0, 0);
        }
    }

    IEnumerator snowballTwoStarter()
    {
        yield return new WaitForSeconds(15);
        snowballRb_two.isKinematic = false;
    }
}
