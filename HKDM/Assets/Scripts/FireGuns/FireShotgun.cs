using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireShotgun : MonoBehaviour
{
    public float scaleLimit;
    public float time = 1f;
    private float time2;
    public float weaponRange = 17;

    public int perdigonCount = 20;

    public bool canShoot;
    public GameObject bH;
    
    [SerializeField] private List<GameObject> bHList = new List<GameObject>();

    public int maxAmmo = 2;
    [SerializeField]private int currentAmo = 2;
    public float reloadTime = 3f;
    public Animator anim;
    private bool isReloading;
    void Start()
    {
        canShoot = true;
        time2 = time;
        isReloading = false;
    }

    void Update()
    {
        if(isReloading) 
        {
            return;
        }
        if(currentAmo <= 0)
        {
           
            StartCoroutine(Reload());
            return;
        }
        if(Input.GetMouseButton(0) && canShoot)
        {
            for(int i = 0; i < 20; i++)
            {
                ShootRay();
                canShoot = false;
            }
                currentAmo--;
        }
        if(!canShoot)
        {
            if(time>= 0)
            {
                time-= Time.deltaTime;
                canShoot = false;
            }
            else
            {
                for(int i = 0; i < bHList.Count; i++)
                {
                    Destroy(bHList[i]);
                }
                bHList.Clear();
                canShoot = true;
                time = time2;
            }
        }
        
    }

    void ShootRay()
    {
        RaycastHit hit;
        
        float randomRadius = UnityEngine.Random.Range( -scaleLimit, scaleLimit );        
         
        float randomAngle = UnityEngine.Random.Range ( -(float)(2*Math.PI), 2 * Mathf.PI );
         
         //Calculating the raycast direction
         Vector3 direction = new Vector3(
             randomRadius * Mathf.Cos( randomAngle ),
             randomRadius * Mathf.Sin( randomAngle ),
             10f
         );
         
         //Make the direction match the transform
         //It is like converting the Vector3.forward to transform.forward
         
         direction = Camera.main.transform.TransformDirection( direction.normalized );
         Ray r = new Ray(Camera.main.transform.position, direction );
        if(Physics.Raycast(r, out hit, weaponRange))
        {
            if(hit.transform.tag == "Target")
            {
                hit.transform.GetComponent<BaseLife>().TakeDamageFromShotgunGun(1f);
                if(hit.transform.GetComponent<ExplosionBox>() != null)
                {
                    hit.transform.GetComponent<ExplosionBox>().hitted = true;
                }
                bHList.Add(Instantiate(bH, hit.point, Quaternion.identity));
            }
             Debug.DrawLine( transform.position, transform.position+(direction*hit.distance), Color.red );    
        }
    }
    IEnumerator Reload()
    {
        isReloading = true;
        anim.SetBool("reload",true);
        yield return new WaitForSeconds(reloadTime);
        currentAmo = maxAmmo;
        anim.SetBool("reload",false);
        isReloading = false;
    }

    
}
