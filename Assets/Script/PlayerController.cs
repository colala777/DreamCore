using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;   // 移动速度
    private float rotationSpeed = 720f; 
    private float gravity = 9.81f;

    private CharacterController controller;
    private Vector3 velocity;
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // 获取玩家输入
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        // 移动向量
        Vector3 moveDirection = new Vector3(horizontal, 0f, vertical);
        if (moveDirection.magnitude > 0.1f) // 防止轻微输入干扰
        {
            // 角色朝向的旋转
            float targetAngle = Mathf.Atan2(moveDirection.x, moveDirection.z) * Mathf.Rad2Deg;
            Quaternion targetRotation = Quaternion.Euler(0, targetAngle, 0);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

            // 移动
            controller.Move(moveDirection.normalized * moveSpeed * Time.deltaTime);

            animator.SetBool("isWalk", true);
        }
        else
        {
            animator.SetBool("isWalk", false);
        }

        // 模拟重力
        if (!controller.isGrounded)
        {
            velocity.y -= gravity * Time.deltaTime;
        }
        else
        {
            velocity.y = -1f; 
        }

        // 执行移动
        controller.Move(velocity * Time.deltaTime);
    }
}
