using UnityEngine;

public class DeathZone : MonoBehaviour
{
    [SerializeField]
    private Crocodile crocodile;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            crocodile.Bite();
        }
    }
}
