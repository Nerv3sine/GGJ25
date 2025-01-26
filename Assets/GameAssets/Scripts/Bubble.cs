using UnityEngine;

public class Bubble : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    MeshRenderer bubble;
    SphereCollider sphereCollider;
    [SerializeField] PlayerMovement PlayerMovement;

    private void Start()
    {
        bubble = GetComponent<MeshRenderer>();
        sphereCollider = GetComponent<SphereCollider>();

    }

    

    public void BubbleBurst()
    {
        bubble.enabled = false;
        PlayerMovement.bubbleSize = 0;
        PlayerMovement.popped = true;
        sphereCollider.enabled = false;
    }

    public void BubbleAppear()
    {
        bubble.enabled = true;
        sphereCollider.enabled = true;
    }

    private void OnTriggerEnter(Collider other)
    {
       
            BubbleBurst();
        
    }


}
