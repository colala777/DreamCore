using UnityEngine;
using System.Collections;

public class R2CameraShake : MonoBehaviour
{
    // 公有参数配置
    public float shakeDuration = 0.5f;   // 抖动持续时间
    public float shakeMagnitude = 0.1f;  // 抖动幅度
    public float dampingSpeed = 1.0f;    // 抖动衰减速度

    private Vector3 initialPosition;     // 初始位置
    private float currentShakeDuration;  // 当前剩余抖动时间

    void Start()
    {
        // 记录初始位置
        initialPosition = transform.localPosition;
    }

    void Update()
    {
        Invoke("TriggerShake", 1);

        // 示例：按空格键触发抖动
        /*if (Input.GetKeyDown(KeyCode.Space))
        {
            TriggerShake();
        }*/
    }

    public void TriggerShake()
    {
        // 启动协程
        StartCoroutine(Shake());
    }

    IEnumerator Shake()
    {
        currentShakeDuration = shakeDuration;

        while (currentShakeDuration > 0)
        {
            // 生成随机偏移
            Vector3 shakeOffset = Random.insideUnitSphere * shakeMagnitude;
            transform.localPosition = initialPosition + shakeOffset;

            // 衰减时间
            currentShakeDuration -= Time.deltaTime * dampingSpeed;

            yield return null; // 等待下一帧
        }

        // 恢复初始位置
        transform.localPosition = initialPosition;
    }
}