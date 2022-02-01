using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallRunning : MonoBehaviour
{
    /// <summary>
    /// Wall run Tutorial stuff, scroll down for full movement
    /// </summary>

    //Wallrunning
    private Rigidbody rb;
    public LayerMask whatIsWall;
    public float jumpFromWallForce;
    private float yPos;
    bool isWallRunning;
    private Vector3 jumpNormal;
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    private void StopWallRun()
    {
        isWallRunning = false;
        rb.useGravity = true;
    }
    void Update()
    {
        if(isWallRunning)
        {
            if(Input.GetKeyDown(KeyCode.Space)&& !PlayerMovement.instace.grounded)
            {
                rb.AddForce(jumpFromWallForce*jumpNormal, ForceMode.Impulse);
                Debug.Log("Jumped from a wall");
            }
        }
    }
    void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.tag == "RunnableWall" && !PlayerMovement.instace.grounded)
        {
            isWallRunning = true;
            rb.useGravity = false;
            yPos = collision.contacts[collision.contactCount/2].point.y;
            jumpNormal = collision.contacts[collision.contactCount/2].normal;
            Debug.DrawRay(collision.contacts[collision.contactCount/2].point, collision.contacts[collision.contactCount/2].normal,Color.red);
        }
    }
    void OnCollisionStay(Collision collision)
    {
        if(collision.transform.tag == "RunnableWall"&& !PlayerMovement.instace.grounded)
        {
            isWallRunning = true;
            rb.useGravity = false;
            rb.transform.position = new Vector3(rb.transform.position.x, yPos, rb.transform.position.z);
            Debug.DrawRay(collision.contacts[collision.contactCount/2].point, collision.contacts[collision.contactCount/2].normal,Color.red);
        }
    }
    void OnCollisionExit(Collision other)
    {
        if(other.transform.tag == "RunnableWall"&& !PlayerMovement.instace.grounded)
        {
            isWallRunning = false;
            rb.useGravity = true;
        }
    }
}
