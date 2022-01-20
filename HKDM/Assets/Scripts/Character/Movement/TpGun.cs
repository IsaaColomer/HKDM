using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TpGun : MonoBehaviour
{
    public float range;
    public float impulseForce;
    public LayerMask whatIsGrappleable;

    public ForceMode forceMode;
    public float minDistance;
    public float timeToStop;
    public bool hasActivated;
    public float approachSpeed = 7;
    // Start is called before the first frame update
    void Start()
    {
        hasActivated = false;
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        if(Input.GetKey(KeyCode.E))
        {
            if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, range, whatIsGrappleable)) 
            {
            if(hit.transform.GetComponent<Light>() != null)
            {
                if(hit.transform.GetComponent<Light>().color == Color.green)
            {
                if(Input.GetKey(KeyCode.E))
                {
                    hasActivated = true;
                    GameObject.Find("Character").GetComponent<Rigidbody>().AddForce(impulseForce*transform.forward, forceMode);
                    Debug.DrawLine(transform.position, hit.point, Color.cyan);
                }            
            }
            }

            if(hit.transform.GetComponent<BaseLife>().isGrappable)
            {
                
                    hit.transform.position = Vector3.MoveTowards(hit.transform.position, transform.position, Time.deltaTime*approachSpeed);
                    Debug.DrawLine(transform.position, hit.point, Color.cyan);
                
            }
        }
        }
        
        if(Input.GetKeyUp(KeyCode.E))
        {
            GameObject.Find("Character").GetComponent<Rigidbody>().velocity = Vector3.zero; 
        }             

    }
}
