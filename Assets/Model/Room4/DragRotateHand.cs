using UnityEngine;

public class DragRotateHand : MonoBehaviour
{
    private Vector3 mouseStartPos;
    private float initialLocalAngle;

    void OnMouseDown()
    {
        // 记录初始鼠标位置和本地Z轴角度
        mouseStartPos = Input.mousePosition;
        initialLocalAngle = transform.localEulerAngles.z;
    }

    void OnMouseDrag()
    {
        // 计算鼠标横向移动差值
        Vector3 delta = Input.mousePosition - mouseStartPos;
        float angleDelta = delta.x * 0.5f; // 调整灵敏度

        // 绕本地Z轴旋转（关键代码）
        transform.localRotation = Quaternion.Euler(0, 0, initialLocalAngle + angleDelta);
    }
}