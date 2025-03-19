using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class R0ClickToShowText : MonoBehaviour
{
    [Header("文本框数组")]
    public TMP_Text[] textBoxes;  // 拖拽赋值 4 个 Text 对象

    private int currentIndex = -1;
    private bool isLastTextShown = false; // 标记是否已显示最后一个文本框

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (isLastTextShown)
            {
                // 跳转到新场景
                SceneManager.LoadScene("Room1");
            }
            else
            {
                ShowNextText();
            }
        }
    }

    private void ShowNextText()
    {
        // 隐藏当前文本框
        if (currentIndex >= 0 && currentIndex < textBoxes.Length)
        {
            textBoxes[currentIndex].gameObject.SetActive(false);
        }

        currentIndex++;

        if (currentIndex < textBoxes.Length)
        {
            // 显示下一个文本框
            textBoxes[currentIndex].gameObject.SetActive(true);
        }
        else
        {
            isLastTextShown = true;
        }
    }

    // 可选：初始化时隐藏所有文本框
    void Start()
    {
        foreach (TMP_Text text in textBoxes)
        {
            text.gameObject.SetActive(false);
        }
    }
}
