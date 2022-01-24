using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookForLight : MonoBehaviour
{
    public static LookForLight instance;
    public float range;
    public bool hitted;
    public RaycastHit hit;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, range)) 
        {
            if(hit.collider != null)
            {
                if(hit.collider == hit.transform.gameObject.GetComponentInChildren<SphereCollider>())
                {
                    hit.transform.parent.GetComponent<LightCode>().hitted = true;
                }
                else
                {
                    hit.transform.parent.GetComponent<LightCode>().hitted = false;
                }                 
            }
        }
    }
}
