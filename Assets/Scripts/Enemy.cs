using System.Collections;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Enemy : LivingEntity {

    NavMeshAgent pathFinder;
    Transform target;

    protected override void Start () {
        base.Start();
        pathFinder = GetComponent<NavMeshAgent>();
        target = GameObject.FindWithTag("Player").transform;
        StartCoroutine(UpdatePath());
	}

    IEnumerator UpdatePath(){
        float refreshRate = .5f;
        while(target != null){
            Vector3 targetPosition = new Vector3(transform.position.x, 0, transform.position.z);
            if(!dead){
                pathFinder.SetDestination(target.position);    
            }
            yield return new WaitForSeconds(refreshRate);
        }
    }
}
