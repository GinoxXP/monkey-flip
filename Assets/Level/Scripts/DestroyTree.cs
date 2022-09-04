using UnityEngine;
using Zenject;

public class DestroyTree : MonoBehaviour
{    
    private MoveLevel moveLevel;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Segment>(out _))
        {
            moveLevel.Segments.Remove(other.transform);
            Destroy(other.gameObject);
        }
    }

    [Inject]
    private void Init(
        MoveLevel moveLevel)
    {
        this.moveLevel = moveLevel;
    }
}
