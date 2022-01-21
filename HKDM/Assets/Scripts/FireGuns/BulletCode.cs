using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCode : MonoBehaviour
{
    private Rigidbody rb;
    public float force;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.AddForce(force*Camera.main.transform.forward, ForceMode.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnCollisionEnter(Collision collision)
    {
        Destroy(this.gameObject);
    }
    void OnTriggerEnter(Collider other)
    {
        Destroy(this.gameObject);
    }
}
