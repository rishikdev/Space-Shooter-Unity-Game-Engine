using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayEnemyDestructionSound : MonoBehaviour
{
    [SerializeField] private AudioClip smallSpaceshipExplosionAudioClip;
    [SerializeField] private AudioClip largeSpaceshipExlopsionAudioClip;
    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlaySmallSpaceshipDestructionSound()
    {
        audioSource.PlayOneShot(smallSpaceshipExplosionAudioClip);
    }

    public void PlayLargeSpaceshipDestructionSound()
    {
        audioSource.volume = 0.1f;
        audioSource.PlayOneShot(largeSpaceshipExlopsionAudioClip);
    }
}
