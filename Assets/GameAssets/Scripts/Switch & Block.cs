using UnityEngine;

public class SwitchBlock : MonoBehaviour
{
    [SerializeField] GameObject block;
    BoxCollider aSwitch;
    MeshRenderer thisMesh;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        aSwitch = GetComponent<BoxCollider>();
        thisMesh = GetComponent<MeshRenderer>();
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.TryGetComponent<PlayerMovement>(out PlayerMovement player))
        {
            block.SetActive(false);
            
        }
    }

}
