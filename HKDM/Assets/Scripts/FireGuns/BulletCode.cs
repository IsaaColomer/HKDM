using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCode : MonoBehaviour
{
    public Rigidbody rb;
    public float direction;
    public float force;
    public Transform end;
    private LineRenderer lr;
    public Transform hitPoint;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.AddForce(force*Camera.main.transform.forward-new Vector3(5,0,0), ForceMode.Impulse);
        lr = GetComponent<LineRenderer>();
        transform.forward = Camera.main.transform.forward;
        end.forward = Camera.main.transform.forward;
    }

    // Update is called once per frame
    void Update()
    {
        lr.SetPosition(0,transform.position);
        lr.SetPosition(1,end.position);
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
