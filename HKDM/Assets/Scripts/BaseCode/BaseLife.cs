using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseLife : MonoBehaviour
{
    public float myLife = 100;
    private MeshRenderer mr;
    // Start is called before the first frame update
    void Start()
    {
        mr = GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        mr.material.color /= myLife*255;
        if(myLife<=0)
        {
            Die();
        }
        Debug.Log(myLife);
    }
    public void Die()
    {
        Destroy(this.gameObject);
    }
    public void TakeDamageFromMachineGun(float damage)
    {
        myLife-=damage;
    }
    public void TakeDamageFromPistolGun(float damage)
    {
        myLife-=damage;
    }
    public void TakeDamageFromShotgunGun(float damage)
    {
        myLife-=damage;
    }
}
