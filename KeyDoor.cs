using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyDoor : MonoBehaviour
{

    [SerializeField] private KeyPickup.KeyNumber keyNumber;
    public AudioSource doorOpen;

    public KeyPickup.KeyNumber GetKeyNumber()
    {
        return keyNumber;
    }

    public void OpenDoor()
    {
        doorOpen.Play();
        gameObject.SetActive(false);
    }


}
