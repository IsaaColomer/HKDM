using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirePistol : MonoBehaviour
{
    public RaycastHit hit;
    public float timeToNextShot;
    [SerializeField] private float timeToNextShot2;
    [SerializeField] private Vector3 positionWhenHit;
    [SerializeField] private LineRenderer lr;
    public float weaponRange;
    public bool canShoot;
    [SerializeField] private bool startBulletCounter;
    [SerializeField] private Vector3 cameraTransform;
    // Start is called before the first frame update
    void Start()
    {
        canShoot = true;
        timeToNextShot2 = timeToNextShot;
        startBulletCounter = false;
        lr = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButton(0))
        {
            if(Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, weaponRange) && canShoot)
            {
                if(hit.transform.tag == "Target")
                {
                   hit.transform.GetComponent<BaseLife>().TakeDamageFromPistolGun(5f);
                   positionWhenHit = transform.position;
                   cameraTransform = Camera.main.transform.forward;
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
                lr.SetPosition(0, positionWhenHit);
                lr.SetPosition(1, positionWhenHit+(cameraTransform*hit.distance));
            }
            else
            {
                canShoot = true;
                timeToNextShot = timeToNextShot2;
                startBulletCounter = false;
            }
        }
        if(Input.GetMouseButtonUp(0))
        {
            startBulletCounter = true;
        }
        
    }
}
