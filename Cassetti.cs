using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.AI;

public class Cassetti : MonoBehaviour
{
    NavMeshAgent nav;
    Vector3 point;
    public Animator anim;
    private bool canMeet = false;
    private float lastMeetTime = 0f;
    private bool canNav = false;
    
    bool setPath;

    void Start()
    {
        nav = GetComponent<NavMeshAgent>();
        nav.isStopped = true;
        anim = GetComponent<Animator>();
        StartCoroutine(DelayMovement());
    }

    // Update is called once per frame
    void Update()
    {
        if(canNav){
            anim.SetBool("play", true);
            if(canMeet)
            {
                CheckDistance();
                lastMeetTime = Time.time;
            }

            if (!canMeet && Time.time - lastMeetTime >= Random.Range(10.0f, 15.0f))
            {
                canMeet = true;
            }

            if(nav.remainingDistance<0.1f){
                nav.isStopped = true;
                anim.SetBool("walk", false);
                //Invoke("onMove", 1.0f);
                StartCoroutine(onMove());
            }
        }
    }

    // IEnumerator startMove(){
    //     Vector3 Point = new Vector3(28f, 6.3f, 45f);
    //     anim.SetBool("walk", true);
    //     anim.SetBool("meet", false);
    //     nav.isStopped = false;
    //     if (RandomPoint(randomPoint, out point))
    //     {
    //         nav.SetDestination(randomPoint);
    //     }

    //     yield return new WaitForSeconds(1f);

    //     anim.SetBool("walk", true);
    //     anim.SetBool("meet", false);
       
    //     nav.isStopped = false;
    // }

    IEnumerator onMove(){
        Vector3 randomPoint = transform.position + Random.insideUnitSphere * Random.Range(5.0f, 20.0f);
        if (RandomPoint(randomPoint, out point))
        {
            nav.SetDestination(randomPoint);
        }

        yield return new WaitForSeconds(1f);

        anim.SetBool("walk", true);
        anim.SetBool("meet", false);
       
        nav.isStopped = false;
    }

    bool RandomPoint(Vector3 center, out Vector3 result)
    {
        for (int i = 0; i < 30; i++)
        {
            Vector3 randomPoint = transform.position + Random.insideUnitSphere * Random.Range(5.0f, 20.0f);
            NavMeshHit hit;
            if (NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.AllAreas))
            {
                result = hit.position;
                return true;
            }
        }
        result = Vector3.zero;
        return false;
    }

    public float checkDistance = 3.0f;
    public string targetTag = "Cassetti";
    public float rotationSpeed = 2.0f;
    public float lookThreshold = 120.0f;

    private GameObject[] targets;
    private float distance;
    private Transform closestTarget;
    private Quaternion lookRotation;
    private Vector3 direction;

    public AnimationClip clip1;

    void CheckDistance(){
        targets = GameObject.FindGameObjectsWithTag(targetTag);

        closestTarget = null;
        foreach (GameObject target in targets)
        {
            if (target != gameObject){
                distance = Vector3.Distance(transform.position, target.transform.position);

                float angle = Vector3.Angle(transform.forward, direction);

                // 일정 거리 이하일 때 가장 가까운 타겟 저장
                if (distance < checkDistance && angle < lookThreshold)
                {
                    if (closestTarget == null || distance < Vector3.Distance(transform.position, closestTarget.position))
                    {
                        closestTarget = target.transform;
                    }
                }
            }
        }

        if (closestTarget != null)
        {
            direction = (closestTarget.position - transform.position).normalized;
            lookRotation = Quaternion.LookRotation(direction);

            if (Vector3.Dot(transform.right, direction) == 0)
            {
                lookRotation *= Quaternion.AngleAxis(180f, Vector3.up);
            }

            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);
            nav.isStopped = true;
            StartCoroutine(PlayAnimation());
            //Invoke("onMove",clip1.length);
        }
    }

    private IEnumerator PlayAnimation()
    {
        anim.SetBool("meet", true);
        yield return new WaitForSeconds(clip1.length);
        anim.SetBool("meet", false);
        canMeet = false;

        Vector3 randomPoint = transform.position + Random.insideUnitSphere * Random.Range(5.0f, 20.0f);
        if (RandomPoint(randomPoint, out point))
        {
            nav.SetDestination(randomPoint);
        }
        nav.isStopped = false;

    }

    private IEnumerator DelayMovement()
    {
        yield return new WaitForSeconds(4.0f);
        anim.SetBool("play", true);
        yield return new WaitForSeconds(0.5f);
        canNav = true;
        canMeet = true;
    
        // Vector3 randomPoint = transform.position + Random.insideUnitSphere * Random.Range(5.0f, 20.0f);
        int rand = Random.Range(1, 4);
        Vector3 randomPoint;
        if(rand == 1) randomPoint= new Vector3(20.0f, 0.0f, -7.0f) + Random.insideUnitSphere * Random.Range(0.0f, 2.0f);
        else if(rand == 2) randomPoint= new Vector3(17.0f, 0.0f, -18.0f) + Random.insideUnitSphere * Random.Range(0.0f, 2.0f);
        else randomPoint= new Vector3(29.0f, 0.0f, 2.0f) + Random.insideUnitSphere * Random.Range(0.0f, 2.0f);

        if (RandomPoint(randomPoint, out point))
        {
            nav.SetDestination(randomPoint);
            nav.isStopped = false;
            anim.SetBool("walk", true);
        }
    }
}
