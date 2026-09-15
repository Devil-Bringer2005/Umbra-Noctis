using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("----------AudioSource----------")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;

    [Header("----AudioClip------")]
    public AudioClip background;
    public AudioClip battleMusic;
    public AudioClip Axecut;
    public AudioClip SkeletonHit;
    public AudioClip GemCollected;
    public AudioClip MenuSelection;
    public AudioClip BigJaack;

    private void Start()
    {
        musicSource.clip = background;
        musicSource.Play();
    }

    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }

    public void ChangeToBattleMusic()
    {
        // Stop current music
        musicSource.Stop();
        // Change clip to battle music
        musicSource.clip = battleMusic;
        // Play the new clip
        musicSource.Play();
    }

    public void ChangeToBackgroundMusic()
    {
        // Stop current music
        musicSource.Stop();
        // Change clip to background music
        musicSource.clip = background;
        // Play the new clip
        musicSource.Play();
    }
}
