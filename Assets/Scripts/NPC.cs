using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NPC : MonoBehaviour
{
    public AudioClip AudioClip;
    public GameObject dialoguePanel;
    public GameObject interactPrompt;
    public TextMeshProUGUI dialogueText;
    public string[] dialogue;
    private int index = 0;

    [SerializeField] private AudioSource SFXSource;
    [SerializeField] private Image dialogueImage; // UI Image component for the canvas
    [SerializeField] private Sprite npcImage; // Sprite to assign to the Image component

    [SerializeField] private TextMeshProUGUI NpcNameUI;
    public string NpcName;

    public float wordSpeed;
    public bool playerIsClose;
    private PlayerController playerController;

    public BoxCollider boxCollider;
    public PlayerInteraction interaction;

    void Start()
    {
        dialogueText.text = "";

        // Ensure the dialogueImage is initially empty or has a placeholder
        if (dialogueImage != null)
        {
            dialogueImage.sprite = null; // Set to null or a default sprite
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && playerIsClose)
        {
            Interact();
        }

        if (Input.GetKeyDown(KeyCode.Q) && dialoguePanel.activeInHierarchy)
        {
            RemoveText();
        }
    }

    public void Interact()
    {
        if (!dialoguePanel.activeInHierarchy)
        {
            SFXSource.PlayOneShot(AudioClip);
            dialoguePanel.SetActive(true);

            // Set the dialogueImage's sprite to the assigned npcImage
            if (dialogueImage != null && npcImage != null)
            {
                dialogueImage.sprite = npcImage;
                dialogueImage.preserveAspect = true; // Optional: Keep the aspect ratio
            }
            if (NpcNameUI != null && NpcName != null)
            {
                NpcNameUI.text = NpcName;
            }

            StartCoroutine(Typing());
        }
        else if (dialogueText.text == dialogue[index])
        {
            NextLine();
        }
    }

    public void RemoveText()
    {
        dialogueText.text = "";
        index = 0;
        dialoguePanel.SetActive(false);

        // Reset the dialogueImage's sprite when dialogue is removed
        if (dialogueImage != null)
        {
            dialogueImage.sprite = null;
        }
    }

    IEnumerator Typing()
    {
        foreach (char letter in dialogue[index].ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(wordSpeed);
        }
    }

    public void NextLine()
    {
        if (index < dialogue.Length - 1)
        {
            index++;
            dialogueText.text = "";
            StartCoroutine(Typing());
        }
        else
        {
            RemoveText();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerController = other.GetComponent<PlayerController>();
            playerIsClose = true;
            //playerController.GetComponent<PlayerInteraction>().npc = this;
            interaction.npc = this;
            interactPrompt.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerIsClose = false;
            interaction.npc = null;
            //playerController.GetComponent<PlayerInteraction>().npc = null;
            RemoveText();
            interactPrompt.SetActive(false);
        }
    }
}
