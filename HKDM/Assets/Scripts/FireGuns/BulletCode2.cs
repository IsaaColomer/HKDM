using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCode2 : MonoBehaviour
{
     public Rigidbody rb;
    public Vector3 direction;
    public float force;
    public Transform end;
    private LineRenderer lr;
    public Transform hitPoint;
    public float scaleLimit;
    // Start is called before the first frame update
    void Start()
    {
        scaleLimit = 2f;
        force = 25f;
        float randomRadius = UnityEngine.Random.Range( -scaleLimit, scaleLimit );        
         
        float randomAngle = UnityEngine.Random.Range ( -(float)(2*Mathf.PI), 2 * Mathf.PI );
         
         //Calculating the raycast direction
          direction = new Vector3(
             randomRadius * Mathf.Cos( randomAngle ),
             randomRadius * Mathf.Sin( randomAngle ),
             10f
         );
        direction = GeneralShotgunScript.instance.firePoint.TransformDirection(direction.normalized);
        rb = GetComponent<Rigidbody>();
        lr = GetComponent<LineRenderer>();
        rb.AddForce(force*direction, ForceMode.Impulse);
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
