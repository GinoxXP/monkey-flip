using UnityEngine;
using Zenject;

public class JumpAssistant : MonoBehaviour
{
    private Level level;
    private SmoothJump smoothJump;
    private MoveLevel moveLevel;

    public float GetPerfectPower()
    {
        var currentBranch = level.CurrentSegment.GetComponent<Segment>().Branch;
        var targetBranch = level.NextSegment.GetComponent<Segment>().Branch;

        var currentBranchPosition = currentBranch.transform.position;
        var targetBranchPosition = targetBranch.transform.position;
        var speed = moveLevel.Speed;
        var curve = smoothJump.JumpCurve;
        var maxHeight = smoothJump.MaxHeight;

        var currentPointPosition = smoothJump.transform.position;
        var targetPointPosition = targetBranchPosition + (currentPointPosition - currentBranchPosition);

        var positionDelta = targetPointPosition - Vector3.up * currentPointPosition.y;

        var intersectPointI = GetIntersectOxPoint(curve, 0);
        var intersectPointJ = GetIntersectOxPoint(curve, positionDelta.y / smoothJump.MaxHeight);

        var deltaIntersectPoint = intersectPointJ - intersectPointI;

        var intersectPoint = intersectPointI + deltaIntersectPoint;

        var result = Mathf.Abs(targetBranchPosition.x) / ((intersectPointI + deltaIntersectPoint) * speed);

        return result;
    }

    private float GetIntersectOxPoint(AnimationCurve curve, float heightDelta)
    {
        var step = 0.05f;
        var curveTime = curve.keys[curve.keys.Length - 1].time;

        var progress = 0f;
        float? lastSign = null;

        float intersectOxPoint = 0;
        while (progress <= curveTime)
        {
            var currentSign = Mathf.Sign(curve.Evaluate(progress) - heightDelta);

            if (lastSign.HasValue)
            {
                if(lastSign.Value >= 0 && currentSign < 0)
                {
                    intersectOxPoint = progress;
                    break;
                }
            }

            lastSign = currentSign;

            progress += step;
        }

        return intersectOxPoint;
    }

    [Inject]
    private void Init(Level level, SmoothJump smoothJump, MoveLevel moveLevel)
    {
        this.level = level;
        this.smoothJump = smoothJump;
        this.moveLevel = moveLevel;
    }
}
