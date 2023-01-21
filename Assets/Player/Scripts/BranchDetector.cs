using System;
using UnityEngine;
using Zenject;

public class BranchDetector : MonoBehaviour
{
    private PlayerController playerController;
    private Level level;

    public event Action LandingOnBranch;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Branch>(out var branch))
        {
            playerController.IsCanJump = true;
            level.CurrentSegment = branch.ParentSegment;
            LandingOnBranch?.Invoke();
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
    private void Init(PlayerController playerController, Level level)
    {
        this.playerController = playerController;
        this.level = level;
    }
}
