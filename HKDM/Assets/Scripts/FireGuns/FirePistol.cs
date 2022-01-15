using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirePistol : MonoBehaviour
{
    public RaycastHit hit;
    public GameObject bH;
    [SerializeField] private List<GameObject> bHList = new List<GameObject>();
    public float timeToNextShot;
    [SerializeField] private float timeToNextShot2;
    [SerializeField] private Vector3 positionWhenHit;
    [SerializeField] private LineRenderer lr;
    public float weaponRange;
    public bool canShoot;
    public float shootingForce = 10f;
    private Vector3 hitPoint;
    [SerializeField] private bool startBulletCounter;
    [SerializeField] private Vector3 cameraTransform;
    public int maxAmmo = 10;
    [SerializeField]private int currentAmo = 10;
    public float reloadTime = 3f;
    public Animator anim;
    private bool isReloading;
    // Start is called before the first frame update
    void Start()
    {
        canShoot = true;
        timeToNextShot2 = timeToNextShot;
        startBulletCounter = false;
        lr = GetComponent<LineRenderer>();
        anim = GetComponent<Animator>();
        isReloading = false;
    }

    // Update is called once per frame
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

        if(Input.GetMouseButton(0))
        {
            if(Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, weaponRange) && canShoot)
            {
                if(hit.transform.tag == "Target")
                {
                    if(hit.transform.gameObject.layer == LayerMask.NameToLayer("GroundEnemy"))
                    {
                        Debug.Log("hitted ground enemy");
                        hit.transform.GetComponent<Rigidbody>().AddExplosionForce(shootingForce,hit.transform.position,0.1f);
                        
                        
                        canShoot = false;
                        
                    }
                    if(canShoot)    bHList.Add(Instantiate(bH, hit.point, Quaternion.identity));
                    if(hit.transform.GetComponent<BaseLife>())    hit.transform.GetComponent<BaseLife>().TakeDamageFromPistolGun(5f);
                    positionWhenHit = transform.position;
                    cameraTransform = Camera.main.transform.forward;
                    hitPoint = hit.point;
                    canShoot = false;
                }
                              
                canShoot = false;
            }
        }
        if(!canShoot && startBulletCounter)
        {
            if(timeToNextShot > 0)
            {
                timeToNextShot-=Time.deltaTime;
                //Debug.DrawLine(positionWhenHit.position, positionWhenHit.position+(cameraTransform*hit.distance), Color.green);
                Debug.DrawLine(positionWhenHit, hit.point, Color.green);
            }
            else
            {
                currentAmo--;
                canShoot = true;
                for(int i = 0; i < bHList.Count; i++)
                {
                    Destroy(bHList[i]);
                }
                bHList.Clear();
                timeToNextShot = timeToNextShot2;
                startBulletCounter = false;
            }
        }
        if(Input.GetMouseButtonUp(0))
        {
            startBulletCounter = true;
        }
        
    }
    IEnumerator Reload()
    {
        isReloading = true;
        anim.SetBool("Reloading",true);
        yield return new WaitForSeconds(reloadTime);
        currentAmo = maxAmmo;
        anim.SetBool("Reloading",false);
        isReloading = false;
    }
}
