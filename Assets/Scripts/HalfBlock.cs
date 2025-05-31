using UnityEngine;

public class HalfBlock : MonoBehaviour
{
    public float flyForce = 3f;
    public float lifeTime = 2f;
    public Vector3 flyDirection = Vector3.zero;


    private Rigidbody rb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.AddForce(flyDirection * flyForce, ForceMode.Impulse);
        }

        Destroy(gameObject, lifeTime);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
