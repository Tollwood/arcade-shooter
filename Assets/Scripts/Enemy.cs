using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Enemy : LivingEntity {

    public enum State { Idle, Chasing, Attacking };
    State currentState;
    NavMeshAgent pathFinder;
    Material skinMaterial;
    Color originalColour;
    float myCollisionRadius;

    Transform target;
    LivingEntity targetEntity;
    float targetCollisionRadius;
    bool hasTarget;

    float attackDistanceThreshold = .5f;
    float timeBetweenAttacks = 1;
    float nextAttackTime;
    float damage = 1;

    protected override void Start () {
        ConfigureSelf();
        target = GameObject.FindWithTag("Player").transform;
        if(target != null){
            ConfigureTarget();
        }
	}

    private void Update()
    {
        if(hasTarget && currentState != State.Attacking && Time.time > nextAttackTime && withinAttackRange()){
            StartCoroutine(Attack());
        }
    }

    private bool withinAttackRange()
    {
        float sqrDstToTarget = (target.position - transform.position).sqrMagnitude;
        return sqrDstToTarget < Mathf.Pow(attackDistanceThreshold + myCollisionRadius + targetCollisionRadius, 2);
    }

    IEnumerator Attack(){
        nextAttackTime = Time.time + timeBetweenAttacks;
        currentState = State.Attacking;
        pathFinder.enabled = false;
        Vector3 originalPosition = transform.position;
        Vector3 dirToTarget = (target.position - transform.position).normalized;
        Vector3 attackPosition = target.position - dirToTarget * myCollisionRadius;

        float percent = 0;
        float attackSpeed = 3;

        skinMaterial.color = Color.red;
        bool hasAppliedDamage = false;

        if (percent <= 1){
            percent += Time.deltaTime * attackSpeed;
            float interpolation = (-Mathf.Pow(percent, 2) + percent) * 4;
            transform.position = Vector3.Lerp(originalPosition, attackPosition, interpolation);
            yield return null;
        }
        targetEntity.TakeDamage(damage);
        skinMaterial.color = originalColour;
        currentState = State.Chasing;
        pathFinder.enabled = true;
    }

    private void ConfigureTarget()
    {
        targetEntity = target.GetComponent<LivingEntity>();
        targetEntity.onDeath += OnTargetDeath;
        targetCollisionRadius = target.GetComponent<CapsuleCollider>().radius;
        currentState = State.Chasing;
        hasTarget = true;
        StartCoroutine(UpdatePath());
    }

    private void OnTargetDeath()
    {
        hasTarget = false;
        currentState = State.Idle;
    }

    IEnumerator UpdatePath(){
        float refreshRate = .25f;
        while(hasTarget){
            if(currentState == State.Chasing){
                Vector3 dirToTarget = (target.position - transform.position).normalized;
                Vector3 targetPosition = target.position - dirToTarget * (myCollisionRadius + targetCollisionRadius + attackDistanceThreshold / 2f);
                if (!dead)
                {
                    pathFinder.SetDestination(targetPosition);
                }
                yield return new WaitForSeconds(refreshRate);
            }
        }
    }

    private void ConfigureSelf()
    {
        base.Start();
        pathFinder = GetComponent<NavMeshAgent>();
        myCollisionRadius = GetComponent<CapsuleCollider>().radius;
        skinMaterial = GetComponent<Renderer>().material;
        originalColour = skinMaterial.color;
    }
}
