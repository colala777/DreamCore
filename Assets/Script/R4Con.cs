using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class R4Con : MonoBehaviour
{
    public GameObject clockEvent;
    public GameObject clock;
    public GameObject schedule;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Camera.main == null) return;
        // 检测鼠标是否点击到方块
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                if (hit.transform.name == "Schedule") 
                {
                    clockEvent.SetActive(true);
                    clockEvent.GetComponent<AutoDeactivate>().delayTime = 8;
                    Camera.main.gameObject.SetActive(false);

                    clock.SetActive(false);
                    schedule.SetActive(true);
                }
                else if (hit.transform.name == "Clock") 
                {
                    clockEvent.SetActive(true);
                    clockEvent.GetComponent<AutoDeactivate>().delayTime = 120;
                    Camera.main.gameObject.SetActive(false);

                    clock.SetActive(true);
                    schedule.SetActive(false);
                }
            }
        }
    }
}
