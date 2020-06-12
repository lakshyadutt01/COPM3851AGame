using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyDoor : MonoBehaviour
{

    [SerializeField] private KeyPickup.KeyNumber keyNumber;
    public AudioSource doorOpen;
    public GameObject Door;
    public Animator animator;

    public KeyPickup.KeyNumber GetKeyNumber()
    {
        return keyNumber;
    }

    public void OpenDoor()
    {
        Door.GetComponent<BoxCollider2D>().enabled = false;
        animator.SetBool("Open", true);
        doorOpen.Play();
        //gameObject.SetActive(false);
    }


}
