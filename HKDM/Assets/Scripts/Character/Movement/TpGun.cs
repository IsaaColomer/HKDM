using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TpGun : MonoBehaviour
{
    public float range;
    public float impulseForce;
    public LayerMask grappable;
    public float minDistance;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        if(Input.GetMouseButton(0))
        {
            if(Physics.Raycast(transform.position, transform.forward, out hit, range, grappable))
            {
                GameObject.Find("Character").GetComponent<Rigidbody>().AddForce((impulseForce*transform.forward), ForceMode.Impulse);
                Debug.DrawLine(transform.position, transform.position +transform.forward*hit.distance, Color.black);
                if(Vector3.Distance(transform.position, hit.transform.position) <= minDistance)
                {
                    GameObject.Find("Character").GetComponent<Rigidbody>().velocity = Vector3.zero;
                    Debug.Log("Reached");
                }
            }
            Debug.Log("Mouse Down");
        }

    }
}
