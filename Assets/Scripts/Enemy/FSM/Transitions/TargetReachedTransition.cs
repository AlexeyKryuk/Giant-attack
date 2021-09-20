using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetReachedTransition : Transition
{
    [SerializeField] private float _range;

    private void Update()
    {
        //Debug.Log(Target);
        float distance = Vector3.Distance(Target.transform.position, transform.position);

        if (distance < _range)
        {
            NeedTransit = true;
        }
        else
        {
            NeedTransit = false;
        }
    }
}
