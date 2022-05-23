using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{

    private static SoundManager instance = null;
    public static SoundManager Instance { get { return instance; } }

    [SerializeField] private AudioSource myAudioSource;

    [SerializeField] private AudioClip confirmSound;
    [SerializeField] private AudioClip clickSound;
    [SerializeField] private AudioClip denySound;
    [SerializeField] private AudioClip acceptSound;

    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    public void PlayClickSound()
    {
        myAudioSource.pitch = Random.Range(0.98f, 1.02f);
        myAudioSource.volume = 1f;
        myAudioSource.PlayOneShot(clickSound);
    }

    public void PlayConfirmSound()
    {
        myAudioSource.pitch = Random.Range(0.98f, 1.02f);
        myAudioSource.volume = 0.25f;
        myAudioSource.PlayOneShot(confirmSound);
    }

    public void PlayDenySound()
    {
        myAudioSource.pitch = Random.Range(0.98f, 1.02f);
        myAudioSource.volume = 0.75f;
        myAudioSource.PlayOneShot(denySound);
    }

    public void PlayAcceptSound()
    {
        myAudioSource.pitch = Random.Range(0.98f, 1.02f);
        myAudioSource.volume = 0.25f;
        myAudioSource.PlayOneShot(acceptSound);
    }

}
