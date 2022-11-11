using UnityEngine;
using Zenject;

public class CreatingSegmentZone : MonoBehaviour
{
    private Level level;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<SmoothJump>(out _))
            level.CreateSegment();
    }

    [Inject]
    private void Init(Level level)
    {
        this.level = level;
    }
}
