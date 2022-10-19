using UnityEngine;
using Zenject;

public class BranchDetector : MonoBehaviour
{
    private PlayerController playerController;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Branch>(out _))
        {
            playerController.IsCanJump = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<Branch>(out _))
        {
            playerController.IsCanJump = false;
        }
    }

    [Inject]
    private void Init(PlayerController playerController)
    {
        this.playerController = playerController;
    }
}
