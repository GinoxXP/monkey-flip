using UnityEngine;
using Zenject;

public class JumpAssistant : MonoBehaviour
{
    private Level level;
    private SmoothJump smoothJump;
    private MoveLevel moveLevel;
    private DifficultyManager difficultyManager;

    [SerializeField]
    private AnimationCurve assistPowerByDifficultyCurve;

    public float GetAssistedPower(float power)
    {
        var perfectPower = GetPerfectPower();
        var assistPower = assistPowerByDifficultyCurve.Evaluate(difficultyManager.Difficulty);

        if (Mathf.Abs(power - perfectPower) <= assistPower)
            return perfectPower;

        if (power > perfectPower)
            power -= assistPower;
        else
            power += assistPower;

        return power;
    }

    private float GetPerfectPower()
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
        var intersectPointJ = GetIntersectOxPoint(curve, positionDelta.y / maxHeight);

        var deltaIntersectPoint = intersectPointJ - intersectPointI;

        var intersectPoint = intersectPointI + deltaIntersectPoint;

        var result = Mathf.Abs(targetBranchPosition.x) / (intersectPoint * speed);

        return result;
    }

    private float GetIntersectOxPoint(AnimationCurve curve, float verticalBorderHeightDelta)
    {
        var step = 0.05f;
        var curveLenght = curve.keys[curve.keys.Length - 1].time;

        var x = 0f;
        float? lastSign = null;

        float xResult = 0;
        while (x <= curveLenght)
        {
            var currentSign = Mathf.Sign(curve.Evaluate(x) - verticalBorderHeightDelta);

            if (lastSign.HasValue)
            {
                if(lastSign.Value >= 0 && currentSign < 0)
                {
                    xResult = x;
                    break;
                }
            }

            lastSign = currentSign;

            x += step;
        }

        return xResult;
    }

    [Inject]
    private void Init(Level level, SmoothJump smoothJump, MoveLevel moveLevel, DifficultyManager difficultyManager)
    {
        this.level = level;
        this.smoothJump = smoothJump;
        this.moveLevel = moveLevel;
        this.difficultyManager = difficultyManager;
    }
}
