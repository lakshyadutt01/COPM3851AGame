using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldSound : MonoBehaviour
{

    public AudioSource blockSound;
    public AudioClip[] blockSoundsCollection;

    void Start()
    {
        blockSound = GetComponent<AudioSource>();
    }


    public void TakeDamage(int damage)
    {
        blockSound.clip = blockSoundsCollection[Random.Range(0, blockSoundsCollection.Length)];
        blockSound.PlayOneShot(blockSound.clip);
    }
}
