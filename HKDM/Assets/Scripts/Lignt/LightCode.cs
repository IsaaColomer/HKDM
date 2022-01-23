using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightCode : MonoBehaviour
{
    public bool hitted;
    [SerializeField] public Light spotLight;
    [SerializeField] public Light pointLight;

    [SerializeField] public SphereCollider sCol;
    [SerializeField] private Color startLightColor;
    // Start is called before the first frame update
    void Start()
    {
        sCol = this.gameObject.GetComponentInChildren<SphereCollider>();
        startLightColor = spotLight.color;
    }

    // Update is called once per frame
    void Update()
    {
        if(this.hitted && LookForLight.instance.hit.collider == sCol)
        {
            if(Input.GetKey(KeyCode.Alpha1))
            {
                this.spotLight.color = Color.red;
                pointLight.color = spotLight.color;
            }
            else if(Input.GetKey(KeyCode.Alpha2))
            {
                this.spotLight.color = Color.green;
                pointLight.color = spotLight.color;
            }
        }
        
    }
}
