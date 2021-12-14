using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCollisionDetector : MonoBehaviour
{
    public float counter = 3;

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {

        if (hit.gameObject.tag == "Enemy") {
            counter--;
            Debug.Log("GETROFFEN!");

            if(counter <= 0)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
    }
    }
}
