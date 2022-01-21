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
    public float timeForApproach;
    public float distanceToHand;
    public float rotationSpeed;
    public float stopSpeed;
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
                StartCoroutine(Aproach(hit));
                
                Debug.DrawLine(transform.position, hit.point, Color.cyan);
                
            }
        }
        }
        
        if(Input.GetKeyUp(KeyCode.E))
        {
            GameObject.Find("Character").GetComponent<Rigidbody>().velocity = Vector3.Lerp(GameObject.Find("Character").GetComponent<Rigidbody>().velocity,Vector3.zero, Time.deltaTime*stopSpeed); 
        }             

    }
    IEnumerator Aproach(RaycastHit hit)
    {
        while(Vector3.Distance(hit.transform.position, transform.position) > distanceToHand)
        {
            yield return new WaitForSeconds(timeForApproach);
            hit.transform.position = Vector3.MoveTowards(hit.transform.position, transform.position, Time.deltaTime*approachSpeed);
        } 
        hit.transform.rotation = Quaternion.Slerp(hit.transform.rotation,hit.transform.GetComponent<BaseLife>().startRotation, Time.deltaTime * rotationSpeed);
    }
}
