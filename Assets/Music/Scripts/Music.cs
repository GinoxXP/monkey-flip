using UnityEngine;

public class Music : MonoBehaviour
{
    private void Awake()
    {
        var musics = FindObjectsOfType<Music>();
        if (musics.Length != 1)
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
    }
}
