using UnityEngine;
using Zenject;

public class DestroyingSegmentZone : MonoBehaviour
{    
    private Level level;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Segment>(out _))
            level.DestroySegment(other.transform);
    }

    [Inject]
    private void Init(Level level)
    {
        this.level = level;
    }
}
