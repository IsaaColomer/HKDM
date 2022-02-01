using System.Linq;
// Some stupid rigidbody based movement by Dani

using System;
using UnityEngine;
using System.Collections.Generic;

public class PlayerMovement : MonoBehaviour {
    public static PlayerMovement instace;
    [Header("Assignables")]
    public Camera camera;
    public Transform playerCam;
    public Transform orientation;
    public LayerMask whatIsGround;
    private Rigidbody rb;

    [Header("Rotation and look")]
    private float xRotation;
    public float sensitivity = 50f;
    private float sensMultiplier = 1.5f;

    [Header("Movement")]
    public float moveSpeed = 4500;
    public float maxSpeed = 20;
    public float counterMovement = 0.175f;
    private float threshold = 0.01f;
    public float maxSlopeAngle = 35f;
    private Vector3 crouchScale = new Vector3(1,0.5f, 1);
    private Vector3 playerScale;
    public float slideForce = 400;
    public float slideCounterMovement = 0.2f;
    private bool readyToJump = true;
    private float jumpCooldown = 0.25f;
    public float jumpForce = 550f; //DO NOT LOWER JUMP FORCE, INSTEAD INCREASE GRAVITY. (default 550)
    float x, y;
    bool jumping;
    private Vector3 normalVector = Vector3.up;

    [Header("Collisions")]
    public bool grounded;
    public bool crouching;
    private bool surfing;
    private bool cancellingGrounded;
    private bool cancellingSurf;
    private bool cancellingWall;
    private bool onWall;
    private bool cancelling;

    public static PlayerMovement Instance { get; private set; }

    void Awake()
    {

        Instance = this;

        rb = GetComponent<Rigidbody>();
    }

    void Start()
    {
        instace = this;
        playerScale = transform.localScale;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        readyToJump = true;
    }


    private void FixedUpdate()
    {
        Movement();
    }

    private void Update()
    {
        MyInput();
        Look();
    }

    private void LateUpdate()
    {
        //call the wallrunning Function
    }

    /// <summary>
    /// Find user input. Should put this in its own class but im lazy
    /// </summary>
    private void MyInput()
    {
        x = Input.GetAxisRaw("Horizontal");
        y = Input.GetAxisRaw("Vertical");
        if(Input.GetButtonDown("Jump"))
        {
            jumping = true;
        }
        if(Input.GetButtonUp("Jump"))
            jumping = false;
            
        crouching = Input.GetKey(KeyCode.LeftControl);

        //Crouching
        if (Input.GetKeyDown(KeyCode.LeftControl))
            StartCrouch();
        if (Input.GetKeyUp(KeyCode.LeftControl))
            StopCrouch();
    }

    private void StartCrouch()
    {
        transform.localScale = crouchScale;
        transform.position = new Vector3(transform.position.x, transform.position.y - 0.5f, transform.position.z);
        if (rb.velocity.magnitude > 0.2f && grounded)
        {
            if (grounded)
            {
                rb.AddForce(orientation.transform.forward * slideForce);
            }
        }
    }

    private void StopCrouch()
    {
        transform.localScale = playerScale;
        transform.position = new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z);
    }

    private void Movement()
    {
        //Extra gravity
        rb.AddForce(Vector3.down * Time.deltaTime * 10);

        //Find actual velocity relative to where player is looking
        Vector2 mag = FindVelRelativeToLook();
        float xMag = mag.x, yMag = mag.y;

        //Counteract sliding and sloppy movement
        CounterMovement(x, y, mag);

        //If holding jump && ready to jump, then jump
        if (readyToJump && jumping) Jump();

        //Set max speed
        float maxSpeed = this.maxSpeed;

        //If sliding down a ramp, add force down so player stays grounded and also builds speed
        if (crouching && grounded && readyToJump)
        {
            rb.AddForce(Vector3.down * Time.deltaTime * 3000);
            return;
        }

        //If speed is larger than maxspeed, cancel out the input so you don't go over max speed
        if (x > 0 && xMag > maxSpeed) x = 0;
        if (x < 0 && xMag < -maxSpeed) x = 0;
        if (y > 0 && yMag > maxSpeed) y = 0;
        if (y < 0 && yMag < -maxSpeed) y = 0;

        //Some multipliers
        float multiplier = 1f, multiplierV = 1f;

        // Movement in air
        if (!grounded)
        {
            multiplier = 0.5f;
            multiplierV = 0.5f;
        }

        // Movement while sliding
        if (grounded && crouching) multiplierV = 0f;

        //Apply forces to move player
        rb.AddForce(orientation.transform.forward * y * moveSpeed * Time.deltaTime * multiplier * multiplierV);
        rb.AddForce(orientation.transform.right * x * moveSpeed * Time.deltaTime * multiplier);
    }

    private void Jump()
    {
        if ((grounded || surfing) && readyToJump)
        {
            MonoBehaviour.print("jumping");
            Vector3 velocity = rb.velocity;
            readyToJump = false;
            rb.AddForce(Vector2.up * jumpForce * 1.5f);
            rb.AddForce(normalVector * jumpForce * 0.5f);
            if (rb.velocity.y < 0.5f)
            {
                rb.velocity = new Vector3(velocity.x, 0f, velocity.z);
            }
            else if (rb.velocity.y > 0f)
            {
                rb.velocity = new Vector3(velocity.x, velocity.y / 2f, velocity.z);
            }
            
            Invoke("ResetJump", jumpCooldown);
            
        }
    }

    private void ResetJump()
    {
        readyToJump = true;
    }

    private float desiredX;
    private void Look()
    {
        float mouseX = Input.GetAxis("Mouse X") * sensitivity * Time.fixedDeltaTime * sensMultiplier;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity * Time.fixedDeltaTime * sensMultiplier;

        //Find current look rotation
        Vector3 rot = playerCam.transform.localRotation.eulerAngles;
        desiredX = rot.y + mouseX;

        //Rotate, and also make sure we dont over- or under-rotate.
        xRotation -= mouseY;
        float clamp = 89.5f;
        xRotation = Mathf.Clamp(xRotation, -clamp, clamp);

        //Perform the rotations
        playerCam.transform.localRotation = Quaternion.Euler(xRotation, desiredX, 0);
        orientation.transform.localRotation = Quaternion.Euler(0, desiredX, 0);
    }

    private void CounterMovement(float x, float y, Vector2 mag)
    {
        if (!grounded || jumping) return;

        //Slow down sliding
        if (crouching)
        {
            rb.AddForce(moveSpeed * Time.deltaTime * -rb.velocity.normalized * slideCounterMovement);
            return;
        }

        //Counter movement
        if (Math.Abs(mag.x) > threshold && Math.Abs(x) < 0.05f || (mag.x < -threshold && x > 0) || (mag.x > threshold && x < 0))
        {
            rb.AddForce(moveSpeed * orientation.transform.right * Time.deltaTime * -mag.x * counterMovement);
        }
        if (Math.Abs(mag.y) > threshold && Math.Abs(y) < 0.05f || (mag.y < -threshold && y > 0) || (mag.y > threshold && y < 0))
        {
            rb.AddForce(moveSpeed * orientation.transform.forward * Time.deltaTime * -mag.y * counterMovement);
        }

        //Limit diagonal running. This will also cause a full stop if sliding fast and un-crouching, so not optimal.
        if (Mathf.Sqrt((Mathf.Pow(rb.velocity.x, 2) + Mathf.Pow(rb.velocity.z, 2))) > maxSpeed)
        {
            float fallspeed = rb.velocity.y;
            Vector3 n = rb.velocity.normalized * maxSpeed;
            rb.velocity = new Vector3(n.x, fallspeed, n.z);
        }
    }

    /// <summary>
    /// Find the velocity relative to where the player is looking
    /// Useful for vectors calculations regarding movement and limiting movement
    /// </summary>
    /// <returns></returns>
    public Vector2 FindVelRelativeToLook()
    {
        float lookAngle = orientation.transform.eulerAngles.y;
        float moveAngle = Mathf.Atan2(rb.velocity.x, rb.velocity.z) * Mathf.Rad2Deg;

        float u = Mathf.DeltaAngle(lookAngle, moveAngle);
        float v = 90 - u;

        float magnitue = rb.velocity.magnitude;
        float yMag = magnitue * Mathf.Cos(u * Mathf.Deg2Rad);
        float xMag = magnitue * Mathf.Cos(v * Mathf.Deg2Rad);

        return new Vector2(xMag, yMag);
    }
    //a lot of math (dont touch)

    private bool IsFloor(Vector3 v)
    {
        return Vector3.Angle(Vector3.up, v) < maxSlopeAngle;
    }

    private bool IsSurf(Vector3 v)
    {
        float num = Vector3.Angle(Vector3.up, v);
        if (num < 89f)
        {
            return num > maxSlopeAngle;
        }
        return false;
    }

    private bool IsRoof(Vector3 v)
    {
        return v.y == -1f;
    }

    /// <summary>
    /// Handle ground detection
    /// </summary>
    private void OnCollisionStay(Collision other)
    {
        int layer = other.gameObject.layer;
        if ((int)whatIsGround != ((int)whatIsGround | (1 << layer)))
        {
            return;
        }
        for (int i = 0; i < other.contactCount; i++)
        {
            Vector3 normal = other.contacts[i].normal;
            if (IsFloor(normal))
            {
                grounded = true;
                normalVector = normal;
                cancellingGrounded = false;
                CancelInvoke("StopGrounded");
            }
            if (IsSurf(normal))
            {
                surfing = true;
                cancellingSurf = false;
                CancelInvoke("StopSurf");
            }
            IsRoof(normal);
        }
        float num = 3f;
        if (!cancellingGrounded)
        {
            cancellingGrounded = true;
            Invoke("StopGrounded", Time.deltaTime * num);
        }
        if (!cancellingSurf)
        {
            cancellingSurf = true;
            Invoke("StopSurf", Time.deltaTime * num);
        }
    }

    private void StopGrounded()
    {
        grounded = false;
    }

    private void StopSurf()
    {
        surfing = false;
    }
}
       