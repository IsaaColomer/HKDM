using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorPart : MonoBehaviour
{
    private Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Debug.Log("Adeu");
        rb.AddExplosionForce(5000f, transform.position, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
