using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Move")]
    public float moveSpeed = 5f;   // 移动速度
    private float rotationSpeed = 720f; 
    private float gravity = 9.81f;

    private CharacterController controller;
    private Vector3 velocity;
    private Animator animator;

    [Header("Interact")]
    public GameObject currentObj;

    public GameObject DrawEvent;
    public GameObject DairyEvent;
    private Camera mainCamera;

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        HandleMovement();
        HandleInteraction();
    }
    private void HandleMovement()
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

    private void HandleInteraction()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (currentObj == null) return;

            if (currentObj.name == "Pen")
            {
                //Debug.Log("执行画笔的交互逻辑");
                Destroy(currentObj.GetComponent<SphereCollider>());
                currentObj.transform.Find("Prompt").gameObject.SetActive(false);
                DrawEvent.SetActive(true);
                mainCamera.gameObject.SetActive(false);
            }
            else if (currentObj.name == "Diary")
            {
                //Debug.Log("执行日记本的交互逻辑");
                Destroy(currentObj.GetComponent<SphereCollider>());
                currentObj.transform.Find("Prompt").gameObject.SetActive(false);
                DairyEvent.SetActive(true);
                mainCamera.gameObject.SetActive(false);
            }
            else if (currentObj.name == "Head_toy")
            {
                Debug.Log("执行Head_toy的交互逻辑");
            }
            else if (currentObj.name == "Body_toy")
            {
                Debug.Log("执行Body_toy的交互逻辑");
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        currentObj = other.gameObject;
        if (other.gameObject.name == "Pen")
        {            
            other.transform.Find("Prompt").gameObject.SetActive(true);
        }
        if (other.gameObject.name == "Diary")
        {
            other.transform.Find("Prompt").gameObject.SetActive(true);
        }
        if (other.gameObject.name == "Body_toy")
        {
            other.transform.Find("Prompt").gameObject.SetActive(true);
        }
        if (other.gameObject.name == "Head_toy")
        {
            other.transform.Find("Prompt").gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        currentObj = null;
        if (other.gameObject.name == "Pen")
        {
            other.transform.Find("Prompt").gameObject.SetActive(false);
        }
        if (other.gameObject.name == "Diary")
        {
            other.transform.Find("Prompt").gameObject.SetActive(false);
        }
        if (other.gameObject.name == "Body_toy")
        {
            other.transform.Find("Prompt").gameObject.SetActive(false);
        }
        if (other.gameObject.name == "Head_toy")
        {
            other.transform.Find("Prompt").gameObject.SetActive(false);
        }
    }
}
