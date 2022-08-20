using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(BackgroundManager))]
public class MoveLevel : MonoBehaviour
{
    private ScoreManager scoreManager;
    private BackgroundManager backgroundManager;

    [SerializeField]
    private float speed;
    [SerializeField]
    private Transform startSegment;

    private IEnumerator moveCoroutine;
    private float trail;

    public List<Transform> Segments { get; set; } = new List<Transform>();

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

        scoreManager.Score += (int)trail;
    }

    private IEnumerator AnimationByTime()
    {
        while (true)
        {
            foreach(var branch in Segments)
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
        Segments.Add(startSegment);
        backgroundManager = GetComponent<BackgroundManager>();
    }

    [Inject]
    private void Init(ScoreManager scoreManager)
    {
        this.scoreManager = scoreManager;
    }
}
