using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalCameraController : MonoBehaviour
{
    public Transform portal;      // 当前传送门
    public Transform targetPortal;// 目标传送门
    public Camera portalCamera;   // 传送门专用相机
    void Update()
    {
        // 计算相对位置
        Vector3 playerOffset = Camera.main.transform.position - portal.position;
        portalCamera.transform.position = targetPortal.position + playerOffset;

        // 计算相对旋转
        float angularDiff = Quaternion.Angle(portal.rotation, targetPortal.rotation);
        Quaternion rotDiff = Quaternion.AngleAxis(angularDiff, Vector3.up);
        Vector3 newDirection = rotDiff * Camera.main.transform.forward;
        portalCamera.transform.rotation = Quaternion.LookRotation(newDirection, Vector3.up);
    }
}
