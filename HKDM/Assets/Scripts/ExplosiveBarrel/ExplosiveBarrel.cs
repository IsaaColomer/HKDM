using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveBarrel : MonoBehaviour
{
    private Rigidbody rb;
    [Range(0,10000f)] public float force;
    [Range(0,50f)] public float range;
    public ParticleSystem particle;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "PlayerBullet")
        {
            particle.Play();
            rb.constraints = RigidbodyConstraints.None;
            rb.AddExplosionForce(force,other.ClosestPoint(transform.position),range);
        }
    }
}
