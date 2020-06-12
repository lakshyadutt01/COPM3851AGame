using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;

    public bool inRange;
    public bool playing = false;
    private bool actionComplete = false;

    private void Update()
    {
        if (inRange == true && Input.GetKeyDown(KeyCode.E) && playing == false)
        {
            if (actionComplete == false)
            {
                TriggerDialogue();
                playing = true;
                actionComplete = true;
            }
        }

        if (inRange == true && Input.GetKeyDown(KeyCode.E) && playing == true)
        {
            if (actionComplete == false)
            {
                playing = false;
                actionComplete = true;
            }
        }

        actionComplete = false;

        if (playing == true && inRange == false)
        {
            playing = false;
            EndDialogue();
        }
    }

    public void TriggerDialogue()
    {
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
    }

    public void EndDialogue()
    {
        FindObjectOfType<DialogueManager>().EndDialogue();
    }

    private void OnTriggerStay2D(Collider2D plyr)
    {
        if (plyr.tag == "Player")
        {
            inRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D plyr)
    {
        if (plyr.tag == "Player")
        {
            inRange = false;
        }
    }


}
