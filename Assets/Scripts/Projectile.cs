using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

    public LayerMask collisionMask;
    float speed = 10;
    int damage = 1;

    float timeToLife = 3;
    float skinWidth = .1f;

    public void SetSpeed(float newSpeed)
    {
        speed = newSpeed;
    }

    private void Start()
    {
        Destroy(gameObject, timeToLife);
        Collider[] initialCollisions = Physics.OverlapSphere(transform.position, .1f, collisionMask);
        if(initialCollisions.Length > 0){
            OnHitObject(initialCollisions[0]);
        }
    }

    void Update()
    {
        if (transform.position.x > 500){
            Destroy(gameObject);
        }
        float maxDistance = speed * Time.deltaTime;
        checkCollision(maxDistance);
        transform.Translate(Vector3.forward * maxDistance);
    }

    private void checkCollision(float maxDistance)
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, maxDistance + skinWidth, collisionMask, QueryTriggerInteraction.Collide)){
            OnHitObject(hit);
        }
    }

    private void OnHitObject(RaycastHit hit)
    {
        IDamagable damagableObject = hit.collider.GetComponent<IDamagable>();
        if(damagableObject != null){
            damagableObject.TakeHit(damage, hit);
        }
        Destroy(gameObject);
    }


    private void OnHitObject(Collider c)
    {
        IDamagable damagableObject = c.GetComponent<IDamagable>();
        if (damagableObject != null)
        {
            damagableObject.TakeDamage(damage);
        }
        Destroy(gameObject);
    }
}
