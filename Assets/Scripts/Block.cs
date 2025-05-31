using UnityEngine;
using System.Collections.Generic;

public class Block : MonoBehaviour
{
    public enum BlockColor { Red, Blue }
    public enum BlockDirection { Up, Down, Left, Right }

    public BlockColor color;
    public BlockDirection direction;
    private bool isHit = false;

    [Header("Red HalfBlock Prefabs")]
    public GameObject redHalfBlockLeftPrefab;
    public GameObject redHalfBlockRightPrefab;
    public GameObject redHalfBlockTopPrefab;
    public GameObject redHalfBlockBottomPrefab;

    [Header("Blue HalfBlock Prefabs")]
    public GameObject blueHalfBlockLeftPrefab;
    public GameObject blueHalfBlockRightPrefab;
    public GameObject blueHalfBlockTopPrefab;
    public GameObject blueHalfBlockBottomPrefab;

    // 统一存储每个颜色所有方向的 prefab 对应表
    private Dictionary<BlockColor, Dictionary<BlockDirection, (GameObject first, GameObject second)>> prefabMap;

    public float splitOffset = 0.3f;
    public AudioClip hitSound;
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.spatialBlend = 0f;

        prefabMap = new Dictionary<BlockColor, Dictionary<BlockDirection, (GameObject, GameObject)>>()
        {
            {
                BlockColor.Red, new Dictionary<BlockDirection, (GameObject, GameObject)>()
                {
                    { BlockDirection.Left, (redHalfBlockLeftPrefab, redHalfBlockRightPrefab) },
                    { BlockDirection.Right, (redHalfBlockRightPrefab, redHalfBlockLeftPrefab) },
                    { BlockDirection.Up, (redHalfBlockTopPrefab, redHalfBlockBottomPrefab) },
                    { BlockDirection.Down, (redHalfBlockBottomPrefab, redHalfBlockTopPrefab) }
                }
            },
            {
                BlockColor.Blue, new Dictionary<BlockDirection, (GameObject, GameObject)>()
                {
                    { BlockDirection.Left, (blueHalfBlockLeftPrefab, blueHalfBlockRightPrefab) },
                    { BlockDirection.Right, (blueHalfBlockRightPrefab, blueHalfBlockLeftPrefab) },
                    { BlockDirection.Up, (blueHalfBlockTopPrefab, blueHalfBlockBottomPrefab) },
                    { BlockDirection.Down, (blueHalfBlockBottomPrefab, blueHalfBlockTopPrefab) }
                }
            }
        };
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("RedSaber") || other.CompareTag("BlueSaber"))
        {
            OnHit(other.tag);
        }
    }

    public void OnHit(string saberTag)
    {
        if (isHit) return;
        isHit = true;

        bool colorMatched =
            (color == BlockColor.Red && saberTag == "RedSaber") ||
            (color == BlockColor.Blue && saberTag == "BlueSaber");

        if (!colorMatched)
        {
            Debug.Log("Wrong Color! Missed.");
            Destroy(gameObject);
            return;
        }

        if (hitSound != null && audioSource != null)
        {
            AudioSource.PlayClipAtPoint(hitSound, transform.position, 1.0f);
        }

        Vector3 position = transform.position;

        // 先确定切割轴
        Vector3 cutAxis = (direction == BlockDirection.Left || direction == BlockDirection.Right)
                          ? transform.up : transform.right;

        var (firstPrefab, secondPrefab) = prefabMap[color][direction];

        // 生成第一半块
        Vector3 firstPos = position + cutAxis * splitOffset;
        GameObject objFirst = Instantiate(firstPrefab, firstPos, Quaternion.LookRotation(-cutAxis, Vector3.up));
        HalfBlock halfFirst = objFirst.GetComponent<HalfBlock>();
        if (halfFirst != null)
            halfFirst.flyDirection = (cutAxis + Vector3.up * 0.5f).normalized;

        // 生成第二半块
        Vector3 secondPos = position - cutAxis * splitOffset;
        GameObject objSecond = Instantiate(secondPrefab, secondPos, Quaternion.LookRotation(cutAxis, Vector3.up));
        HalfBlock halfSecond = objSecond.GetComponent<HalfBlock>();
        if (halfSecond != null)
            halfSecond.flyDirection = (-cutAxis + Vector3.up * 0.5f).normalized;

        ScoreManager.score++;
        Destroy(gameObject);
    }
}
