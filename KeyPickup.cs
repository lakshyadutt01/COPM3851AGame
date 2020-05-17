using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyPickup : MonoBehaviour
{
    [SerializeField] private KeyNumber keyType;

    public enum KeyNumber
    {
        One,
        Two,
        Three
    }

    public KeyNumber GetKeyNumber()
    {
        return keyType;
    }

 
}
