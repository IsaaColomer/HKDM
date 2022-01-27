using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakDoor : MonoBehaviour
{
    public string name;
    public List<GameObject> gameObjectsList = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
            foreach (Transform child in transform)
            {
                if(child.name !=name)
                {
                    child.gameObject.SetActive(false);
                    gameObjectsList.Add(child.gameObject);
                }                    
            }
    }
}
