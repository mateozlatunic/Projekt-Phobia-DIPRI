using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] float idleTime = 2f;
    [SerializeField] float walkSpeed = 5f;
    [SerializeField] float chaseSpeed = 8f;
    [SerializeField] float sightDistance = 10f;
    [SerializeField] float attackRange = 5f;
    [SerializeField] float rotationSpeed = 5f;
    public Transform[] waypoints;

    [SerializeField] AudioClip idleSound;
    [SerializeField] AudioClip patrolSound;
    [SerializeField] AudioClip chaseSound;
    [SerializeField] AudioClip attackSound;
    AudioSource audioSource;

    int currentWaypointIndex = 0;
    float idleTimer = 0;
    float distanceToTarget = Mathf.Infinity;

    NavMeshAgent navMeshAgent;
    Animator animator;

    public enum EnemyState { Idle, Patrol, Chase, Attack }
    public EnemyState currentState = EnemyState.Idle;

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        SetDestinationToWaypoint();
    }

    void Update()
    {
        distanceToTarget = Vector3.Distance(target.position, transform.position);
        switch (currentState)
        {
            case EnemyState.Idle:
                idleTimer += Time.deltaTime;
                animator.SetBool("IsIdle", true);
                animator.SetBool("IsMoving", false);
                animator.SetBool("IsAttacking", false);
                PlaySound(idleSound);

                if (idleTimer >= idleTime)
                {
                    NextWaypoint();
                }

                CheckPlayerDetection();
                break;

            case EnemyState.Patrol:
                idleTimer = 0f;
                animator.SetBool("IsIdle", false);
                animator.SetBool("IsMoving", true);
                animator.SetBool("IsAttacking", false);
                PlaySound(patrolSound);

                if (navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance)
                {
                    currentState = EnemyState.Idle;
                }

                CheckPlayerDetection();
                break;

            case EnemyState.Chase:
                idleTimer = 0f;
                animator.SetBool("IsIdle", false);
                animator.SetBool("IsMoving", true);
                animator.SetBool("IsAttacking", false);
                navMeshAgent.speed = chaseSpeed; 
                navMeshAgent.SetDestination(target.position);
                FaceTarget();
                PlaySound(chaseSound);

                if (Vector3.Distance(transform.position, target.position) > sightDistance)
                {
                    currentState = EnemyState.Patrol;
                    navMeshAgent.speed = walkSpeed;
                }

                if (distanceToTarget <= attackRange)
                {
                    currentState = EnemyState.Attack;
                }
                break;

            case EnemyState.Attack:
                idleTimer = 0f;
                animator.SetBool("IsIdle", false);
                animator.SetBool("IsMoving", false);
                animator.SetBool("IsAttacking", true);
                AttackTarget();
                PlaySound(attackSound);

                break;
        }
    }
    void AttackTarget()
    {
        FaceTarget();
        animator.SetBool("IsAttacking", true);

        if (distanceToTarget <= attackRange)
        {
            navMeshAgent.velocity = Vector3.zero;
        }

        if (distanceToTarget > navMeshAgent.stoppingDistance)
        {
            currentState = EnemyState.Chase;
        }
    }

    public void CheckPlayerDetection()
    {
        RaycastHit hit;
        Vector3 playerDirection = target.position - transform.position;

        if(Physics.Raycast(transform.position, playerDirection.normalized, out hit, sightDistance))
        {
            if (hit.collider.CompareTag("Player"))
            {
                currentState = EnemyState.Chase;
            }
        }
    }
    void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position);
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 20f, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);
    }

    void PlaySound(AudioClip soundClip)
    {
        if (!audioSource.isPlaying || audioSource.clip != soundClip)
        {
            audioSource.clip = soundClip;
            audioSource.Play();
        }
    }

    void NextWaypoint()
    {
        currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
        SetDestinationToWaypoint();
    }
    void SetDestinationToWaypoint()
    {
        navMeshAgent.SetDestination(waypoints[currentWaypointIndex].position);
        navMeshAgent.speed = walkSpeed;
        currentState = EnemyState.Patrol;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, sightDistance);
    }
}