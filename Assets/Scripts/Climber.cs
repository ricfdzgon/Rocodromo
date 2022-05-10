using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Climber : MonoBehaviour
{
    CharacterController characterController;
    ActionBasedController climbingHand;
    AbstractSpeedometer climbinghandSpeedometer;
    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    void FixedUpdate()
    {
        if (climbingHand)
        {
            Climb(Time.fixedDeltaTime);
        }
    }

    public void SetClimbingHand(MonoBehaviour hand, bool grab)
    {
        if (grab)
        {
            Debug.Log("Climber.SetClimbingHand mano agarrada " + hand.gameObject.name);

            climbingHand = hand.GetComponent<ActionBasedController>();
            climbinghandSpeedometer = hand.GetComponent<Speedometer>();
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
