using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sound : MonoBehaviour
{
    public AudioSource audioSource;

    public void playClip()
    {
        audioSource.Play();
    }
}
