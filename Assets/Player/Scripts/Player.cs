using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private AnimationCurve jumpCurve;

    public void Jump(float power)
    {
        StartCoroutine(AnimationByTime(power));
    }

    private IEnumerator AnimationByTime(float power)
    {
        var duration = 0f;
        while(duration < 1)
        {
            var y = jumpCurve.Evaluate(duration) * power;

            transform.position = new Vector3(0, y, 0);

            duration += Time.deltaTime / power;
            yield return null;
        }

        yield return null;
    }
}
