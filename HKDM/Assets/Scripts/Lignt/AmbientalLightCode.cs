using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbientalLightCode : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField ]private Light light;
    void Start()
    {
        light = GetComponent<Light>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
