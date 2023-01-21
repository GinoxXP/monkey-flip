using UnityEngine;
using Zenject;

[RequireComponent(typeof(Animator))]
public class Crocodile : MonoBehaviour
{
    private Monkey monkey;
    private Animator animator;

    public void Bite()
    {
        monkey.Cry();
        animator.speed = 1;
    }

    private void Start()
    {
        animator = GetComponent<Animator>();
        animator.speed = 0;
    }

    [Inject]
    private void Init(Monkey monkey)
    {
        this.monkey = monkey;
    }
}
