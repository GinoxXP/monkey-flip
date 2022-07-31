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
    private Transform startBranch;

    private IEnumerator moveCoroutine;
    private float trail;

    public List<Transform> Branches { get; set; } = new List<Transform>();

    public void Move(float power)
    {
        trail = 0;
        moveCoroutine = AnimationByTime(power);
        StartCoroutine(moveCoroutine);
    }

    public void StopMove()
    {
        if (moveCoroutine == null)
            return;

        StopCoroutine(moveCoroutine);
        scoreManager.Score += (int)trail;
    }

    private IEnumerator AnimationByTime(float power)
    {
        while (true)
        {
            foreach(var branch in Branches)
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
        Branches.Add(startBranch);
    }

    [Inject]
    private void Init(ScoreManager scoreManager)
    {
        this.scoreManager = scoreManager;
    }
}
