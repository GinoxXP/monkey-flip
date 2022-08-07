using UnityEngine;
using Zenject;

public class Branch : MonoBehaviour
{
    private PlayerController playerController;
    private SmoothJump smoothJump;
    private MoveLevel moveLevel;
    private MoveCamera moveCamera;

    private bool isPlayerStanded;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player" && !isPlayerStanded)
        {
            moveLevel.StopMove();
            smoothJump.StopJump();
            isPlayerStanded = true;
            playerController.IsCanJump = true;
            moveCamera.StartReturn();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            playerController.IsCanJump = false;
            moveCamera.StopMove();
        }
    }

    [Inject]
    private void Init(
        PlayerController playerController,
        SmoothJump smoothJump,
        MoveLevel moveLevel,
        MoveCamera moveCamera)
    {
        this.playerController = playerController;
        this.smoothJump = smoothJump;
        this.moveLevel = moveLevel;
        this.moveCamera = moveCamera;
    }
}
