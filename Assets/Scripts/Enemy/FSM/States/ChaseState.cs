using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ChaseState : State
{
    [SerializeField] private float _moveSpeed;

    protected override void OnEnable()
    {
        base.OnEnable();
        Animator.SetBool("Move", true);
    }

    private void Update()
    {
        if (Target != null)
        {
            MoveToTarget(Target.transform.position, _moveSpeed);
            RotateToTarget(Target.transform.position);
        }
    }

    private void MoveToTarget(Vector3 target, float speed)
    {
        Vector2 currentPos = new Vector2(transform.position.x, transform.position.z);
        Vector2 targetPos = new Vector2(target.x, target.z);

        Vector2 vector2Pos = Vector2.MoveTowards(currentPos, targetPos, speed * Time.deltaTime);

        Vector3 newPos = new Vector3(vector2Pos.x, transform.position.y, vector2Pos.y);
        transform.position = newPos;
    }

    private void RotateToTarget(Vector3 target)
    {
        Vector3 targetDirection = (target - transform.position).normalized;

        if (targetDirection != Vector3.zero)
        {
            Quaternion lookRotation = Quaternion.LookRotation(new Vector3(targetDirection.x, 0, targetDirection.z));
            transform.rotation = Quaternion.Lerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
        }
    }
}
