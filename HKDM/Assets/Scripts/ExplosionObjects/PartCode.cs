using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartCode : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerStay(Collider other)
    {
        if(PlayerMovement.instace.crouching && other.tag == "Player")
        {
            for(int i = 0; i < gameObject.transform.GetComponentInParent<BreakDoor>().gameObjectsList.Count; i++)
            {
                gameObject.transform.GetComponentInParent<BreakDoor>().gameObjectsList[i].gameObject.SetActive(true);
                if(gameObject.transform.GetComponentInParent<BreakDoor>().gameObjectsList[i].gameObject.name == name)
                {
                    gameObject.transform.GetComponentInParent<BreakDoor>().gameObjectsList[i].gameObject.gameObject.SetActive(false);
                }
            }
            this.gameObject.SetActive(false);
        }
        else
        {
            if(other.tag == "PlayerBullet")
            {
                for(int i = 0; i < gameObject.transform.GetComponentInParent<BreakDoor>().gameObjectsList.Count; i++)
                {
                    gameObject.transform.GetComponentInParent<BreakDoor>().gameObjectsList[i].gameObject.SetActive(true);
                        if(gameObject.transform.GetComponentInParent<BreakDoor>().gameObjectsList[i].gameObject.name == name)
                        {
                            gameObject.transform.GetComponentInParent<BreakDoor>().gameObjectsList[i].gameObject.gameObject.SetActive(false);
                        }
                }
                this.gameObject.SetActive(false);
            }
        }
    }
}
