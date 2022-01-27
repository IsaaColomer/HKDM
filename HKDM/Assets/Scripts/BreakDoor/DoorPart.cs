using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorPart : MonoBehaviour
{
    public float timer;
    private Rigidbody rb;
    public GameObject parent;
    public float f;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Debug.Log( parent.GetComponent<PartCode>().hitPoint);
        rb.AddForce(transform.forward*f,ForceMode.Impulse);
    }
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
