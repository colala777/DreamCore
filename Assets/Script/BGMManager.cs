using UnityEngine;
using UnityEngine.SceneManagement;

public class BGMManager : MonoBehaviour
{
    public static BGMManager Instance; // 单例

    private AudioSource audioSource;

    private void Awake()
    {

        // 确保只有一个实例存在
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // 跨场景保留
            audioSource = GetComponent<AudioSource>();
            audioSource.Play(); // 自动播放
        }
        else
        {
            Destroy(gameObject); // 销毁重复实例
        }
    }

    private void Update()
    {
        if (SceneManager.GetActiveScene().name == "Room4")
        {
            Destroy(gameObject); // 最后一个场景销毁bgm01
        }
    }

    // 切换BGM（可选）
    public void ChangeBGM(AudioClip newClip)
    {
        audioSource.Stop();
        audioSource.clip = newClip;
        audioSource.Play();
    }
}