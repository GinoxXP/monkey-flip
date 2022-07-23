using UnityEngine;
using Zenject;

public class Branch : MonoBehaviour
{
    private GenerationLevel generationLevel;
    private PlayerController playerController;
    private SmoothJump smoothJump;
    private MoveLevel moveLevel;
    private RotateCamera rotateCamera;

    private bool isPlayerStanded;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player" && !isPlayerStanded)
        {
            moveLevel.StopMove();
            smoothJump.StopJump();
            isPlayerStanded = true;
            generationLevel?.Generate();
            playerController.IsCanJump = true;
            rotateCamera.CameraBack();
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
        GenerationLevel generationLevel,
        PlayerController playerController,
        SmoothJump smoothJump,
        MoveLevel moveLevel,
        RotateCamera rotateCamera)
    {
        this.generationLevel = generationLevel;
        this.playerController = playerController;
        this.smoothJump = smoothJump;
        this.moveLevel = moveLevel;
        this.rotateCamera = rotateCamera;
    }
}
