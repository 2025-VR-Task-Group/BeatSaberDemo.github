using System.Threading;
using UnityEngine;

public class BlockMove : MonoBehaviour
{
    // speed (per second)
    [Header("Movement Settings")]
    public float speed = 10f;

    [Header("Destroy Settings")]
    private Vector3 moveDirection = Vector3.back;

    private float destroyZ = -5f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        CheckOutOfBounds();
    }

    private void Move()
    {
        transform.position += moveDirection * speed * Time.deltaTime;
    }

    private void CheckOutOfBounds()
    {
        if (transform.position.z < destroyZ)
        {
            Destroy(gameObject);
        }
    }
}
