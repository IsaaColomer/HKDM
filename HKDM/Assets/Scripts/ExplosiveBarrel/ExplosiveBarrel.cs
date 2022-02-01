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
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "PlayerBullet")
        {
            Instantiate(particle, other.ClosestPoint(transform.position), Quaternion.identity);
            Wait();
            rb.constraints = RigidbodyConstraints.None;
            rb.AddExplosionForce(force,other.ClosestPoint(transform.position),range);
            StartCoroutine(Wait());
        }
    }
    IEnumerator Wait()
    {
        yield return new WaitForSeconds(2f);
        Destroy(particle);
        Destroy(this.gameObject);
    }
}
