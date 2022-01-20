using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildLife : MonoBehaviour
{
    public float life = 3f;
    private bool started = false;
    public float explosionForce = 10000f;
    [SerializeField] private Rigidbody rb;
    private bool explode;
    // Start is called before the first frame update
    void Start()
    {
        started = true;
        rb = GetComponent<Rigidbody>();
        explode = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(started)
        {
            if(!explode)
            {
                rb.AddExplosionForce(explosionForce,transform.position, 10f);
                explode = true;
            }                    
            if(life>= 0)
            {
                life-=Time.deltaTime;
            }
            else
            {
                Destroy(this.gameObject);
            }
        }
    }
}
