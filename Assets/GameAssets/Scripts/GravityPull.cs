using UnityEngine;

public class GravityPull : MonoBehaviour
{
    [SerializeField] Transform player;
    Rigidbody playerBody;
    [SerializeField] float range;
    [SerializeField] float intensity;
    [SerializeField] float distanceToPlayer;
    Vector2 pullForce;

    void Start()
    {
        playerBody = player.GetComponent<Rigidbody>();
    }

    void Update()
    {
        distanceToPlayer = Vector2.Distance(player.position, transform.position);
        if (distanceToPlayer <= range)
        {
            pullForce = (transform.position - player.position).normalized / distanceToPlayer * intensity;
            playerBody.AddForce(pullForce, ForceMode.Force);

        }

    }
}