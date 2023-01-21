using System.Collections;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(Animator))]
public class Snake : MonoBehaviour
{
    private const string BITE_ANIMATION = "Bite";
    private const string TURN_BACK_ANIMATION = "TurnBack";
    private const float PAUSE_BEFORE_SCENE_RELOAD = 0.2f;

    private Monkey monkey;
    private Animator animator;
    private DifficultyManager difficultyManager;
    private PlayerController playerController;
    private BranchDetector branchDetector;
    private PauseController pauseController;
    private ReloadScene reloadScene;


    [SerializeField]
    private AnimationCurve speedByDifficultyCurve;

    private bool isPause;
    private bool isBiteFinished;
    private bool isReadyToBite;

    public void StartBite()
    {
        if (isPause || !isReadyToBite)
            return;

        isReadyToBite = false;
        animator.speed = speedByDifficultyCurve.Evaluate(difficultyManager.Difficulty);
        animator.Play(BITE_ANIMATION);
    }

    public void CancelBite()
    {
        if (isBiteFinished)
            return;

        animator.speed = 1;
        animator.SetTrigger(TURN_BACK_ANIMATION);
    }

    public void FinishBite()
    {
        isBiteFinished = true;

        var coroutine = FinishBiteCoroutine();
        StartCoroutine(coroutine);
    }

    public void FinishTurnBack()
    {
        isReadyToBite = true;
    }

    private IEnumerator FinishBiteCoroutine()
    {
        monkey.Cry();
        playerController.IsCanJump = false;
        yield return new WaitForSeconds(PAUSE_BEFORE_SCENE_RELOAD);
        reloadScene.Reload();
    }

    private void OnPauseChanged(bool isPause)
        => this.isPause = isPause;

    private void Start()
    {
        animator = GetComponent<Animator>();
        animator.speed = 0;
    }

    private void OnDestroy()
    {
        playerController.StartClick -= CancelBite;
        branchDetector.LandingOnBranch -= StartBite;
    }

    [Inject]
    private void Init(
        Monkey monkey,
        DifficultyManager difficultyManager,
        PlayerController playerController,
        BranchDetector branchDetector,
        PauseController pauseController,
        ReloadScene reloadScene)
    {
        this.monkey = monkey;
        this.difficultyManager = difficultyManager;
        this.playerController = playerController;
        this.branchDetector = branchDetector;
        this.pauseController = pauseController;
        this.reloadScene = reloadScene;

        playerController.StartClick += CancelBite;
        branchDetector.LandingOnBranch += StartBite;
        pauseController.PauseChanged += OnPauseChanged;
    }
}
