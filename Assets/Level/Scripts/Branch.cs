using UnityEngine;
using Zenject;

public class Branch : MonoBehaviour
{
    private PlayerController playerController;
    private SmoothJump smoothJump;
    private MoveLevel moveLevel;

    private bool isPlayerStanded;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player" && !isPlayerStanded)
        {
            moveLevel.StopMove();
            smoothJump.StopJump();
            isPlayerStanded = true;
            playerController.IsCanJump = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            playerController.IsCanJump = false;
        }
    }

    [Inject]
    private void Init(
        PlayerController playerController,
        SmoothJump smoothJump,
        MoveLevel moveLevel)
    {
        this.playerController = playerController;
        this.smoothJump = smoothJump;
        this.moveLevel = moveLevel;
    }
}
