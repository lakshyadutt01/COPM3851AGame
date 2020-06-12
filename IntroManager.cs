using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class IntroManager : MonoBehaviour
{

    public Text nameText;
    public Text dialogueText;
    private Queue<string> sentences;
    public float delay;
    public AudioSource textSound;
    public AudioClip[] textSoundCollection;

    void Start()
    {
        sentences = new Queue<string>();
        
    }

    public void StartIntro(IDialogue dialogue)
    {

        nameText.text = dialogue.name;

        sentences.Clear();

        foreach(string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }
        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if(sentences.Count == 0)
        {
            EndIntro();
            return;
        }
        string sentence = sentences.Dequeue();
        dialogueText.text = sentence;
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    IEnumerator TypeSentence (string sentence)
    {
        dialogueText.text = "";
            foreach (char letter in sentence.ToCharArray())
        {
            textSound.clip = textSoundCollection[Random.Range(0, textSoundCollection.Length)];
            dialogueText.text += letter;
            textSound.PlayOneShot(textSound.clip);
            yield return new WaitForSeconds(delay);
        }
    }

    public void EndIntro()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
