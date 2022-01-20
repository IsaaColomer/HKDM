using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseLife : MonoBehaviour
{
    public float myLife = 100;
    public bool isGrappable;
    private MeshRenderer mr;
    // Start is called before the first frame update
    void Start()
    {
        mr = GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(myLife<=0)
        {
            Die();
        }
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
