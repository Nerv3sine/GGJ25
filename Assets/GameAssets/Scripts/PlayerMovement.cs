using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerMovement : MonoBehaviour
{
    [SerializeField] GameObject bubble;
    

    [SerializeField] InputActionReference blow;
    [SerializeField] InputActionReference swing;
    Rigidbody rb;

    public float bubbleSize = 1;
    public bool popped = false;
    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    
    void Update()
    {
        if (!popped)
        {
            bubbleSize = Mathf.Clamp(bubbleSize, 1.1f, 1.7f);
            BubbleMovement();
        }
        

        if (swing.action.inProgress)
        {
            LeanPlayer();
        }
        

    }

    private void FixedUpdate()
    {


        if (blow.action.inProgress)
        {
            FillBubble();
        }
        if (!blow.action.inProgress) 
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
        bubbleSize += .01f;
    }

    private void EmptyBubble()
    {
        bubbleSize -= .01f;
    }

    private void BubbleMovement()
    {
        rb.AddForce(0, bubbleSize, 0);
       
        bubble.transform.localScale = new Vector3(1*bubbleSize, 1*bubbleSize, 1*bubbleSize);
    }

    private void OnCollisionEnter(Collision collision)
    {
        popped = false;
    }


}
