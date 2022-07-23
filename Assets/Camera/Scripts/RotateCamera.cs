using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Animator))]
public class RotateCamera : MonoBehaviour
{
    [SerializeField]
    private AnimationCurve speedMultiplicatorCurve;

    private Animator animator;

    public void Death()
    {
        /// TODO 
        /// Remake this

        var currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }

    public void CameraBack()
    {
        animator.SetTrigger("CameraBack");
    }

    private void Start()
    {
        animator = GetComponent<Animator>();
    }
}
