using System.Collections;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(BackgroundManager))]
public class MoveLevel : MonoBehaviour
{
    private Score scoreManager;
    private BackgroundManager backgroundManager;
    private Level levelController;

    [SerializeField]
    private float speed;
    [SerializeField]
    private AnimationCurve curvePosition;
    [SerializeField]
    private Transform destroingZone;
    [SerializeField]
    private float startTransformPositionZ;

    private IEnumerator moveCoroutine;
    private float trail;

    public float Speed => speed;

    public void Move()
    {
        trail = 0;
        backgroundManager.Move();
        moveCoroutine = AnimationByTime();
        StartCoroutine(moveCoroutine);
    }

    public void StopMove(bool ignoreScore = false)
    {
        if (moveCoroutine == null)
            return;

        backgroundManager.StopMove();

        StopCoroutine(moveCoroutine);

        if (ignoreScore)
            return;

        scoreManager.ScoreValue += (int)trail;
    }

    private IEnumerator AnimationByTime()
    {
        while (true)
        {
            foreach(var branch in levelController.Segments)
            {
                trail += speed * Time.deltaTime;

                var newPosition = new Vector3(
                branch.transform.position.x + speed * Time.deltaTime,
                branch.transform.position.y,
                branch.transform.position.x > startTransformPositionZ ? branch.transform.position.z - curvePosition.Evaluate(destroingZone.transform.position.x / branch.transform.position.x) * Time.deltaTime : branch.transform.position.z);

                branch.transform.position = newPosition;
            }
            
            yield return null;
        }
    }

    private void Start()
    {
        backgroundManager = GetComponent<BackgroundManager>();
    }

    [Inject]
    private void Init(Score scoreManager, Level levelController)
    {
        this.scoreManager = scoreManager;
        this.levelController = levelController;
    }
}
