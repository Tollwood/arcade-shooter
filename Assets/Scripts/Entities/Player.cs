using UnityEngine;

[RequireComponent(typeof(PlayerController))]
[RequireComponent(typeof(GunController))]
public class Player : LivingEntity{

    public float moveSpeed = 5;
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
        if (rightJoystick.GetMagnitude() > 0.7f)
        {
            gunController.Shoot();
        }
    }

    private void handleMovement()
    {
        Vector3 moveVector = leftJoystick.GetInputDirection();
        Vector3 moveInput = new Vector3(moveVector.x, 0, moveVector.y);
        Vector3 moveVelocity = moveInput.normalized * moveSpeed;

        controller.Move(moveVelocity);
    }

    private void handleLookAt()
    {
        Vector3 point = transform.position + new Vector3(rightJoystick.GetInputDirection().x, 0, rightJoystick.GetInputDirection().y);
        controller.LookAt(point);
    }
}
