using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Part : MonoBehaviour
{
    // Start is called before the first frame update
    private Rigidbody rb;
    public float force;
    public bool startCount;
    private float timeToDestroy = 3f;
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    void Start()
    {
        rb.centerOfMass = Vector3.zero;
        rb.inertiaTensorRotation = Quaternion.identity;
        rb.constraints = RigidbodyConstraints.FreezeAll;
        rb.constraints = RigidbodyConstraints.FreezeRotation;
        rb.constraints = RigidbodyConstraints.FreezePosition;
        rb.useGravity = false;
        startCount = false;
    }

    // Update is called once per frame
    void Update()
    {  
        if(startCount)
        {
            if(timeToDestroy>=0)
            {
                timeToDestroy-=Time.deltaTime;
            }
            else
            {
                Destroy(this.gameObject);
            }
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "PlayerBullet")
        {
            for(int i = 0; i < gameObject.GetComponentInParent<General>().sons.Count; i++)
            {
                gameObject.GetComponentInParent<General>().sons[i].GetComponent<Part>().rb.constraints = RigidbodyConstraints.None;
                gameObject.GetComponentInParent<General>().sons[i].GetComponent<Part>().rb.useGravity = true;
                gameObject.GetComponentInParent<General>().sons[i].GetComponent<Part>().rb.AddExplosionForce(force, other.ClosestPoint(transform.position), .2f);
                gameObject.GetComponentInParent<General>().sons[i].GetComponent<Part>().startCount = true;
                if(gameObject.GetComponentInParent<BoxCollider>().enabled)
                {
                    gameObject.GetComponentInParent<BoxCollider>().enabled = false;
                }
                
            }
        }
    }
    
}
