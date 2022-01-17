using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnowLightSources : MonoBehaviour
{
    public Transform playerHand;
    [SerializeField] private bool healing;
    [SerializeField] private float timeToHeal = 2;
    private bool drawLines;
    private float timeToHeal2;
    public float distanceToHeal = 10f;
    // Start is called before the first frame update
    void Start()
    {
        healing = false;
        drawLines = false;
        timeToHeal2 = timeToHeal;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.Q))
        {
            KnowLight(distanceToHeal);
            drawLines = true;
        }
        if(healing)
        {
            if(timeToHeal>= 0)
            {
                timeToHeal-= Time.deltaTime;
                KnowLight(distanceToHeal);
            }
            else
            {
                timeToHeal = timeToHeal2;
                healing =  false;
                drawLines = false;
            }
        }
        if(!healing)
        {
            timeToHeal = timeToHeal2;
        }
        DrawLines(distanceToHeal);
            
    }
    public void KnowLight(float radius)
    {
        Collider[] lightSources = Physics.OverlapSphere(transform.position, radius);
        foreach(var lightSource in lightSources)
        {
            if(lightSource.GetComponent<Light>() != null)
            {
                                   
                    if(Vector3.Distance(lightSource.GetComponent<Light>().transform.position, transform.position)<= radius)
                    {
                        healing = true;
                        
                        lightSource.GetComponent<LineRenderer>().enabled = true;
                        lightSource.GetComponent<LineRenderer>().material.color = lightSource.GetComponent<Light>().color;
                        lightSource.GetComponent<LineRenderer>().SetPosition(0, lightSource.GetComponent<Light>().transform.position);
                        lightSource.GetComponent<LineRenderer>().SetPosition(1, playerHand.position);                        
                    }                    
                    
                
            }
        }
    }
    public void DrawLines(float radius)
    {
        Collider[] lightSources2 = Physics.OverlapSphere(transform.position, radius);
         foreach(var lightSource in lightSources2)
        {
            if(lightSource.GetComponent<Light>() != null)
            {
                       
                    if(timeToHeal < timeToHeal2)
                    {
                        lightSource.GetComponent<LineRenderer>().enabled = true;                    
                    }
                    else
                    {
                        lightSource.GetComponent<LineRenderer>().enabled = false;   
                    }
                   
                
            }
        }
    }
}
