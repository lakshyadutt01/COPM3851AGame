using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public Text nameText;
    public Text dialogueText;
    public float delay;
    public Animator animator;
    public Animator bookAnimator;

    private Queue<string> sentences;
    public GameObject journalUI;
    public AudioSource bookSound;


    void Start()
    {
        sentences = new Queue<string>();
    }


    public void StartDialogue (Dialogue dialogue)
    {
        animator.SetBool("IsOpen", true);
        bookAnimator.SetBool("IsOpen", true);
        bookSound.Play();
        journalUI.SetActive(true);

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
    }

    IEnumerator TypeSentence (string sentence)
    {
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(delay);
        }
    }

    public void EndDialogue()
    {
        journalUI.SetActive(false);
        animator.SetBool("IsOpen", false);
        bookAnimator.SetBool("IsOpen", false);
    }
}
