using UnityEngine;
using Zenject;

public class Branch : MonoBehaviour
{
    private SmoothJump smoothJump;
    private MoveLevel moveLevel;
    private MoveCamera moveCamera;

    private bool isPlayerStanded;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player") && !isPlayerStanded)
        {
            moveLevel.StopMove();
            smoothJump.StopJump();
            isPlayerStanded = true;
            moveCamera.StartReturn();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            moveCamera.StopMove();
        }
    }

    [Inject]
    private void Init(
        SmoothJump smoothJump,
        MoveLevel moveLevel,
        MoveCamera moveCamera)
    {
        this.smoothJump = smoothJump;
        this.moveLevel = moveLevel;
        this.moveCamera = moveCamera;
    }
}
