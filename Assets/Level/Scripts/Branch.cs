using UnityEngine;
using Zenject;

public class Branch : MonoBehaviour
{
    private GenerationLevel generationLevel;
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
            generationLevel?.Generate();
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
    private void Init(GenerationLevel generationLevel, PlayerController playerController, SmoothJump smoothJump, MoveLevel moveLevel)
    {
        this.generationLevel = generationLevel;
        this.playerController = playerController;
        this.smoothJump = smoothJump;
        this.moveLevel = moveLevel;
    }
}
