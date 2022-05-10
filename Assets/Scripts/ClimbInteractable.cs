using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
public class ClimbInteractable : XRBaseInteractable
{
    public Climber climber;
    void Start()
    {
        if (climber == null)
        {
            Debug.Log("ClimbInteractable.Start: climber no establecido");
        }
    }
    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        base.OnSelectEntered(args);

        MonoBehaviour interactorComponent = (MonoBehaviour)args.interactorObject;
        climber.SetClimbingHand(interactorComponent);
    }

    protected override void OnSelectExited(SelectExitEventArgs args)
    {
        base.OnSelectExited(args);

        climber.SetClimbingHand(null);
    }
}
