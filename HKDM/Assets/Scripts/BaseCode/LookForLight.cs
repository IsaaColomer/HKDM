using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookForLight : MonoBehaviour
{
    public float range;
    public LayerMask whatIsGrappleable;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, range, whatIsGrappleable)) 
        {
            GameObject.Find("L").GetComponent<LightCode>().hitted = true;         
        }
    }
}