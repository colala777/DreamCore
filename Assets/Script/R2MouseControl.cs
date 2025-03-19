using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class R2MouseControl : MonoBehaviour
{
    public float minY = 0f;    // Y轴最小值
    public float maxY = 5f;    // Y轴最大值

    private bool isDragging = false;
    private float offsetY;    // 鼠标和方块的偏移量

    public GameObject Screen;
    public Texture2D newTexture1, newTexture2, newTexture3;

    public AudioClip TvAudio;
    private AudioSource TvAudioSource;

    private void Start()
    {
        TvAudioSource = GetComponent<AudioSource>();
    }
    void Update()
    {
        // 检测鼠标是否点击到方块
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                if (hit.transform == transform) // 如果点击的是当前方块
                {
                    isDragging = true;

                    // 记录偏移量，确保滑动更平滑
                    offsetY = transform.position.y - hit.point.y;
                }
            }
        }

        // 滑动控制
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

        // 松开鼠标，停止滑动
        if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Material material = Screen.GetComponent<Renderer>().material;
        string texturePropertyName = "_MainTex";
        if (other.gameObject.name == "Pos1")
        {
            //Debug.Log("图片1");
            TvAudioSource.PlayOneShot(TvAudio);
            material.SetTexture(texturePropertyName, newTexture1);
        }
        else if (other.gameObject.name == "Pos2")
        {
            //Debug.Log("图片2");
            TvAudioSource.PlayOneShot(TvAudio);
            material.SetTexture(texturePropertyName, newTexture2);
        }
        else if (other.gameObject.name == "Pos3")
        {
            //Debug.Log("图片3");
            TvAudioSource.PlayOneShot(TvAudio);
            material.SetTexture(texturePropertyName, newTexture3);
            Invoke("SceneJump", 5); //跳转到下一个场景 房间3
            
        }
    }

    private void SceneJump()
    {
        SceneManager.LoadScene("Room3");
    }
}