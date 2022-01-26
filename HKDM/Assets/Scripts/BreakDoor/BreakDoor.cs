using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakDoor : MonoBehaviour
{
    private Rigidbody[] sonsArray;
    public string name;
    // Start is called before the first frame update
    void Start()
    {
            foreach (Transform child in transform)
            {
                if(child.name != name)
                {
                    child.gameObject.SetActive(false);
                }                    
            }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerStay(Collider other)
    {
        if(PlayerMovement.instace.crouching && other.tag == "Player")
        {
            Debug.Log("Hola");
            foreach (Transform child in transform)
            {
                child.gameObject.SetActive(true);
                if(child.name == name)
                {
                    child.gameObject.SetActive(false);
                }
            }
            //this.gameObject.SetActive(false);
        }
        else
        {
            if(other.tag == "PlayerBullet")
            {
                foreach (Transform child in transform)
                {
                    child.gameObject.SetActive(true);
                    if (child.name == name)
                    {
                        child.gameObject.SetActive(false);
                    }
                }
            }
        }

    }
}
