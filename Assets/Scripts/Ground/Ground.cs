using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private List<AudioClip> clipList;

    private void OnTriggerEnter2D(Collider2D other)
    {
        AudioClip clipToPlay;

        switch (other.gameObject.tag)
        {
            case "Bag":
                clipToPlay = clipList[0];
                break;
            case "Anvil":
                clipToPlay = clipList[1];
                break;
            case "Flower":
                clipToPlay = clipList[2];
                break;
            case "Fish":
                clipToPlay = clipList[3];
                break;
            default:
                return;
        }

        audioSource.clip = clipToPlay;
        audioSource.Play();
    }
}
