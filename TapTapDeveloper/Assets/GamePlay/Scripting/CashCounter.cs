using UnityEngine;
using UnityEngine.UI;

public class CashCounter : MonoBehaviour
{
    public float Score;

    private Text scriptObject;

    private void Start()
    {
        Money.startMoney();

        scriptObject = GetComponent<Text>();
    }

    private void Update()
    {
        Score = Money.Value;

        Money.setMoney();

        scriptObject.text = "Money: " + Score;
    }
}

public static class Money
{
    public static void startMoney()
    {
        Value = PlayerPrefs.GetFloat("PLAYERMONEY");
    }

    public static void setMoney()
    {
        PlayerPrefs.SetFloat("PLAYERMONEY", Value);
    }

    public static float Value;
}
