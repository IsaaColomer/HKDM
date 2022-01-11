using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireMachine : MonoBehaviour
{
    // Start is called before the first frame update    public RaycastHit hit;
    public RaycastHit hit;
    public float timeToNextShot;

    public float ammountOfBullets = 30;
    [SerializeField] private float shootedBullets = 0;
    public float timeToReload = 2f;
    private float timeToReload2;
    [SerializeField] private float timeToNextShot2;
    [SerializeField] private Vector3 positionWhenHit;
    [SerializeField] private LineRenderer lr;
    public float weaponRange;
    public bool canShoot;
    public bool canShootReload;
    [SerializeField] private bool startBulletCounter;
    [SerializeField] private Vector3 cameraTransform;
    // Start is called before the first frame update
    void Start()
    {
        canShoot = true;
        canShootReload = true;
        timeToNextShot2 = timeToNextShot;
        startBulletCounter = false;
        lr = GetComponent<LineRenderer>();
        timeToReload2 = timeToReload;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButton(0))
        {
            if(Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, weaponRange) && canShoot && canShootReload)
            {
                if(hit.transform.tag == "Target")
                {
                   Debug.Log("Hitted with machinegun: " + hit.transform.name);
                   positionWhenHit = transform.position;
                   cameraTransform = Camera.main.transform.forward;
                }                
                canShoot = false;
            }
        }
        if(!canShoot)
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
                shootedBullets++;
                timeToNextShot = timeToNextShot2;
                startBulletCounter = false;
            }
        }        

        if(shootedBullets >= 30)
        {
            Reload();
            Debug.Log("Mus reload!");
        }
    }
    void Reload()
    {        
        if(timeToReload>=0)
        {
            canShootReload = false;
            timeToReload -=Time.deltaTime;
        }
        else
        {
            canShootReload = true;
            shootedBullets = 0;
            timeToReload = timeToReload2;
        }
    }
}
