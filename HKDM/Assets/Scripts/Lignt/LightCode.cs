using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightCode : MonoBehaviour
{
    public bool hitted;
    [SerializeField] private Light light;
    [SerializeField] private Color startLightColor;
    // Start is called before the first frame update
    void Start()
    {
        light  = GetComponent<Light>();
        startLightColor = light.color;
    }

    // Update is called once per frame
    void Update()
    {
        if(hitted)
        {
            if(Input.GetKey(KeyCode.Alpha1))
            {
                light.color = Color.red;
            }
            else if(Input.GetKey(KeyCode.Alpha2))
            {
                light.color = Color.green;
            }
            else
            {
                light.color = startLightColor;
            }
        }
    }
}
