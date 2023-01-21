using System.Collections;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(Monkey))]
public class SmoothJump : MonoBehaviour
{
    private PlayerController playerController;
    private Monkey monkey;

    [SerializeField]
    private float maxHeight;
    [SerializeField]
    private AnimationCurve jumpCurve;
    [SerializeField]
    private float animationSpeedMultiplier;

    public Animator Animator { get; set; }

    public float MaxHeight => maxHeight;

    public AnimationCurve JumpCurve => jumpCurve;

    private IEnumerator jumpCoroutine;

    public void Jump(float power)
    {
        monkey.Hop();
        Animator.SetTrigger("Flip");
        Animator.speed = playerController.MaxPower / power;
        jumpCoroutine = AnimationByTime(power);
        StartCoroutine(jumpCoroutine);
    }

    public void StopJump()
    {
        if (jumpCoroutine == null)
            return;

        Animator.SetTrigger("Idle");
        StopCoroutine(jumpCoroutine);
    }

    private IEnumerator AnimationByTime(float power)
    {
        var startPosition = transform.position;

        var duration = 0f;
        var curveTime = jumpCurve.keys[jumpCurve.keys.Length - 1].time * animationSpeedMultiplier;

        Animator.speed = animationSpeedMultiplier;

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
    private void Init(PlayerController playerController, Monkey monkey)
    {
        this.playerController = playerController;
        this.monkey = monkey;
    }
}
