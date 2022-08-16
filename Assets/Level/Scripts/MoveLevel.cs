using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class MoveLevel : MonoBehaviour
{
    private ScoreManager scoreManager;

    [SerializeField]
    private float speed;
    [SerializeField]
    private Transform startSegment;

    private IEnumerator moveCoroutine;
    private float trail;

    public List<Transform> Segments { get; set; } = new List<Transform>();

    public void Move(float power)
    {
        trail = 0;
        moveCoroutine = AnimationByTime(power);
        StartCoroutine(moveCoroutine);
    }

    public void StopMove(bool ignoreScore = false)
    {
        if (moveCoroutine == null)
            return;

        StopCoroutine(moveCoroutine);

        if (ignoreScore)
            return;

        scoreManager.Score += (int)trail;
    }

    private IEnumerator AnimationByTime(float power)
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
    }

    [Inject]
    private void Init(ScoreManager scoreManager)
    {
        this.scoreManager = scoreManager;
    }
}
