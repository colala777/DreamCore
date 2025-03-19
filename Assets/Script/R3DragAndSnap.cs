using UnityEngine;
using UnityEngine.SceneManagement;

public class R3DragAndSnap : MonoBehaviour
{
    [Header("吸附设置")]
    public Transform targetAttachPoint; // 身体的吸附点（拖拽赋值）
    public float snapDistance = 0.5f;    // 触发吸附的最小距离
    public float lerpSpeed = 10f;        // 吸附时的平滑速度

    private bool isDragging = false;
    private Vector3 offset;
    private float zDistance;

    public Camera DragCamera;

    void OnMouseDown()
    {
        // 计算物体与鼠标点击位置的偏移量
        zDistance = DragCamera.WorldToScreenPoint(transform.position).z;
        offset = transform.position - GetMouseWorldPos();
        isDragging = true;
    }

    void OnMouseDrag()
    {
        if (isDragging)
        {
            // 拖拽时更新位置
            transform.position = GetMouseWorldPos() + offset;
        }
    }

    void OnMouseUp()
    {
        isDragging = false;
        // 松开鼠标时检测是否可吸附
        TrySnapToTarget();
    }

    private void TrySnapToTarget()
    {
        if (targetAttachPoint == null) return;

        // 计算头部与身体吸附点的距离
        float distance = Vector3.Distance(transform.position, targetAttachPoint.position);

        if (distance <= snapDistance)
        {
            // 平滑吸附到目标位置和旋转
            StartCoroutine(SnapToPosition());
            Invoke("SceneJump", 4); //跳转到下一个场景 房间3
        }
    }

    private System.Collections.IEnumerator SnapToPosition()
    {
        // 禁用拖拽
        isDragging = false;

        // 平滑移动和旋转
        while (Vector3.Distance(transform.position, targetAttachPoint.position) > 0.01f)
        {
            transform.position = Vector3.Lerp(transform.position, targetAttachPoint.position, lerpSpeed * Time.deltaTime);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetAttachPoint.rotation, lerpSpeed * Time.deltaTime);
            yield return null;
        }

        // 吸附完成后固定位置
        transform.SetParent(targetAttachPoint);
        //GetComponent<Rigidbody>().isKinematic = true; // 若使用物理
    }

    private Vector3 GetMouseWorldPos()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = zDistance;
        return DragCamera.ScreenToWorldPoint(mousePos);
    }

    private void SceneJump()
    {
        SceneManager.LoadScene("Room4");
    }
}