using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class R1DoorCon : MonoBehaviour
{
    //public GameObject door;
    public GameObject doorTrigger;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public void Open()
    {
        this.GetComponent<Animator>().SetBool("isOpen", true);
        doorTrigger.SetActive(true);
    }
}
