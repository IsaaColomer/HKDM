using UnityEngine;

public class GrapplingGun : MonoBehaviour 
{
    private Vector3 grapplePoint;
    public LayerMask whatIsGrappleable;
    public Transform gunTip, camera, player;
    public float maxDistance = 5f;
    private SpringJoint joint;
    public bool isGrappling;

    public RaycastHit hit;
    private Transform hittedGameObject;

    void Awake() 
    {
        isGrappling = false;
    }

    void Update() 
    {
        if(GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>().grounded)
        {
            maxDistance = 30;
        }
        else
        {
            maxDistance = 40;
        }
        if (Input.GetMouseButtonDown(1)) 
        {
            StartGrapple();
        }
        else if (Input.GetMouseButtonUp(1)) 
        {
            StopGrapple();
        }
    }

    //Called after Update
    void LateUpdate() {
        DrawRope();
    }

    /// <summary>
    /// Call whenever we want to start a grapple
    /// </summary>
    void StartGrapple() {
        
        if (Physics.Raycast(camera.position, camera.forward, out hit, maxDistance, whatIsGrappleable)) 
        {
            if(hit.transform.GetComponent<Light>().color == Color.red)
            {
                hittedGameObject = hit.transform.parent;
                isGrappling = true;
                grapplePoint = hit.point;
                joint = player.gameObject.AddComponent<SpringJoint>();
                joint.autoConfigureConnectedAnchor = false;
                joint.connectedAnchor = grapplePoint;
                float distanceFromPoint = Vector3.Distance(player.position, grapplePoint);
            //The distance grapple will try to keep from grapple point. 
                joint.maxDistance = distanceFromPoint * 0.8f;
                joint.minDistance = distanceFromPoint * 0.25f;

            //Adjust these values to fit your game.
                joint.spring = 4.5f;
                joint.damper = 7f;
                joint.massScale = 4.5f;
                currentGrapplePosition = gunTip.position;
            }

        }
    }


    /// <summary>
    /// Call whenever we want to stop a grapple
    /// </summary>
    void StopGrapple() {
        hittedGameObject.GetComponent<LightCode>().hitted = false;
        isGrappling = false;
        Destroy(joint);
    }

    private Vector3 currentGrapplePosition;
    
    void DrawRope() {
        //If not grappling, don't draw rope
        if (!joint) return;

        currentGrapplePosition = Vector3.Lerp(currentGrapplePosition, grapplePoint, Time.deltaTime * 8f);
    }

    public bool IsGrappling() {
        return joint != null;
    }

    public Vector3 GetGrapplePoint() {
        return grapplePoint;
    }
}
