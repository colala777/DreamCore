using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorScene : MonoBehaviour
{
    // 要传送到的场景名称
    public string sceneName;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        // 检查碰撞的对象是否是玩家（或你想要检测的对象）

        if (other.CompareTag("Player"))
        {
            // 切换到指定的场景
            SceneManager.LoadScene(sceneName);
        }
    }
}
