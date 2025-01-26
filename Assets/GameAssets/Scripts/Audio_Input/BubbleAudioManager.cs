using UnityEngine;

public class BubbleAudioManager : MonoBehaviour
{
    public static BubbleAudioManager INSTANCE;
    private AudioClip microphoneClip;
    int sampleWindow = 64;
    private void OnEnable()
    {
        INSTANCE = FindFirstObjectByType<BubbleAudioManager>().GetComponent<BubbleAudioManager>();
        MicrophoneToAudio();
    }
    public float GetLoudnessFromAudioClip(int clipPos, AudioClip clip)
    {
        int startPostion = clipPos - sampleWindow;
        if(startPostion < 0)
        {
            return 0;
        }
        float[] waveData = new float[sampleWindow];
        clip.GetData(waveData, startPostion);

        //get loudness
        float totalLoudness = 0;
        for(int i = 0; i < sampleWindow; i++)
        {
            totalLoudness += Mathf.Abs(waveData[i]); //abs(sin [-1, 1])
          
        }
        return totalLoudness / sampleWindow;
    }
    public void MicrophoneToAudio()
    {
        //get first microphone in device list
        string microphoneName = Microphone.devices[0];
        microphoneClip = Microphone.Start(microphoneName, true, 20,AudioSettings.outputSampleRate);
    }
    public float GetLoudnessFromMicrophone()
    {
        return GetLoudnessFromAudioClip(Microphone.GetPosition(Microphone.devices[0]), microphoneClip);
    }
}
