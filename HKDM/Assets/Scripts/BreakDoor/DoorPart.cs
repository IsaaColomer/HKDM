using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorPart : MonoBehaviour
{
    public float timer;
    // Update is called once per frame
    void Update()
    {
        if(timer >= 0)
        {
            timer -= Time.deltaTime;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
}
