using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyHolder : MonoBehaviour
{

    public AudioSource keySound;
    private List<KeyPickup.KeyNumber> keyList;

    private void Awake()
    {
        keyList = new List<KeyPickup.KeyNumber>();
    }

    public void AddKey(KeyPickup.KeyNumber keyNumber)
    {
        keyList.Add(keyNumber);
        Debug.Log("Added Key: " + keyNumber);
    }

    public void RemoveKey(KeyPickup.KeyNumber keyNumber)
    {
        keyList.Remove(keyNumber);
    }

    public bool ContainsKey(KeyPickup.KeyNumber keyNumber)
    {
        return keyList.Contains(keyNumber);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        KeyPickup key = collider.GetComponent<KeyPickup>();
        if (key != null)
        {
            keySound.Play();
            AddKey(key.GetKeyNumber());
            Destroy(key.gameObject);
        }

        KeyDoor keyDoor = collider.GetComponent<KeyDoor>();
        if (keyDoor != null)
        {
            if (ContainsKey(keyDoor.GetKeyNumber()))
            {
                RemoveKey(keyDoor.GetKeyNumber());
                keyDoor.OpenDoor();
            }
        }
    }
}
