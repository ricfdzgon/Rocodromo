using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Climber : MonoBehaviour
{
    private ContiniusMovement continiusMovement;
    private Vector3 velocity;
    public ClimbingMode climbingMode;
    CharacterController characterController;
    ActionBasedController climbingHand;
    //secondaryHand guarda la referencia a la mano con la que estbamos previamente
    //agarrados, mientras esta no se libere el agarre del ClimbingKnob
    ActionBasedController secondaryHand;
    AbstractSpeedometer climbinghandSpeedometer;
    void Start()
    {
        characterController = GetComponent<CharacterController>();

        continiusMovement = GetComponent<ContiniusMovement>();
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
            velocity.y = 0;
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

            //Antes de quedarnos con la nueva climbingHand, guardamos la referencia
            //de la actual en secondaryHand
            secondaryHand = climbingHand;

            climbingHand = hand.GetComponent<ActionBasedController>();
            climbinghandSpeedometer = GetSpeedometer(climbingHand);

            //Como estamos agarrados para trepar, desactivamos el movimiento continuo
            if (continiusMovement != null)
            {
                continiusMovement.enabled = false;
            }
        }
        else
        {
            //Si grab es false, quiere decir que se soltó la mano que representa el parametro hand

            if (hand.name == climbingHand.name)
            {
                climbingHand = secondaryHand;
                if (climbingHand == null)
                {
                    Debug.Log("Climber.SetClimbingHand no estoy agarrado");
                    climbinghandSpeedometer = null;

                    //Dejamos de estar agarrados, por lo tanto activamos el movimiento continuo
                    if (continiusMovement != null)
                    {
                        continiusMovement.enabled = true;
                    }
                }
                else
                {
                    climbinghandSpeedometer = GetSpeedometer(climbingHand);
                }
            }
            else
            {
                secondaryHand = null;
            }
        }
    }

    private void Climb(float deltaTime)
    {
        characterController.Move(-climbinghandSpeedometer.GetVelocity() * deltaTime);
    }

    private AbstractSpeedometer GetSpeedometer(ActionBasedController hand)
    {
        if (climbingMode == ClimbingMode.bad)
        {
            return hand.GetComponent<Speedometer>();
        }
        else
        {
            return hand.GetComponent<HandControllerSpeedometer>();
        }
    }
}

public enum ClimbingMode
{
    good,
    bad
}
