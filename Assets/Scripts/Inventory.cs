using System.Collections;
using EasyUI.Dialogs;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    [SerializeField] private Text diamondConter;

    [SerializeField] public int diamonds;

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

        diamonds = PlayerPrefs.GetInt("Diamonds");
        UpdateGUI();
    }
    private void Update()
    {
        CheckBridge();
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
            FindObjectOfType<GameManager>().ShowUI();
            DialogUI.Instance
            .SetTitle("You reached the end and found your owner.")
            .SetMessage("Congratulations!")
            .SetButtonColor(DialogButtonColor.Blue)
            .OnClose(() => Debug.Log("Closed 1"))
            .Show();
            FindObjectOfType<GameManager>().EndUI();
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
                UpdateGUI();

                if (diamonds == 4)
                {
                    // First Dialog -----------------------------
                    FindObjectOfType<GameManager>().ShowUI();
                    DialogUI.Instance
                    .SetTitle("Congratulation!")
                    .SetMessage("You collected enough notes! A bridge appeared.")
                    .SetButtonColor(DialogButtonColor.Blue)
                    .OnClose(() => Debug.Log("Closed 1"))
                    .Show();
                    FindObjectOfType<GameManager>().EndUI();
                    //-------------------------------------------- }

                    //bridgeToLevel2.SetActive(true);
                    StartCoroutine(UnlockNextLevel(bridgeToLevel2));
                }

                if (diamonds == 8)
                {
                    // First Dialog -----------------------------
                    FindObjectOfType<GameManager>().ShowUI();
                    DialogUI.Instance
                    .SetTitle("Congratulation!")
                    .SetMessage("You collected enough notes! A bridge appeared.")
                    .SetButtonColor(DialogButtonColor.Blue)
                    .OnClose(() => Debug.Log("Closed 1"))
                    .Show();
                    FindObjectOfType<GameManager>().EndUI();
                    //--------------------------------------------
                    StartCoroutine(UnlockNextLevel(bridgeToLevel3));
                }

                if (diamonds == 16)
                {

                    // First Dialog -----------------------------
                    FindObjectOfType<GameManager>().ShowUI();
                    DialogUI.Instance
                    .SetTitle("Congratulation!")
                    .SetMessage("You collected enough notes! A bridge appeared.")
                    .SetButtonColor(DialogButtonColor.Blue)
                    .OnClose(() => Debug.Log("Closed 1"))
                    .Show();
                    FindObjectOfType<GameManager>().EndUI();
                    //--------------------------------------------
                    StartCoroutine(UnlockNextLevel(bridgeToLevel4));
                }

                if (diamonds == 26)
                {
                    // First Dialog -----------------------------
                    FindObjectOfType<GameManager>().ShowUI();
                    DialogUI.Instance
                    .SetTitle("Congratulation!")
                    .SetMessage("You collected enough notes! A bridge appeared.")
                    .SetButtonColor(DialogButtonColor.Blue)
                    .OnClose(() => Debug.Log("Closed 1"))
                    .Show();
                    FindObjectOfType<GameManager>().EndUI();
                    //--------------------------------------------
                    StartCoroutine(UnlockNextLevel(bridgeToLevel5));
                }
            }
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
        UpdateGUI();
    }

    void CheckBridge()
    {

        if (diamonds >= 4)
        {
            bridgeToLevel2.SetActive(true);
        }
        if (diamonds >= 8)
        {
            bridgeToLevel3.SetActive(true);
        }
        if (diamonds >= 16)
        {
            bridgeToLevel4.SetActive(true);
        }
        if (diamonds >= 26)
        {
            bridgeToLevel5.SetActive(true);
        }
    }
}
