using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ChaseState : State
{
    [SerializeField] private float _moveSpeed;

    private Coroutine _coroutine;

    protected override void OnEnable()
    {
        base.OnEnable();
        Animator.SetBool("Move", true);
        Enemy.Damaged += OnTakeDamage;
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        Animator.SetBool("Move", false);
        Enemy.Damaged -= OnTakeDamage;
    }

    private void Update()
    {
        if (Target != null)
        {
            MoveToTarget(Target.transform.position, _moveSpeed);
        }
    }

    private void MoveToTarget(Vector3 target, float speed)
    {
        RotateToTarget(target);

        Vector2 currentPos = new Vector2(transform.position.x, transform.position.z);
        Vector2 targetPos = new Vector2(target.x, target.z);

        Vector2 vector2Pos = Vector2.MoveTowards(currentPos, targetPos, speed * Time.unscaledDeltaTime);

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

    private void OnTakeDamage()
    {
        if (_coroutine == null)
            _coroutine = StartCoroutine(StopMoving());
    }

    private IEnumerator StopMoving()
    {
        Player currentTarget = Target;
        Target = null;
        float animationLength = Animator.GetCurrentAnimatorClipInfo(0).Length - 0.2f;

        yield return new WaitForSeconds(animationLength);

        Target = currentTarget;
        _coroutine = null;
    }
}
