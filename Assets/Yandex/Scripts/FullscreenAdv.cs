using System;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(Yandex))]
public class FullscreenAdv : MonoBehaviour
{
    private ReloadScene reloadScene;
    private Yandex yandex;

    private void ShowAdv()
    {
        try
        {
            yandex.ShowFullscreenAdv();
        }
        catch (Exception e)
        {
            Debug.LogError(e);
        }
    }

    private void Start()
    {
        yandex = GetComponent<Yandex>();
        reloadScene.SceneReloaded += ShowAdv;
    }


    private void OnDestroy()
    {
        reloadScene.SceneReloaded -= ShowAdv;
    }

    [Inject]
    private void Init(ReloadScene reloadScene)
    {
        this.reloadScene = reloadScene;
    }
}
