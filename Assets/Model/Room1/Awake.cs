using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Awake : MonoBehaviour
{
    public GameObject Room1;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("AwakeObj",5.5f);
    }

    // Update is called once per frame
    private void AwakeObj()
    {
        Room1.SetActive(true);
    }
}
