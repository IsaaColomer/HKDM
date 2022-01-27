using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorPart : MonoBehaviour
{
    private Rigidbody rb;
    private Transform explosionPosition;
    public string nameOfExplosionPosition;
    public float timer;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        foreach (Transform child in transform)
        {
            if(child.name == nameOfExplosionPosition)
            {
                explosionPosition = child;
            }
        }
        // MODIFICAR PQ TINGUI EXPLOSIO REALISTA
        float force = Random.Range(5000f,7000f);
        rb.AddExplosionForce(force, explosionPosition.position, 4f);
        Debug.Log(force);
    }

    // Update is called once per frame
    void Update()
    {
        if(timer >= 0)
        {
            timer -= Time.deltaTime;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
}
