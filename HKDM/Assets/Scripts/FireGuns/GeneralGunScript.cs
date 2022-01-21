using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralGunScript : MonoBehaviour
{
    [SerializeField] public GameObject bulletPrefab;
    [SerializeField] public List<GameObject> pistolBullets = new List<GameObject>();
    public Transform pistolFirePoint;
    private float totalPistolBullets;
    public float pistolReloadTime;
    [SerializeField] private int thisPistolBullet;
    private bool pistolReloading;
    // Start is called before the first frame update
    void Start()
    {
        pistolReloading = false;
        for(int i = 0; i < pistolBullets.Count; i++)
        {
            pistolBullets[i] = bulletPrefab;
        }
        thisPistolBullet = pistolBullets.Count;
        totalPistolBullets = thisPistolBullet;
        Debug.Log(totalPistolBullets);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0)&& this.gameObject.name == "FireGun" && pistolBullets.Count > 0 && !pistolReloading)
        {
            PistolShoot();
        }
        if(pistolBullets.Count <= 0)
        {
            StartCoroutine(PistolReload());
            return;
        }
    }
    void PistolShoot()
    {
        Instantiate(bulletPrefab, pistolFirePoint.position, Quaternion.identity);
        pistolBullets.RemoveAt(thisPistolBullet-1);
        thisPistolBullet--;
    }

    IEnumerator PistolReload()
    {        
        pistolReloading =true;
        for(int i = 0; i < totalPistolBullets; i++)
        {
            pistolBullets.Insert(i, bulletPrefab);
            yield return new WaitForSeconds(0.3f);
        }     
        yield return new WaitForSeconds(pistolReloadTime);
        thisPistolBullet = pistolBullets.Count;            
        pistolReloading =false;
    }
}
