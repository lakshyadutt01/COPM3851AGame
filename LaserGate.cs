using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserGate : MonoBehaviour
{

    public GameObject Laser;
    Animator leverAnimator, laserAnimator;
    public bool on = false;
    public bool inRange;
    private bool actionComplete = false;
    //public AudioSource leverSound;

    private void Start()
    {
        leverAnimator = this.GetComponent<Animator>();
        laserAnimator = Laser.GetComponent<Animator>();
    }

    private void Update()
    {
        if (inRange == true && Input.GetKeyDown(KeyCode.E) && on == false)
        {
            if (actionComplete == false)
            {
                leverAnimator.SetBool("On", true);
                laserAnimator.SetBool("Open", true);
                //leverSound.Play();
                on = true;
                Laser.GetComponent<BoxCollider2D>().enabled = false;
                actionComplete = true;
            }
        }

        if (inRange == true && Input.GetKeyDown(KeyCode.E) && on == true)
        {
            if (actionComplete == false)
            {
                leverAnimator.SetBool("On", false);
                laserAnimator.SetBool("Open", false);
                on = false;
                Laser.GetComponent<BoxCollider2D>().enabled = true;
                actionComplete = true;
            }
        }

        actionComplete = false;
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
