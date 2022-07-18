using System.Collections;
using UnityEngine;
using Zenject;

public class MoveLevel : MonoBehaviour
{
    private PlayerController playerController;

    [SerializeField]
    private float speed;
    [SerializeField]
    private Transform level;

    public void Move(float power)
    {
        StartCoroutine(Moving(power));
    }

    private IEnumerator Moving(float power)
    {
        var time = 0f;
        while (time < 1)
        {
            var newPosition = new Vector3(
                level.transform.position.x + speed * Time.deltaTime,
                level.transform.position.y,
                level.transform.position.z);

            level.transform.position = newPosition;
            time += Time.deltaTime / (power / playerController.MaxPower);
            yield return null;
        }
        yield return null;
    }

    [Inject]
    private void Init(PlayerController playerController)
    {
        this.playerController = playerController;
    }
}
