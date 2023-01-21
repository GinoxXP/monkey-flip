using TMPro;
using UnityEngine;

public class LeaderboardEntry : MonoBehaviour
{
    [SerializeField]
    private TMP_Text rankText;
    [SerializeField]
    private TMP_Text nameText;
    [SerializeField]
    private TMP_Text scoreText;

    public string Rank { get => rankText.text; set => rankText.text = value; }

    public string Name { get => nameText.text; set => nameText.text = value; }

    public string Score { get => scoreText.text; set => scoreText.text = value; }
}
