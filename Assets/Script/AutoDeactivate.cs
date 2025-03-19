using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDeactivate : MonoBehaviour
{
    public float delayTime = 30f;
    private Camera mainCamera;

    public R1DoorCon R1DoorCon;

    // 在对象激活时启动协程（适用于重复激活/关闭的对象）
    private void OnEnable()
    {
        StartCoroutine(DeactivateAfterDelay());
        mainCamera = Camera.main;
    }

    IEnumerator DeactivateAfterDelay()
    {
        // 等待指定时间
        yield return new WaitForSeconds(delayTime);
        // 关闭当前对象
        gameObject.SetActive(false);
        mainCamera.gameObject.SetActive(true);

        if(this.name == "DrawEvent")
        {
            R1DoorCon.Open();
        }
    }
}
