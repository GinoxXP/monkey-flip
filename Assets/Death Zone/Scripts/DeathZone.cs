using UnityEngine;
using Zenject;

public class DeathZone : MonoBehaviour
{
    private MoveLevel moveLevel;

    [SerializeField]
    private Crocodile crocodile;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            crocodile.Bite();
            moveLevel.StopMove(true);
        }
    }

    [Inject]
    private void Init(MoveLevel moveLevel)
    {
        this.moveLevel = moveLevel;
    }
}
