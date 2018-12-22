using UnityEngine;
using UnityEngine.UI;

public class ScoreCounter : MonoBehaviour
{
    public float Score;

    private Text scriptObject;

    private void Start()
    {
        scriptObject = GetComponent<Text>();
    }

    private void Update()
    {
        Score = ScoreManager.Value;

        scriptObject.text = "Data Points: " + Score;
    }
}

public static class ScoreManager
{
    public static float Value;
}
