using UnityEngine;

public class BlockCollision : MonoBehaviour
{
    private Block block;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        block = GetComponent<Block>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("RedSaber") || other.CompareTag("BlueSaber"))
        {
            block.OnHit(other.tag);
        }
    }
}
