using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;


public class VideoPlayerScript : MonoBehaviour
{
    public VideoPlayer temp;
    private bool firstTime = false;
    public VideoClip myClip;

    private void Start()
    {
        temp = GetComponent<VideoPlayer>();
        temp.url = System.IO.Path.Combine(Application.streamingAssetsPath, "IntroUnstopPAWble.mp4");
        temp.Play();
    }

    private void Update()
    {
        if(Input.anyKey && !firstTime)
        {
            temp.Play();

            firstTime = true;
        }
      
        if (temp.time >= myClip.length)
        {
            SceneManager.LoadScene(1);
        }
    }
}
