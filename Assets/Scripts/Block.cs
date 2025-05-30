using UnityEngine;

public class Block : MonoBehaviour
{
    public enum BlockColor
    {
        Red,
        Blue
    }

    public enum BlockDirection
    {
        Up,
        Down,
        Left,
        Right
    }


    public BlockColor color;
    public BlockDirection direction;
    public GameObject hitEffectPrefab;
    public AudioClip hitSound;

    private bool isHit = false;


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("RedSaber") || other.CompareTag("BlueSaber"))
        {
            OnHit(other.tag);
        }
    }

    // Call when hit on the block by saber
    public void OnHit(string saberTag)
    {
        if (isHit) 
            return;

        bool colorMatched =
         (color == BlockColor.Red && saberTag == "RedSaber") ||
         (color == BlockColor.Blue && saberTag == "BlueSaber");

        if (colorMatched)
        {
            Debug.Log("Correct Color Hit!");

            if (hitEffectPrefab != null)
            {
                Instantiate(hitEffectPrefab, transform.position, Quaternion.identity);
            }


            if (hitSound != null)
            {
                AudioSource.PlayClipAtPoint(hitSound, transform.position);
            }


            ScoreManager.score++; // add score
            
            Destroy(gameObject);
        } else
        {
            Debug.Log("Wrong Color! Missed.");
            Destroy(gameObject);
        }

        isHit = true;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
