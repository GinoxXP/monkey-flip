using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    private readonly float MIN_TRESHOLD = 0.2f;

    [SerializeField]
    private AnimationCurve speedCurve;
    [SerializeField]
    private float speed;
    [SerializeField]
    private float returnSpeed;
    [SerializeField]
    private Vector3 startPosition;
    [SerializeField]
    private Vector3 endPosition;

    private IEnumerator moveCoroutine;
    private IEnumerator returnCoroutine;

    private IEnumerator MoveAnimationByTime()
    {
        var distanceBetweenPoints = Vector3.Distance(startPosition, endPosition);

        while(true)
        {
            var distanceToEnd = Vector3.Distance(transform.position, endPosition);
            if (distanceToEnd == MIN_TRESHOLD)
                break;

            var completenessCoef = 1 - Mathf.Clamp(distanceToEnd, 0, distanceBetweenPoints) / distanceBetweenPoints;

            var newPosition = Vector3.MoveTowards(transform.position, endPosition, speed * speedCurve.Evaluate(completenessCoef));
            transform.position = newPosition;

            yield return null;
        }

        yield return null;
    }

    private IEnumerator ReturnAnimationByTime()
    {
        while (Vector3.Distance(transform.position, startPosition) > MIN_TRESHOLD)
        {
            var newPosition = Vector3.MoveTowards(transform.position, startPosition, returnSpeed);
            transform.position = newPosition;

            yield return null;
        }

        StartMove();

        yield return null;
    }

    public void StopMove()
    {
        if (moveCoroutine == null)
            return;

        StopCoroutine(moveCoroutine);
        moveCoroutine = null;
    }

    public void StartMove()
    {
        moveCoroutine = MoveAnimationByTime();
        StartCoroutine(moveCoroutine);
    }

    public void StartReturn()
    {
        returnCoroutine = ReturnAnimationByTime();
        StartCoroutine(returnCoroutine);
    }
}
