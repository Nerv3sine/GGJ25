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
     
    private float mouseDirectionDegree = 0.0f; //getter below
    [SerializeField]
    private Vector3 mouseDirection = Vector3.zero;

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

        if ( BlowManager.INSTANCE.IsBlowing() && !popped)
        {
            Debug.Log("FILLING!!!");
            FillBubble();
        }

        if (!BlowManager.INSTANCE.IsBlowing()) 
        {
            Debug.Log("NOT FILLING!!!");
            EmptyBubble();
        }
        
    }

    private void LeanPlayer()
    {
        //swing value goes between -1, 1 on X axis.
       
        rb.AddForce(swing.action.ReadValue<float>(), 0, 0);
    }

    private void FillBubble()
    {
       
        rb.AddForce(mouseDirection * bubbleSize);
        bubbleSize += riseSpeed;
    }

    private void EmptyBubble()
    {
        rb.AddForce(0, -fallSpeed, 0);
        bubbleSize -= deflateSpeed;
    }

    private void BubbleMovement()
    {
        Debug.Log("loudness --> " + BlowManager.INSTANCE.loudness);
        Debug.Log("bubbleSize --> " + bubbleSize);

        // rb.AddForce(mouseDirection * bubbleSize);
        MouseInput();
        bubble.transform.localScale = new Vector3(1.1f*bubbleSize, 1.1f*bubbleSize, 1.1f*bubbleSize);

      

    }

    public void Grounded()
    {
        popped = false;
        bubbleS.BubbleAppear();

    }
    public void MouseInput()
    {
        Vector3 mousePos = Input.mousePosition.normalized;
        float mouseX = mousePos.x; //bounded between 0 - 1...

        Debug.Log(mouseX);

        float pi = Mathf.PI;

        float theta = Mathf.Lerp(3 * pi / 4, pi/4, mouseX);

        mouseDirectionDegree = theta; //setting mouse degree. 
        Vector3 originalDirection = new Vector3(Mathf.Cos(theta), Mathf.Sin(theta), 0);
        mouseDirection = originalDirection;
        //if we want z position movement later on, vec3 flips 90 degrees.
        Vector3 rotatedDirection = new Vector3(-originalDirection.z, originalDirection.y, originalDirection.x);

        Debug.DrawRay(Vector3.zero, originalDirection, Color.green);


    }
    public float GetMouseAngle()
    {
        //USE FOR UI DEVELOPMENT!!!!
        return mouseDirectionDegree;

    }
    

}
