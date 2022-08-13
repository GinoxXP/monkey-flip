using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Crocodile : MonoBehaviour
{
    private Animator animator;

    public void Bite()
    {
        animator.speed = 1;
    }

    private void Start()
    {
        animator = GetComponent<Animator>();
        animator.speed = 0;
    }
}
