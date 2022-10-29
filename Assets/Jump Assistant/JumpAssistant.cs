using UnityEngine;
using Zenject;

public class JumpAssistant : MonoBehaviour
{
    private Level level;
    private SmoothJump smoothJump;
    private MoveLevel moveLevel;

    private float intersectOxPoint;

    public float GetPerfectPower()
    {
        var targetSegmentPosition = level.NextSegment.position;

        var speed = moveLevel.Speed;

        var result = Mathf.Abs(targetSegmentPosition.x) / (intersectOxPoint * speed);
        return result;
    }

    private void FindIntersectOxPoint()
    {
        var step = 0.05f;
        var curve = smoothJump.JumpCurve;
        var curveTime = curve.keys[curve.keys.Length - 1].time;

        var progress = 0f;
        float? sign = null;

        while (progress <= curveTime)
        {
            if (!sign.HasValue)
            {
                sign = Mathf.Sign(curve.Evaluate(progress));
            }
            else
            {
                if(sign != Mathf.Sign(curve.Evaluate(progress)))
                {
                    intersectOxPoint = progress;
                    break;
                }
            }

            progress += step;
        }
    }

    [Inject]
    private void Init(Level level, SmoothJump smoothJump, MoveLevel moveLevel)
    {
        this.level = level;
        this.smoothJump = smoothJump;
        this.moveLevel = moveLevel;

        FindIntersectOxPoint();
    }
}
