using UnityEngine;
[CreateAssetMenu(fileName = "SoundData", menuName = "ScriptableObjects/SoundData", order = 1)]
public class SoundScriptableObject : ScriptableObject
{
    public AudioClip musicLoop, fallingSound, victorySound;
}
