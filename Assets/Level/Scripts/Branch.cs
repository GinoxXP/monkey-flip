using UnityEngine;

public class Branch : MonoBehaviour
{
    public GenerationLevel GenerationLevel { get; set; }

    private bool isPlayerStanded;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player" && !isPlayerStanded)
        {
            isPlayerStanded = true;
            GenerationLevel?.Generate();
        }
    }
}
