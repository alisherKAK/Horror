using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Monster : MonoBehaviour
{
    [SerializeField]
    private Transform player;

    [SerializeField]
    private NavMeshAgent navMesh;

    [SerializeField]
    private Animator animator;

    [SerializeField]
    private Transform eyes;

    [SerializeField]
    private AudioSource scream;

    private string state = "idle";
    private bool isAlive = true;
    private float waitTime = 2f;

    private void Start()
    {
        navMesh.speed = 1f;
        animator.speed = 1f;
    }

    private void Update()
    {
        if(!isAlive)
        {
            return;
        }

        animator.SetFloat("Velocity", navMesh.velocity.magnitude);
        if(state == "idle")
        {
            GoToRandomPoint();
        }
        
        if(state == "walk")
        {
            StopWalking();
        }

        if(state == "search")
        {
            SearchForPlayer();
        }

        if(state == "chase")
        {
            ChaseForPlayer();
        }
    }

    public void CheckSight()
    {
        if(!isAlive)
        {
            return;
        }

        RaycastHit hit;
        if(Physics.Linecast(eyes.position, player.position, out hit))
        {
            if(hit.collider.tag == "Player")
            {
                if(state == "kill")
                {
                    return;
                }

                scream.Play();

                state = "chase";
                navMesh.speed = 2f;
                animator.speed = 2f;
            }
        }
    }

    private void GoToRandomPoint()
    {
        Vector3 randomPosition = Random.insideUnitSphere * 20;
        NavMeshHit navMeshHit;

        NavMesh.SamplePosition(transform.position + randomPosition, out navMeshHit, 20f, NavMesh.AllAreas);
        navMesh.SetDestination(navMeshHit.position);
        state = "walk";
    }

    private void StopWalking()
    {
        if(navMesh.remainingDistance <= navMesh.stoppingDistance && !navMesh.pathPending)
        {
            state = "search";
            waitTime = 5f;
        }
    }
    
    private void SearchForPlayer()
    {
        if(waitTime > 0)
        {
            waitTime -= Time.deltaTime;
            transform.Rotate(0, 120f * Time.deltaTime, 0);
        }
        else
        {
            state = "idle";
        }
    }

    private void ChaseForPlayer()
    {
        navMesh.SetDestination(player.position);
    }

    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, 20f);
    }
}
