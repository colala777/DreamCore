using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseControl : MonoBehaviour
{
    public float minY = 0f;    // Y����Сֵ
    public float maxY = 5f;    // Y�����ֵ

    private bool isDragging = false;
    private float offsetY;    // ���ͷ����ƫ����

    void Update()
    {
        // �������Ƿ���������
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                if (hit.transform == transform) // ���������ǵ�ǰ����
                {
                    isDragging = true;

                    // ��¼ƫ������ȷ��������ƽ��
                    offsetY = transform.position.y - hit.point.y;
                }
            }
        }

        // ��������
        if (isDragging && Input.GetMouseButton(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                float targetY = Mathf.Clamp(hit.point.y + offsetY, minY, maxY);

                transform.position = new Vector3(
                    transform.position.x,
                    targetY,
                    transform.position.z
                );

            }
        }

        // �ɿ���ֹ꣬ͣ����
        if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Pos1")
        {
            Debug.Log("ͼƬ1");
        }
        else if (other.gameObject.name == "Pos2")
        {
            Debug.Log("ͼƬ2");
        }
        else if (other.gameObject.name == "Pos3")
        {
            Debug.Log("ͼƬ3");
        }
    }
}