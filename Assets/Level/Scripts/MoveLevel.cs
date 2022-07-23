using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class MoveLevel : MonoBehaviour
{
    private PlayerController playerController;

    [SerializeField]
    private float speed;

    public List<Transform> Branches { get; set; } = new List<Transform>();

    public void Move(float power)
    {
        StartCoroutine(Moving(power));
    }

    private IEnumerator Moving(float power)
    {
        var time = 0f;
        while (time < 1)
        {
            foreach(var branch in Branches)
            {
                var newPosition = new Vector3(
                branch.transform.position.x + speed * Time.deltaTime,
                branch.transform.position.y,
                branch.transform.position.z);

                branch.transform.position = newPosition;
            }
            
            time += Time.deltaTime / (power / playerController.MaxPower);
            yield return null;
        }
        yield return null;
    }

    private void Start()
    {
        Branches.Add(transform.GetChild(0));
    }

    [Inject]
    private void Init(PlayerController playerController)
    {
        this.playerController = playerController;
    }
}
