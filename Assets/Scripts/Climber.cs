using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Climber : MonoBehaviour
{
    private Vector3 velocity;
    public ClimbingMode climbingMode;
    CharacterController characterController;
    ActionBasedController climbingHand;
    AbstractSpeedometer climbinghandSpeedometer;
    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        if (!climbingHand)
        {
            characterController.Move(velocity * Time.deltaTime);
        }
    }

    void FixedUpdate()
    {
        if (climbingHand)
        {
            Climb(Time.fixedDeltaTime);
        }

        if (!climbingHand)
        {
            velocity += Physics.gravity * Time.fixedDeltaTime;
        }
    }

    public void SetClimbingHand(MonoBehaviour hand, bool grab)
    {
        if (grab)
        {
            Debug.Log("Climber.SetClimbingHand mano agarrada " + hand.gameObject.name);

            climbingHand = hand.GetComponent<ActionBasedController>();
            if (climbingMode == ClimbingMode.bad)
            {
                climbinghandSpeedometer = hand.GetComponent<Speedometer>();
            }
            else
            {
                climbinghandSpeedometer = hand.GetComponent<HandControllerSpeedometer>();
            }
        }
        else
        {
            if (hand.name == climbingHand.name)
            {
                Debug.Log("Climber.SetClimbingHand no estoy agarrado");
                climbingHand = null;
                climbinghandSpeedometer = null;
            }
        }
    }

    private void Climb(float deltaTime)
    {
        characterController.Move(-climbinghandSpeedometer.GetVelocity() * deltaTime);
    }
}

public enum ClimbingMode
{
    good,
    bad
}
