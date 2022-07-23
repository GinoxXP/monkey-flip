using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLevel : MonoBehaviour
{
    [SerializeField]
    private float speed;

    private IEnumerator moveCoroutine;

    public List<Transform> Branches { get; set; } = new List<Transform>();

    public void Move(float power)
    {
        moveCoroutine = AnimationByTime(power);
        StartCoroutine(moveCoroutine);
    }

    public void StopMove()
    {
        if (moveCoroutine == null)
            return;

        StopCoroutine(moveCoroutine);
    }

    private IEnumerator AnimationByTime(float power)
    {
        while (true)
        {
            foreach(var branch in Branches)
            {
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
        Branches.Add(transform.GetChild(0));
    }
}
