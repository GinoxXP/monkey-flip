using UnityEngine;

public class Monkey : MonoBehaviour
{
    [SerializeField]
    private AudioSource hop;
    [SerializeField]
    private AudioSource cry;
    [SerializeField]
    private AudioSource yey;

    public void Hop()
        => hop.Play();

    public void Cry()
        => cry.Play();

    public void Yey()
        => yey.Play();
}
