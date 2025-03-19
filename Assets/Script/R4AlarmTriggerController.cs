using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class R4AlarmTriggerController : MonoBehaviour
{
    [Header("目标区域")]
    public Collider hourTargetZone;   // 时针目标区域
    public Collider minuteTargetZone; // 分针目标区域

    [Header("天空盒设置")]
    public Material targetSkybox;     // 目标天空盒材质

    private bool isHourInZone = false;
    private bool isMinuteInZone = false;

    private Animator playerAni;
    public GameObject mainCamera;

    private AudioSource ClockAudio;

    private void Start()
    {
        playerAni = GameObject.FindWithTag("Player").GetComponent<Animator>();
        ClockAudio = GameObject.Find("Clock").GetComponent<AudioSource>();
    }

    void Update()
    {
        // 当两个指针同时位于目标区域时触发
        if (isHourInZone && isMinuteInZone)
        {
            TriggerSkyboxChange();

            this.gameObject.SetActive(false);
            playerAni.SetBool("isWake", true);
            mainCamera.SetActive(true);
            ClockAudio.Play();//闹钟铃响

            // 重置状态防止重复触发（可选）
            isHourInZone = false;
            isMinuteInZone = false;
        }
    }

    // 时针
    public void OnHourEnterZone()
    {
        isHourInZone = true;
    }
    public void OnHourExitZone()
    {
        isHourInZone = false;
    }

    // 分针
    public void OnMinuteEnterZone()
    {
        isMinuteInZone = true;
    }
    public void OnMinuteExitZone()
    {
        isMinuteInZone = false;
    }

    private void TriggerSkyboxChange()
    {
        RenderSettings.skybox = targetSkybox;
        //DynamicGI.UpdateEnvironment();
        Debug.Log("天空盒已切换！");
    }
}
