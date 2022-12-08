using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

public class ReloadScene : MonoBehaviour
{
    private Yandex yandex;

    public void Reload()
    {
        yandex.ShowFullscreenAdv();

        var currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }

    [Inject]
    private void Init(Yandex yandex)
    {
        this.yandex = yandex;
    }
}