using UnityEngine;

public class SkinPreviewCameraRotate : MonoBehaviour
{
    [SerializeField]
    private Transform origin;
    [Space]
    [SerializeField]
    private float xMultiplier;
    [SerializeField]
    private float yMultiplier;
    [SerializeField]
    private float zMultiplier;
    [Space]
    [SerializeField]
    private float xTimeMultiplier;
    [SerializeField]
    private float yTimeMultiplier;
    [SerializeField]
    private float zTimeMultiplier;

    private float time = 0;

    // Update is called once per frame
    void Update()
    {
        float xPos = Mathf.Sin(time * xTimeMultiplier) * xMultiplier;
        float yPos = Mathf.Sin(time * yTimeMultiplier) * yMultiplier;
        float zPos = Mathf.Sin(time * zTimeMultiplier + Mathf.PI/2) * zMultiplier;
        transform.position = new Vector3(xPos, yPos, zPos);

        transform.LookAt(origin);
        time += Time.deltaTime;
    }
}
