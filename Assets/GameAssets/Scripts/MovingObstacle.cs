using UnityEngine;

public class MovingObstacle : MonoBehaviour
{

    Vector3 startPosition;
    [SerializeField] Vector3 endPosition;
    float aPosition;
    Rigidbody rb;
    [SerializeField] float time;

    [SerializeField] bool BackandForth = false;
    bool reverse = false;
   

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startPosition = gameObject.transform.position;
        rb = GetComponent<Rigidbody>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!reverse) { time += (Time.deltaTime / 10); }
        else if (reverse) { time -= (Time.deltaTime / 10); }
        
        aPosition = Mathf.Lerp(startPosition.x, endPosition.x, time);
        this.gameObject.transform.position = new Vector3(aPosition, this.gameObject.transform.position.y, this.gameObject.transform.position.z);
        if(time >= 1) 
        {
            if (BackandForth) {
                 reverse = true; }
            else if (!BackandForth) { time = 0; }
        }
        if (reverse && time <= 0) { reverse = false; }


    }

    private void OnDrawGizmos()
    {
        startPosition = gameObject.transform.position;
        Gizmos.color = Color.red;
        Gizmos.DrawLine(startPosition, endPosition);
    }

}
