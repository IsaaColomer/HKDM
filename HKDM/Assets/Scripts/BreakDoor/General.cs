using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class General : MonoBehaviour
{
    public List<GameObject> sons = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        foreach(Transform child in transform)
        {
            sons.Add(child.gameObject);
            child.gameObject.GetComponent<Rigidbody>().useGravity = false;
            child.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionX|RigidbodyConstraints.FreezePositionY|RigidbodyConstraints.FreezePositionZ;
        }
    }
}
