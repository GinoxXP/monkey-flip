using System.Collections;
using System.Collections.Generic;
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
                branch.transform.position.z);

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
