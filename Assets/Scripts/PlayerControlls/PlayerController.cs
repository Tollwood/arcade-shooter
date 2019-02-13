using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour {

    Vector3 velocity;
    Rigidbody myRigidbody;

    public float rotationSpeed = 2.0f;
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody>();
    }

    public void FixedUpdate()
    {

        myRigidbody.MovePosition(myRigidbody.position + transform.TransformDirection(velocity) * 1 * Time.deltaTime);

    }

    public void Move(Vector3 _velocity)
    {
        velocity = _velocity;
    }

    public void Rotate(float rotateTo)
    {
        transform.Rotate(new Vector3(0,rotateTo , 0));
    }

    public void LookUpDown(float rotationZ){

        Camera.main.transform.localEulerAngles = new Vector3(-rotationZ, 0,0 );
    }
}
