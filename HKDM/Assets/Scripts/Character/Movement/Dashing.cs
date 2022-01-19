using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dashing : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    public ForceMode forceMode;
    public Transform back;
    public float explotionRadius = 1f;
    public float force = 1000;
    public Camera camera;
    private float groundForce;
    public float timeToDash = 2f;
    private float timeToDash2;
    private bool dashed;
    // Start is called before the first frame update
    void Start()
    {
        timeToDash2 = timeToDash;
        rb = GetComponent<Rigidbody>();
        dashed =false;
        groundForce = force*10;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q) && !dashed)
        {
            if(!GetComponent<PlayerMovement>().grounded)
            {
                Debug.Log("Dash");
                rb.AddForce(force*camera.transform.forward, forceMode);
            }
            if(GetComponent<PlayerMovement>().grounded)
            {
                Debug.Log("Ground Dash");
                rb.AddForce(groundForce*camera.transform.forward, forceMode);
            }

            //rb.AddExplosionForce(force, back.position, explotionRadius);
            dashed = true;
            
        }
        if(dashed)
        {
            if(timeToDash>= 0)
            {
                timeToDash-= Time.deltaTime;
            }
            else
            {
                timeToDash = timeToDash2;
                dashed = false;                
            }
        }
    }
}
