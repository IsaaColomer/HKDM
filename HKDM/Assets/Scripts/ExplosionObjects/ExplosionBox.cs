using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionBox : MonoBehaviour
{
    public string mainName;
    public bool hitted;
    // Start is called before the first frame update
    void Start()
    {
        hitted = false;
        foreach(Transform child in transform)
        {
            if(child.name != mainName)
            {
                child.gameObject.SetActive(false);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        CheckHitted();
        if(gameObject.transform.childCount == 0)
        {
            Destroy(this.gameObject);
        }
    }
    void CheckHitted()
    {
        if(hitted)
        {
            foreach(Transform child in transform)
            {
                if(child.name == mainName)
                {
                    Destroy(child.gameObject);
                }
                else
                {
                    child.gameObject.SetActive(true);
                }
            }

            GetComponent<BoxCollider>().enabled = false;
        }
    }
}
