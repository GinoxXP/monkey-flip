using System.Collections;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(Rigidbody))]
public class SmoothJump : MonoBehaviour
{
    private PlayerController playerController;

    [SerializeField]
    private float maxHeight;
    [SerializeField]
    private AnimationCurve jumpCurve;
    [SerializeField]
    private bool isFlyMode;
    [SerializeField]
    private Vector3 flyPosition;
    [SerializeField]
    private float speedMultiplier;

    public Animator Animator { get; set; }

    private Rigidbody rigidbody;
    private IEnumerator jumpCoroutine;

    public void Jump(float power)
    {
        Animator.SetTrigger("Flip");
        Animator.speed = playerController.MaxPower / power;
        if (isFlyMode)
        {
            Fly();
        }
        else
        {
            jumpCoroutine = AnimationByTime(power);
            StartCoroutine(jumpCoroutine);
        }
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
        var curveTime = jumpCurve.keys[jumpCurve.keys.Length - 1].time * speedMultiplier;

        Animator.speed = speedMultiplier;

        while (duration < curveTime)
        {
            var y = jumpCurve.Evaluate(duration) * power * maxHeight;

            transform.position = startPosition + new Vector3(0, y, 0);

            duration += Time.deltaTime / (power / playerController.MaxPower);
            yield return null;
        }

        yield return null;
    }

    private void Fly()
    {
        rigidbody.useGravity = false;
        transform.position = flyPosition;
    }

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    [Inject]
    private void Init(PlayerController playerController)
    {
        this.playerController = playerController;
    }
}
