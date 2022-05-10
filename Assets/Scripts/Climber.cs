using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Climber : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetClimbingHand(MonoBehaviour hand)
    {
        if (hand != null)
        {
            Debug.Log("Climber.SetClimbingHand mano agarrada " + hand.gameObject.name);
        }
        else
        {
            Debug.Log("Climber.SetClimbingHand no estoy agarrado");
        }
    }
}
