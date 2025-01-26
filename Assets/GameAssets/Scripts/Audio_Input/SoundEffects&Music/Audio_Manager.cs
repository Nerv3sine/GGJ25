using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audio_Manager : MonoBehaviour
{
    public static Audio_Manager INSTANCE;
    [SerializeField]
    SoundScriptableObject soundScriptableObject;
    public AudioSource musicAudioSource;
    public AudioSource sfxAudioSource;

    public AudioClip music { get; private set; }
    public AudioClip fall { get; private set; }
    public AudioClip victory { get; private set; }

    public void OnEnable()
    {
        INSTANCE = FindFirstObjectByType<Audio_Manager>().GetComponent<Audio_Manager>();
    }
    private void Start()
    {
        music = soundScriptableObject.musicLoop;
        fall = soundScriptableObject.fallingSound;
        victory = soundScriptableObject.victorySound;
        musicAudioSource.loop = true;

        PlayLoopAudio(music);
    }
    public void PlayLoopAudio(AudioClip audioClip)
    {
        //audio is looping...
        musicAudioSource.clip = audioClip;
        musicAudioSource.Play();
    }
    public IEnumerator CutMusicPlaySFXAudio(float audioCutWaitTime,float easingInMusicVolumeInSeconds, AudioClip audioClip)
    {
        float volume = musicAudioSource.volume;
        musicAudioSource.Stop();
        sfxAudioSource.clip = audioClip;
        sfxAudioSource.Play();

        float i = 0;
        yield return new WaitForSeconds(audioCutWaitTime);
        musicAudioSource.volume = 0;
        musicAudioSource.Play();
        while (i < 1)
        {
            i += Time.deltaTime / easingInMusicVolumeInSeconds;
            musicAudioSource.volume = Mathf.Lerp(0, volume, i);
            yield return new WaitForFixedUpdate();
        }
        Debug.Log("done");
        musicAudioSource.volume = volume;
        yield return null;


    }
    public void PlayAudio(AudioClip audioClip)
    {
        sfxAudioSource.clip = audioClip;
        sfxAudioSource.Play();

    }

    public void StopAudio(ref AudioSource audioClip)
    {
        audioClip.Stop();
    }
    

}
