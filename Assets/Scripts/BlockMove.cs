using System.Threading;
using UnityEngine;

public class BlockMove : MonoBehaviour
{
    // speed (per second)
    public float speed = 10f;


    private Vector3 moveDirection = Vector3.back;

    private float destroyZ = -5f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += moveDirection * speed * Time.deltaTime;
        
        if (transform.position.z < destroyZ)
        {
            Destroy(gameObject);
        }
    }
}
