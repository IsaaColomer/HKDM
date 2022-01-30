using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorAnimation : MonoBehaviour
{
    private Animator animator;
    private Transform player;
    public float timeToWait;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector3.Distance(player.position, transform.position)<=3 && Input.GetKeyDown(KeyCode.E))
        {
            animator.SetBool("openDoor", true);
        }
        if(animator.GetBool("openDoor"))
        {
            StartCoroutine(KeepDoorOpen());
        }
        if(Vector3.Distance(player.position, transform.position)<=3 && Input.GetKeyDown(KeyCode.E)&&animator.GetBool("doorOpen"))
        {
            animator.SetBool("closeDoor", true);
            animator.SetBool("doorOpen", false);
        }
        if(animator.GetBool("closeDoor"))
        {
            StartCoroutine(KeepDoorClose());
        }
    }
    IEnumerator KeepDoorOpen()
    {
        yield return new WaitForSeconds(timeToWait);
        animator.SetBool("doorOpen", true);
        animator.SetBool("openDoor", false);
    }
    IEnumerator KeepDoorClose()
    {
        yield return new WaitForSeconds(timeToWait);
        animator.SetBool("closeDoor", false);
        animator.SetBool("idleDoor", true);
    }
}
