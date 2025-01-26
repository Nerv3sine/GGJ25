using UnityEngine;

public class BubbleManager : MonoBehaviour
{
    public static BubbleManager INSTANCE;
    [SerializeField]
    float bubbleSize = 0.0f;
    
   

    private void FillBubble(in float bubbleS)
    {
        bubbleSize += .01f;
    }

    private void EmptyBubble(in float bubbleS)
    {
        bubbleSize -= .01f;
    }

}
