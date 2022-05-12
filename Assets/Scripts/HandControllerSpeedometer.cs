using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class HandControllerSpeedometer : AbstractSpeedometer
{
    public InputActionProperty handControllerVelocityProperty;
    Vector3 velocity;
    void Start()
    {

    }

    void Update()
    {
        velocity = handControllerVelocityProperty.action.ReadValue<Vector3>();
    }
    public override Vector3 GetVelocity()
    {
        return velocity;
    }
}
