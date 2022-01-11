using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TpGun : MonoBehaviour
{
    public float range;
    public float impulseForce;
    public LayerMask grappable;

    public ForceMode forceMode;
    public float minDistance;
    public bool hasActivated;
    // Start is called before the first frame update
    void Start()
    {
        hasActivated = false;
    }

    // Update is called once per frame
    void Update()
    {
            if(Input.GetKey(KeyCode.E)&&GameObject.Find("GGun").GetComponent<GrapplingGun>().isGrappling)
            {
                hasActivated = true;
                GameObject.Find("Character").GetComponent<Rigidbody>().AddForce(impulseForce*transform.forward, forceMode);
                Debug.DrawLine(transform.position, transform.position +transform.forward*GameObject.Find("GGun").GetComponent<GrapplingGun>().hit.distance, Color.black);
                if(Vector3.Distance(transform.position, GameObject.Find("GGun").GetComponent<GrapplingGun>().hit.transform.position) <= minDistance)
                {
                    GameObject.Find("Character").GetComponent<Rigidbody>().velocity = Vector3.zero;
                    Debug.Log("Reached");
                }
            }
                

    }
}
