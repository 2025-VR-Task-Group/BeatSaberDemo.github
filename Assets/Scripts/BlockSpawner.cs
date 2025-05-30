using UnityEngine;

public class BlockSpawner : MonoBehaviour
{
    private GameObject[] blockPrefabs;     // pull in prefabs
    public float interval = 1f;         // spawn interval

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // auto scan the prefeb of block. Path: Assets/Resources/*Block
        blockPrefabs = Resources.LoadAll<GameObject>("Blocks");

        InvokeRepeating(nameof(SpawnBlock), 1f, interval);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnBlock()
    {
        if (blockPrefabs == null || blockPrefabs.Length == 0)
        {
            Debug.LogWarning("Block Prefab is NULL! Cannot spawn.");
            return;
        }

        // select the prefeb randomly 
        int randomIndex = UnityEngine.Random.Range(0, blockPrefabs.Length);
        GameObject selectedPrefab = blockPrefabs[randomIndex];

        // random position
        Vector3 pos = new Vector3(Random.Range(-4f, 4f), 1f, 30f);

        // instance
        Instantiate(selectedPrefab, pos, Quaternion.identity);
    }
}
