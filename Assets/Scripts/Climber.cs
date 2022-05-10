using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Climber : MonoBehaviour
{
    ActionBasedController climbingHand;
    void Start()
    {

    }

    void Update()
    {

    }

    public void SetClimbingHand(MonoBehaviour hand, bool grab)
    {
        if (grab)
        {
            Debug.Log("Climber.SetClimbingHand mano agarrada " + hand.gameObject.name);
            climbingHand = hand.GetComponent<ActionBasedController>();
        }
        else
        {
            if (hand.name == climbingHand.name)
            {
                Debug.Log("Climber.SetClimbingHand no estoy agarrado");
                climbingHand = null;
            }
        }
    }
}
