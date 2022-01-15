using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightCode : MonoBehaviour
{
    public bool hitted;
    [SerializeField] private Light light;

    [SerializeField] public SphereCollider sCol;
    [SerializeField] private Color startLightColor;
    // Start is called before the first frame update
    void Start()
    {
        light  = GetComponent<Light>();
        sCol = this.gameObject.GetComponent<SphereCollider>();
        startLightColor = light.color;
    }

    // Update is called once per frame
    void Update()
    {
        if(this.hitted && LookForLight.instance.hit.collider == sCol)
        {
            if(Input.GetKey(KeyCode.Alpha1))
            {
                this.light.color = Color.red;
            }
            else if(Input.GetKey(KeyCode.Alpha2))
            {
                this.light.color = Color.green;
            }
        }
    }
}
