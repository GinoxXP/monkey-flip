using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Animator))]
public class RotateCamera : MonoBehaviour
{
    [SerializeField]
    private AnimationCurve speedMultiplicatorCurve;

    private Animator animator;
    private IEnumerator animationSpeedCoroutine;

    public void CameraBack()
    {
        animator.SetTrigger("CameraBack");
        StopCoroutine(animationSpeedCoroutine);
    }

    public void StartSpeedMultiplicatorByTime()
    {
        animationSpeedCoroutine = AnimationSpeedMultiplicatorByTime();
        StartCoroutine(animationSpeedCoroutine);
    }

    private IEnumerator AnimationSpeedMultiplicatorByTime()
    {
        var duration = 0f;
        var maxDuration = speedMultiplicatorCurve.keys.LastOrDefault().time;
        while (duration < maxDuration)
        {
            animator.speed = speedMultiplicatorCurve.Evaluate(duration);

            duration += Time.deltaTime;
        }

        yield return null;
    }

    private void Death()
    {
        /// TODO 
        /// Remake this

        var currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }

    private void Start()
    {
        animator = GetComponent<Animator>();
        StartSpeedMultiplicatorByTime();
    }
}
