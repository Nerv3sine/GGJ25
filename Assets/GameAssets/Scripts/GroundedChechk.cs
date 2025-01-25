using UnityEngine;

public class GroundedChechk : MonoBehaviour
{
    SphereCollider sphereCollider;
    [SerializeField] PlayerMovement PlayerMovement;

    private void Start()
    {
        sphereCollider = GetComponent<SphereCollider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        PlayerMovement.Grounded();
    }
}
