using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using EasyUI.Dialogs;

public class Inventory : MonoBehaviour
{
    public Text diamondConter;
    public GameObject bridgeToLevel2;
    public GameObject bridgeToLevel3;
    public GameObject bridgeToLevel4;
    public GameObject bridgeToLevel5;
    public AudioClip collectibleSound;
    public AudioClip bridgeAppearanceSound;
    public AudioClip checkpointSound;

    private int diamonds = 0;
    private AudioSource collectibleAudio;
    private AudioSource bridgeAppearanceAudio;
    private AudioSource checkpointAudio;

    void Start()
    {
        collectibleAudio = GetComponent<AudioSource>();
        bridgeAppearanceAudio = GetComponent<AudioSource>();
        checkpointAudio = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Collectible"))
        {
            Collect(other.GetComponent<Collectible>());
        }
        
        if (other.CompareTag("Finish") && diamonds == 32)
        {
            checkpointAudio.PlayOneShot(checkpointSound, 0.7f);
            Debug.Log("You reached the end. Congratulations!");
        }
    }

    private void Collect(Collectible collectible)
    {
        if (collectible.Collect())
        {
            if (collectible is DiamondCollectible)
            {
                diamonds++;
                collectibleAudio.PlayOneShot(collectibleSound, 0.7f);

                //// test

                // First Dialog -----------------------------
                DialogUI.Instance
                .SetTitle("Message 1")
                .SetMessage("Hello!")
                .SetButtonColor(DialogButtonColor.Blue)
                .OnClose(() => Debug.Log("Closed 1"))
                .Show();


                //// Second Dialog ----------------------------
                //DialogUI.Instance
                //.SetTitle("Message 2")
                //.SetMessage("Hello Again :)")
                //.SetButtonColor(DialogButtonColor.Magenta)
                //.SetButtonText("ok")
                //.OnClose(() => Debug.Log("Closed 2"))
                //.Show();


                //// Third Dialog -----------------------------
                //DialogUI.Instance
                //.SetTitle("Message 3")
                //.SetMessage("Bye!")
                //.SetFadeInDuration(1f)
                //.SetButtonColor(DialogButtonColor.Red)
                //.OnClose(() => Debug.Log("Closed 3"))
                //.Show();

                if (diamonds == 4)
                {
                    //bridgeToLevel2.SetActive(true);
                    StartCoroutine(UnlockNextLevel(bridgeToLevel2));
                }
                if (diamonds == 8)
                {
                    //bridgeToLevel3.SetActive(true);
                    StartCoroutine(UnlockNextLevel(bridgeToLevel3));
                }
                if (diamonds == 16)
                {
                    StartCoroutine(UnlockNextLevel(bridgeToLevel4));
                }
                if (diamonds == 26)
                {
                    StartCoroutine(UnlockNextLevel(bridgeToLevel5));
                }
            }
            UpdateGUI();
        }
    }

    private void UpdateGUI()
    {
        diamondConter.text = diamonds.ToString();
    }

    IEnumerator UnlockNextLevel(GameObject bridge)
    {
        yield return new WaitForSeconds(1);
        bridge.SetActive(true);
        bridgeAppearanceAudio.PlayOneShot(bridgeAppearanceSound, 0.7f);
    }
}
