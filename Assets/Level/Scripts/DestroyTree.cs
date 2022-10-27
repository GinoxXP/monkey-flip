using UnityEngine;
using Zenject;

public class DestroyTree : MonoBehaviour
{    
    private Level levelController;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Segment>(out _))
        {
            levelController.Segments.Remove(other.transform);
            Destroy(other.gameObject);
        }
    }

    [Inject]
    private void Init(Level levelController)
    {
        this.levelController = levelController;
    }
}
