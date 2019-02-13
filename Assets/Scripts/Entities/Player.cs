using UnityEngine;

[RequireComponent(typeof(PlayerController))]
[RequireComponent(typeof(GunController))]
public class Player : LivingEntity{

    float moveSpeed = 5;
    float rotationSpeed = 6;
    float upDownSpeed = 2;
    private float rotationZ = 0f;
    LeftJoystick leftJoystick;
    RightJoystick rightJoystick;
    PlayerController controller;
    GunController gunController;

    protected override void Start()
    {
        base.Start();
        controller = GetComponent<PlayerController>();
        gunController = GetComponent<GunController>();
        leftJoystick = FindObjectOfType<LeftJoystick>();
        rightJoystick = FindObjectOfType<RightJoystick>();
    }

    protected override void Update () {
        base.Update();
        handleMovement();
        handleShooting();
        handleLookAt();
	}

    private void handleShooting()
    {
        if (rightJoystick != null && rightJoystick.GetMagnitude() > 0.7f)
        {
            gunController.Shoot();
        }
    }

    private void handleMovement()
    {
        if(leftJoystick != null){
            Vector3 moveVector = leftJoystick.GetInputDirection();
            Vector3 moveInput = new Vector3(moveVector.x, 0, moveVector.y);
            Vector3 moveVelocity = moveInput.normalized * moveSpeed;

            controller.Move(moveVelocity);    
        }
    }

    private void handleLookAt()
    {
        if(rightJoystick != null){
            
            controller.Rotate(rightJoystick.GetInputDirection().x * rotationSpeed);
            rotationZ += Input.acceleration.y + 0.3f * upDownSpeed;
            rotationZ = Mathf.Clamp(rotationZ, -30, 30);
            // controller.LookUpDown(rotationZ);
        }
    }
}
