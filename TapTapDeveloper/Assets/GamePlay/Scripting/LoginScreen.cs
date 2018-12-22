using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoginScreen : MonoBehaviour
{
    [SerializeField] private string[] LoginTexts;

    [SerializeField] private string[] FailedLoginText;

    private void Start()
    {
        GameManager.StartLevels();
        DisplayLoginText();
    }
    int attempt;
    public void DisplayLoginText()
    {
        PublicText.Text[0] = LoginTexts[attempt];

        if (LoginTexts.Length - 1 > attempt)
            attempt++;
        else
        {
            LoadLevel();
        }
    }
    int inApropriateAttempt;
    public void DisplayInvalidText()
    {
        PublicText.Text[0] = FailedLoginText[inApropriateAttempt];
        if (FailedLoginText.Length - 1 > inApropriateAttempt)
            inApropriateAttempt++;
        else
        {
            LoadLevel();
        }
    }

    void LoadLevel()
    {
        GameManager.GameHasStarted();

        FindObjectOfType<PlayerManager>().LoadScene("ContractScreen");
    }
}
