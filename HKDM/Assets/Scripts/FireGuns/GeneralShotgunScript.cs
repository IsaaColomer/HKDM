using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class GeneralShotgunScript : MonoBehaviour
{
    public static GeneralShotgunScript instance;
    [SerializeField] public GameObject bulletPrefab;
    [SerializeField] public List<GameObject> bullets = new List<GameObject>();
    public Transform firePoint;
    private float totalBullets;
    public float reloadTime;
    [SerializeField] private int thisBullet;
    [SerializeField] private float time;
    [SerializeField] private bool shootThroughBullets;
    [SerializeField] private bool canShoot;
    public int perdigons;
    private bool reloading;
    public float scaleLimit;
    public Vector3 direction;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        reloading = false;
        time = .1f;
        shootThroughBullets = false;
        canShoot = false;
        for(int i = 0; i < bullets.Count; i++)
        {
            bullets[i] = bulletPrefab;
        }
        thisBullet = bullets.Count;
        totalBullets = thisBullet;
        Debug.Log(totalBullets);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0)&& this.gameObject.name == "Shotgun" && bullets.Count > 0 && !reloading)
        {
            if(canShoot)
            {
                for(int i = 0; i < perdigons; i++)
                {
                    Shoot();
                }
                canShoot = false;
            }

        }
        if(bullets.Count <= 0)
        {
            StartCoroutine(Reload());
            return;
        }
        if(!canShoot)
        {
            if(time>=0)
            {
                time-=Time.deltaTime;
                canShoot = false;
            }
            else
            {
                time = .1f;
                canShoot = true;
            }
        }
    }
    void Shoot()
    {
        Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);

        if(thisBullet >= 0)
            bullets.RemoveAt(thisBullet-1);

        thisBullet--;
    }

    IEnumerator Reload()
    {        
        reloading =true;
        for(int i = 0; i < totalBullets; i++)
        {
            bullets.Insert(i, bulletPrefab);
            yield return new WaitForSeconds(0.1f);
        }     
        yield return new WaitForSeconds(reloadTime);
        thisBullet = bullets.Count;            
        reloading =false;
    }
}
