using UnityEngine;
using Zenject;

public class JumpAssistant : MonoBehaviour
{
    private Level level;
    private SmoothJump smoothJump;
    private MoveLevel moveLevel;

    public float GetPerfectPower()
    {
        var startSegmentPosition = level.CurrentSegment.position;
        var targetSegmentPosition = level.NextSegment.position;

        var monkeyPosition = smoothJump.transform.position;
        var curve = smoothJump.JumpCurve;
        var speed = moveLevel.Speed;

        var deltaSegmentPosition = startSegmentPosition - targetSegmentPosition;
        var timeMoveToTargetSegment = deltaSegmentPosition.x / speed;

        var curveResult = curve.Evaluate(timeMoveToTargetSegment);

        return curveResult;
    }

    [Inject]
    private void Init(Level level, SmoothJump smoothJump, MoveLevel moveLevel)
    {
        this.level = level;
        this.smoothJump = smoothJump;
        this.moveLevel = moveLevel;
    }
}
