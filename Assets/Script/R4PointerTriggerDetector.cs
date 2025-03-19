using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class R4PointerTriggerDetector : MonoBehaviour
{
    [Header("目标区域类型")]
    public bool isHourPointer; // 勾选表示这是时针

    private R4AlarmTriggerController controller;
    // Start is called before the first frame update
    void Start()
    {
        // 获取主控制器（确保挂载到闹钟主体）
        controller = FindObjectOfType<R4AlarmTriggerController>();
    }

    void OnTriggerEnter(Collider other)
    {
        // 根据指针类型触发不同事件
        if (isHourPointer && other == controller.hourTargetZone)
        {
            controller.OnHourEnterZone();
        }
        else if (!isHourPointer && other == controller.minuteTargetZone)
        {
            controller.OnMinuteEnterZone();
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (isHourPointer && other == controller.hourTargetZone)
        {
            controller.OnHourExitZone();
        }
        else if (!isHourPointer && other == controller.minuteTargetZone)
        {
            controller.OnMinuteExitZone();
        }
    }
}
