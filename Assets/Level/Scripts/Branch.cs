using UnityEngine;
using Zenject;

public class Branch : MonoBehaviour
{
    private SmoothJump smoothJump;
    private MoveLevel moveLevel;
    private DiContainer container;

    [SerializeField]
    private Transform parentSegment;
    [SerializeField]
    private Transform lootParent;

    private bool isPlayerStanded;

    public Transform ParentSegment => parentSegment;

    public void SetHeight(float height)
    {
        transform.position = new Vector3(transform.position.x, height, transform.position.z);
    }

    public void SetLoot(GameObject loot)
    {
        if (loot == null)
            return;

        container.InstantiatePrefab(loot, lootParent);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player") && !isPlayerStanded)
        {
            moveLevel.StopMove();
            smoothJump.StopJump();
            isPlayerStanded = true;
        }
    }

    [Inject]
    private void Init(
        SmoothJump smoothJump,
        MoveLevel moveLevel,
        DiContainer container)
    {
        this.smoothJump = smoothJump;
        this.moveLevel = moveLevel;
        this.container = container;
    }
}
