using System.Collections;
using EasyUI.Dialogs;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    [SerializeField] private Text diamondConter;

    [SerializeField] private int diamonds = 0;

    [SerializeField] private GameObject bridgeToLevel2;
    [SerializeField] private GameObject bridgeToLevel3;
    [SerializeField] private GameObject bridgeToLevel4;
    [SerializeField] private GameObject bridgeToLevel5;

    [SerializeField] private AudioClip collectibleSound;
    [SerializeField] private AudioClip bridgeAppearanceSound;
    [SerializeField] private AudioClip checkpointSound;

    [SerializeField] private AudioSource collectibleAudio;
    [SerializeField] private AudioSource bridgeAppearanceAudio;
    [SerializeField] private AudioSource checkpointAudio;

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

                if (diamonds == 4)
                {
                    //bridgeToLevel2.SetActive(true);
                    StartCoroutine(UnlockNextLevel(bridgeToLevel2));

                    // First Dialog -----------------------------
                    FindObjectOfType<GameManager>().ShowUI();
                    DialogUI.Instance
                    .SetTitle("Congratulation!")
                    .SetMessage("you collected 4 proof, a bridge has opened for you. Go to Level 2")
                    .SetButtonColor(DialogButtonColor.Blue)
                    .OnClose(() => Debug.Log("Closed 1"))
                    .Show();
                    FindObjectOfType<GameManager>().EndUI();
                    //--------------------------------------------
                }
                if (diamonds == 8)
                {
                    //bridgeToLevel3.SetActive(true);
                    StartCoroutine(UnlockNextLevel(bridgeToLevel3));

                    // First Dialog -----------------------------
                    FindObjectOfType<GameManager>().ShowUI();
                    DialogUI.Instance
                    .SetTitle("Congratulation!")
                    .SetMessage("you collected 4 proof, a bridge has opened for you. Go to Level 3")
                    .SetButtonColor(DialogButtonColor.Blue)
                    .OnClose(() => Debug.Log("Closed 1"))
                    .Show();
                    FindObjectOfType<GameManager>().EndUI();
                    //--------------------------------------------
                }
                if (diamonds == 16)
                {
                    StartCoroutine(UnlockNextLevel(bridgeToLevel4));

                    // First Dialog -----------------------------
                    FindObjectOfType<GameManager>().ShowUI();
                    DialogUI.Instance
                    .SetTitle("Congratulation!")
                    .SetMessage("you collected 4 proof, a bridge has opened for you. Go to Level 4")
                    .SetButtonColor(DialogButtonColor.Blue)
                    .OnClose(() => Debug.Log("Closed 1"))
                    .Show();
                    FindObjectOfType<GameManager>().EndUI();
                    //--------------------------------------------
                }
                if (diamonds == 26)
                {
                    StartCoroutine(UnlockNextLevel(bridgeToLevel5));

                    // First Dialog -----------------------------
                    FindObjectOfType<GameManager>().ShowUI();
                    DialogUI.Instance
                    .SetTitle("Congratulation!")
                    .SetMessage("you collected 4 proof, a bridge has opened for you. Go to Level 5")
                    .SetButtonColor(DialogButtonColor.Blue)
                    .OnClose(() => Debug.Log("Closed 1"))
                    .Show();
                    FindObjectOfType<GameManager>().EndUI();
                    //--------------------------------------------
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
