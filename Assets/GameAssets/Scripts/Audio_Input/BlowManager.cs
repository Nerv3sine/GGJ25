using UnityEngine;

public class BlowManager : MonoBehaviour
{
    //when character blows in blowAudioManager, bubble get larger using functions from bubble manager

    public static BlowManager INSTANCE;

    [SerializeField]
    private AudioSource source;
    [SerializeField]
    public Vector3 minScale, maxScale;

    public float loudness { get; set; } = 0.0f;

    [SerializeField]
    private float loudnessSensitivity = 1.5f;

    //public float threshold { get; set; } = 0.05f; //USE THIS VARIABLE FOR AUDIO THRESHOLD!!!!!!!!!!!! RAAAAAAHHHH

    public float threshold;
    [SerializeField] Threshold getthethreshold;

    public void OnEnable()
    {
        INSTANCE = FindFirstObjectByType<BlowManager>().GetComponent<BlowManager>();
        threshold = getthethreshold.threshold;

    }
    public void Update()
    {
        //Blow();
    }
    public bool IsBlowing()
    {
        loudness = BubbleAudioManager.INSTANCE.GetLoudnessFromMicrophone() * loudnessSensitivity;
        if (loudness < threshold)
        {
            return false;
        }
        return true;
    }
   
    private void Blow()
    {
        loudness = BubbleAudioManager.INSTANCE.GetLoudnessFromMicrophone() * loudnessSensitivity;
        if (loudness < threshold)
        {
            loudness = 0;
            return;
        }
        Debug.Log("loudness --> " + loudness);

        //bubble goes up based on intensity of the blow.



        //bubble moves up

        //direction move based on mouse input

    }


}
