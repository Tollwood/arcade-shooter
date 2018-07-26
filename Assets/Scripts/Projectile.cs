using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

    public LayerMask collisionMask;
    float speed = 10;

    public void SetSpeed(float newSpeed)
    {
        speed = newSpeed;
    }

    void Update()
    {
        float maxDistance = speed * Time.deltaTime* speed;
        checkCollision(maxDistance);
        transform.Translate(Vector3.forward * speed);
    }

    private void checkCollision(float maxDistance)
    {
        Ray ray = new Ray(transform.position, Vector3.forward);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, maxDistance, collisionMask, QueryTriggerInteraction.Collide)){
            OnHitObject();
        }
    }

    private void OnHitObject()
    {
        Destroy(gameObject);
    }
}
