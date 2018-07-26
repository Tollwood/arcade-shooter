using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Enemy : MonoBehaviour {

    NavMeshAgent pathFinder;
    Transform target;

	void Start () {
        pathFinder = GetComponent<NavMeshAgent>();
        target = GameObject.FindWithTag("Player").transform;
        StartCoroutine(UpdatePath());
	}
	
	void Update () {
        
	}

    IEnumerator UpdatePath(){
        float refreshRate = .5f;
        while(target != null){
            Vector3 targetPosition = new Vector3(transform.position.x, 0, transform.position.z);
            pathFinder.SetDestination(target.position);
            Debug.Log("Destination updated");
            yield return new WaitForSeconds(refreshRate);
        }
    }
}
