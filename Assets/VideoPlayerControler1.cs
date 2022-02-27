using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class VideoPlayerControler1 : MonoBehaviour
{
    public VideoPlayer temp;

    // Update is called once per frame
    void Update()
    {
        temp = GetComponent<VideoPlayer>();

        Debug.Log(temp.time + ", " + temp.clip.length);
        if (temp.time >= temp.clip.length)
        {
            SceneManager.LoadScene(1);
        }
    }
}
