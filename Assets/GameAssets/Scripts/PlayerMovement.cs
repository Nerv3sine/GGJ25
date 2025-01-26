using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerMovement : MonoBehaviour
{
    [SerializeField] GameObject bubble;
    Bubble bubbleS;
    //[SerializeField] BlowManager bubbleManager;

    [SerializeField] InputActionReference blow;
    [SerializeField] InputActionReference swing;
    Rigidbody rb;

    public float bubbleSize = 1;
    public bool popped = false;
    [SerializeField] float riseSpeed = .02f;
    [SerializeField] float deflateSpeed = .01f;
    [SerializeField] float fallSpeed = 0.5f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        bubbleS = bubble.GetComponent<Bubble>();
    }

    
    void Update()
    {
        if (!popped)
        {
            bubbleSize = Mathf.Clamp(bubbleSize, 0.7f, 1.7f);
            BubbleMovement();
        }
        

        if (swing.action.inProgress)
        {
            LeanPlayer();
        }
        

    }

    private void FixedUpdate()
    {
        //bubbleManager.loudness > bubbleManager.threshold ||
        //bubbleManager.loudness < bubbleManager.threshold &&

        if (  blow.action.inProgress)
        {
            FillBubble();
        }
        if ( !blow.action.inProgress) 
        {
            EmptyBubble();
        }
    }

    private void LeanPlayer()
    {
        rb.AddForce(swing.action.ReadValue<float>(), 0, 0);
    }

    private void FillBubble()
    {
        rb.AddForce(0, bubbleSize, 0);
        bubbleSize += riseSpeed;
    }

    private void EmptyBubble()
    {
        rb.AddForce(0, -fallSpeed, 0);
        bubbleSize -= deflateSpeed;
    }

    private void BubbleMovement()
    {
        rb.AddForce(0, bubbleSize, 0);
       
        bubble.transform.localScale = new Vector3(1.1f*bubbleSize, 1.1f*bubbleSize, 1.1f*bubbleSize);
    }

    public void Grounded()
    {
        popped = false;
        bubbleS.BubbleAppear();

    }

}
