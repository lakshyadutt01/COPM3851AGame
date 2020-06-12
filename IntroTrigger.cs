using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroTrigger : MonoBehaviour
{
    public IDialogue dialogue;

    public void TriggerIntro()
    {
        FindObjectOfType<IntroManager>().StartIntro(dialogue);
    }

    void Start()
    {
        //FindObjectOfType<IntroManager>().StartIntro(dialogue);
    }

}
