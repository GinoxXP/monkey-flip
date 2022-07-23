using System.Collections;
using UnityEngine;
using Zenject;

public class SmoothJump : MonoBehaviour
{
    private PlayerController playerController;

    [SerializeField]
    private float maxHeight;
    [SerializeField]
    private AnimationCurve jumpCurve;

    private IEnumerator jumpCoroutine;

    public void Jump(float power)
    {
        jumpCoroutine = AnimationByTime(power);
        StartCoroutine(jumpCoroutine);
    }

    public void StopJump()
    {
        if (jumpCoroutine == null)
            return;
            
        StopCoroutine(jumpCoroutine);
    }

    private IEnumerator AnimationByTime(float power)
    {
        var startPosition = transform.position;

        var duration = 0f;
        var curveTime = jumpCurve.keys[jumpCurve.keys.Length - 1].time;

        while (duration < curveTime)
        {
            var y = jumpCurve.Evaluate(duration) * power * maxHeight;

            transform.position = startPosition + new Vector3(0, y, 0);

            duration += Time.deltaTime / (power / playerController.MaxPower);
            yield return null;
        }

        yield return null;
    }

    [Inject]
    private void Init(PlayerController playerController)
    {
        this.playerController = playerController;
    }
}
