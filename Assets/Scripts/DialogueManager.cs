using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public Text nameText;
    public Text dialogueText;
    public Image profileImage;
    public Text continueText;

    public Animator animator;

    private Queue<string> sentences;
    private bool currentlyTypingSentence;
    private AudioSource audioSource;
    private AudioClip audioClip;

    void Start()
    {
        sentences = new Queue<string>();
        currentlyTypingSentence = false;
        audioSource = GameObject.Find("DialogueManager").GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) 
        {
            if (!currentlyTypingSentence)
            {
            FindObjectOfType<DialogueManager>().DisplayNextSentence();
            }
        }
    }

    public void StartDialogue(Dialogue dialogue)
    {
        audioClip = dialogue.sound;

        profileImage.sprite = dialogue.profileImage;

        animator.SetBool("IsOpen", true);

        nameText.text = dialogue.name;

        sentences.Clear();

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }


        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {

        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
        currentlyTypingSentence = true;
    }

    IEnumerator TypeSentence(string sentence)
    {
        continueText.enabled = false;
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            audioSource.PlayOneShot(audioClip);
            yield return new WaitForSeconds(0.05f);
        }
        continueText.enabled = true;
        currentlyTypingSentence = false;
    }

    void EndDialogue()
    {
        animator.SetBool("IsOpen", false);
        Debug.Log("End of conversation");
        if (FindObjectOfType<TimelineManager>())
        {
            FindObjectOfType<TimelineManager>().ResumePlayback();
        }
    }
}
