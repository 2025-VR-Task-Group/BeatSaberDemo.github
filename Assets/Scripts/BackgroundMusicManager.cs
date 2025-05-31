
// using UnityEngine;
// using TMPro;


// public class ScoreManager : MonoBehaviour
// {
//     public static int score = 0;
//     public TextMeshProUGUI scoreText;

//     // Start is called once before the first execution of Update after the MonoBehaviour is created
//     void Start()
//     {
        
//     }

//     // Update is called once per frame
//     void Update()
//     {
//         scoreText.text = "Score " + score;
//     }
// }




using UnityEngine;

public class BackgroundMusicManager : MonoBehaviour
{
    [Header("Background Music")]
    public AudioClip backgroundMusicClip;

    private AudioSource audioSource;

    void Start()
    {
        // 动态添加 AudioSource
        audioSource = gameObject.AddComponent<AudioSource>();

        // 绑定音乐
        audioSource.clip = backgroundMusicClip;

        // 设置循环
        audioSource.loop = true;

        // 播放
        audioSource.Play();
    }
}
