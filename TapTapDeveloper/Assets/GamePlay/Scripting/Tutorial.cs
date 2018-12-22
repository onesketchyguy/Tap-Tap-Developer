using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    [SerializeField] private string[] MainText; int progressThroughMainText = 0;
    [SerializeField] private string[] MoneyText; int progressThroughMoneyText = 0;
    [SerializeField] private string[] ContractText; int progressThroughContractText = 0;

    private void Start()
    {
        StartCoroutine(ShowTutorial());
    }

    IEnumerator ShowTutorial()
    {
        progressThroughMainText = 0;
        progressThroughMoneyText = 0;
        progressThroughContractText = 0;

        DisplayMainText();

        yield return new WaitForSeconds(2);

        progressThroughMainText = 1;

        DisplayMainText();

        yield return new WaitForSeconds(3);

        progressThroughMainText = 2;

        progressThroughMoneyText = 1;

        DisplayMainText();

        yield return new WaitForSeconds(5);

        progressThroughMainText = 3;

        progressThroughMoneyText = 0;

        progressThroughContractText = 1;

        DisplayMainText();

        yield return new WaitForSeconds(5);

        progressThroughMainText = 4;

        progressThroughMoneyText = 0;

        progressThroughContractText = 0;

        DisplayMainText();

        yield return new WaitForSeconds(5);

        progressThroughMainText = 5;

        DisplayMainText();

        yield return new WaitForSeconds(4);

        progressThroughMainText = 6;

        DisplayMainText();

        yield return new WaitForSeconds(4);

        progressThroughMainText = 7;

        DisplayMainText();

        yield return new WaitForSeconds(4);

        progressThroughMainText = 8;

        DisplayMainText();

        yield return new WaitForSeconds(4);

        progressThroughMainText = 9;

        DisplayMainText();

        yield return new WaitForSeconds(3);

        FindObjectOfType<PlayerManager>().LoadScene("LoginScreen");
    }

    public void DisplayMainText()
    {
        PublicText.Text[0] = MainText[progressThroughMainText];
        PublicText.Text[1] = MoneyText[progressThroughMoneyText];
        PublicText.Text[2] = ContractText[progressThroughContractText];
    }
}
