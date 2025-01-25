using UnityEngine;

public class Bubble : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    MeshRenderer bubble;
    SphereCollider sphereCollider;
    PlayerMovement PlayerMovement;

    private void Start()
    {
        bubble = GetComponent<MeshRenderer>();
        sphereCollider = GetComponent<SphereCollider>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        BubbleBurst();
    }

    public void BubbleBurst()
    {
        bubble.enabled = false;
        PlayerMovement.bubbleSize = 0;
        PlayerMovement.popped = true;
    }



}
