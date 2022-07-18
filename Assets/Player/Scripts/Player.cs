using System.Collections;
using UnityEngine;
using Zenject;

public class Player : MonoBehaviour
{
    private PlayerController playerController;

    [SerializeField]
    private AnimationCurve jumpCurve;

    public void Jump(float power)
    {
        StartCoroutine(AnimationByTime(power));
    }

    private IEnumerator AnimationByTime(float power)
    {
        var duration = 0f;
        var curveTime = jumpCurve.keys[jumpCurve.keys.Length - 1].time;

        while (duration < curveTime)
        {
            var y = jumpCurve.Evaluate(duration) * power;

            transform.position = new Vector3(0, y, 0);

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
