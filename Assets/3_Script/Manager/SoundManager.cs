using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoSingleton<SoundManager>
{
    public AudioMixer mixer;
    public AudioSource backgroundSound;
    public AudioClip[] soundList;
    public void OnBackground(int number)
    {
        BackGroundSoundPlay(soundList[number]);
    }
    public void BGSoundVoloume(float val)
    {
        mixer.SetFloat("BackGround",Mathf.Log(val)*20);
        
    }
    public void SFXVoloume(float val)
    {
        mixer.SetFloat("SFX",Mathf.Log(val)*20);
    }
    public void SFXPlay(string sfxName,AudioClip clip)
    {
        GameObject gameObject = new GameObject(sfxName + "Sound");
        AudioSource audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.outputAudioMixerGroup = mixer.FindMatchingGroups("SFX")[0];
        audioSource.clip = clip;
        audioSource.Play();

        Destroy(gameObject,clip.length);
    }
    public void BackGroundSoundPlay(AudioClip clip)
    {
        backgroundSound.outputAudioMixerGroup = mixer.FindMatchingGroups("BackGround")[0];
        backgroundSound.clip = clip;
        backgroundSound.loop = true;
        backgroundSound.volume = 0.1f;
        backgroundSound.Play();
    }
}
