using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

public class MoveCamera : MonoBehaviour
{
    private readonly float MIN_TRESHOLD = 0.01f;

    private DifficultyManager difficultyManager;
    private PauseController pauseController;

    [SerializeField]
    private AnimationCurve speedCurve;
    [SerializeField]
    private float returnSpeed;
    [SerializeField]
    private Vector3 startPosition;
    [SerializeField]
    private Vector3 endPosition;
    [SerializeField]
    private AnimationCurve speedByDifficultyCurve;

    private IEnumerator moveCoroutine;
    private IEnumerator returnCoroutine;

    private bool isPause;

    public void StopMove()
    {
        if (moveCoroutine == null)
            return;

        StopCoroutine(moveCoroutine);
        moveCoroutine = null;
    }

    public void StartMove()
    {
        moveCoroutine = MoveAnimationByTime();
        StartCoroutine(moveCoroutine);
    }

    public void StartReturn()
    {
        returnCoroutine = ReturnAnimationByTime();
        StartCoroutine(returnCoroutine);
    }

    private IEnumerator MoveAnimationByTime()
    {
        var distanceBetweenPoints = Vector3.Distance(startPosition, endPosition);

        while(true)
        {
            if (isPause)
            {
                yield return null;
                continue;
            }

            var distanceToEnd = Vector3.Distance(transform.position, endPosition);
            if (distanceToEnd < MIN_TRESHOLD)
                break;

            var completenessCoef = 1 - Mathf.Clamp(distanceToEnd, 0, distanceBetweenPoints) / distanceBetweenPoints;
            var a = speedCurve.Evaluate(completenessCoef);
            var b = speedByDifficultyCurve.Evaluate(difficultyManager.Difficulty);
            var speed =  a *b ;

            var newPosition = Vector3.MoveTowards(transform.position, endPosition, speed);
            transform.position = newPosition;

            yield return null;
        }

        Death();
    }

    private IEnumerator ReturnAnimationByTime()
    {
        while (Vector3.Distance(transform.position, startPosition) > MIN_TRESHOLD)
        {
            var newPosition = Vector3.MoveTowards(transform.position, startPosition, returnSpeed * Time.deltaTime);
            transform.position = newPosition;

            yield return null;
        }

        StartMove();

        yield return null;
    }

    private void Death()
    {
        var currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }

    private void OnPauseChanged(bool isPause)
        => this.isPause = isPause;

    private void OnDestroy()
    {
        pauseController.PauseChanged -= OnPauseChanged;
    }

    [Inject]
    private void Init(DifficultyManager difficultyManager, PauseController pauseController)
    {
        this.difficultyManager = difficultyManager;
        this.pauseController = pauseController;

        this.pauseController.PauseChanged += OnPauseChanged;
    }
}
