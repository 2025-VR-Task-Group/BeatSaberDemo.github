using UnityEngine;
using System.Collections.Generic;

public class BlockSpawner : MonoBehaviour
{
    private List<GameObject> shuffledPrefabs = new List<GameObject>();
    private GameObject[] blockPrefabs;     // pull in prefabs
    public float interval = 1f;         // spawn interval
    private int currentIndex = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // auto scan the prefeb of block. Path: Assets/Resources/*Block
        blockPrefabs = Resources.LoadAll<GameObject>("Blocks");
        ShufflePrefabs();
        UnityEngine.Debug.Log("Length:" + blockPrefabs.Length);
        InvokeRepeating(nameof(SpawnBlock), 1f, interval);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ShufflePrefabs()
    {
        shuffledPrefabs.Clear();
        shuffledPrefabs.AddRange(blockPrefabs);

        // Fisher-Yates flsuh card algorithm
        for (int i = shuffledPrefabs.Count - 1; i > 0; i--)
        {
            int j = Random.Range(0, i + 1);
            var temp = shuffledPrefabs[i];
            shuffledPrefabs[i] = shuffledPrefabs[j];
            shuffledPrefabs[j] = temp;
        }

        currentIndex = 0;
    }

    void SpawnBlock()
    {
        if (shuffledPrefabs.Count == 0) return;

        UnityEngine.Debug.Log("CurrentIndex:" + currentIndex);

        GameObject selectedPrefab = shuffledPrefabs[currentIndex];
        Vector3 pos = new Vector3(UnityEngine.Random.Range(-4f, 4f), 1f, 30f);
        Instantiate(selectedPrefab, pos, selectedPrefab.transform.rotation);


        currentIndex++;

        if (currentIndex >= shuffledPrefabs.Count)
            ShufflePrefabs();
    }
}
