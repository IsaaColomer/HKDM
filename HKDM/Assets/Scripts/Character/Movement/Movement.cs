using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private Vector3 move;
    [SerializeField] private Rigidbody rb;
    //public CharacterController controller;
    public float speed = 12;
    [SerializeField] private float gravity = -9.81f;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        move = transform.right * x + transform.forward * z;
        
        transform.position += new Vector3(move.x,gravity, move.z)*speed*Time.deltaTime;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.layer.ToString() == "Ground")
        {
            gravity = 0;
        }
    }
    private void OnCollisionStay(Collision collision)
    {
        if(collision.gameObject.layer.ToString() == "Ground")
        {
            gravity = 0;
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.layer.ToString() == "Ground")
        {
            gravity = -9.81f;
        }
    }
}
