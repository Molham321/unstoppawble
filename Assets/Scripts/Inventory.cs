using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using EasyUI.Dialogs;

public class Inventory : MonoBehaviour
{
    public Text diamondConter;
    public GameObject bridgeToNextLevel;
    public AudioClip collectibleSound;
    public AudioClip bridgeAppearanceSound;

    private int diamonds = 0;
    private AudioSource collectibleAudio;
    private AudioSource bridgeAppearanceAudio;

    void Start()
    {
        collectibleAudio = GetComponent<AudioSource>();
        bridgeAppearanceAudio = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Collectible"))
        {
            Collect(other.GetComponent<Collectible>());
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
                    bridgeToNextLevel.SetActive(true);
                    StartCoroutine(PlayLevelCompletionSound());
                }
            }
            UpdateGUI();
        }
    }

    private void UpdateGUI()
    {
        diamondConter.text = diamonds.ToString();
    }

    IEnumerator PlayLevelCompletionSound()
    {
        yield return new WaitForSeconds(1);
        bridgeAppearanceAudio.PlayOneShot(bridgeAppearanceSound, 0.7f);
    }
}
