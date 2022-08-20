using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundManager : MonoBehaviour
{
    [SerializeField]
    private List<BackgroundSection> bgSections;

    private IEnumerator moveCoroutine;

    public void Move()
    {
        moveCoroutine = AnimationByTime();
        StartCoroutine(moveCoroutine);
    }

    public void StopMove()
    {
        if (moveCoroutine == null)
            return;

        StopCoroutine(moveCoroutine);
    }

    private IEnumerator AnimationByTime()
    {
        while (true)
        {
            foreach(var bgSection in bgSections)
            {
                foreach(var bg in bgSection.background.Transforms)
                {
                    var newPosition = new Vector3(
                    bg.transform.position.x + bgSection.speed * Time.deltaTime,
                    bg.transform.position.y,
                    bg.transform.position.z);

                    if(newPosition.x >= bgSection.background.MaxPosition.x)
                    {
                        newPosition -= bgSection.background.MaxPosition;
                    }

                    bg.transform.position = newPosition;
                }
            }
            yield return null;
        }
    }

    [System.Serializable]
    public struct BackgroundSection
    {
        public Background background;
        public float speed;
    }
}
