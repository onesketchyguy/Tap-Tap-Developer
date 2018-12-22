using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ContractBehavior : MonoBehaviour
{
    Text mytext;

    public Slider slider;

    public string[] Company;
    public string[] Contract;

    private void Start()
    {
        mytext = GetComponent<Text>();

        mytext.text = "";
    }

    private void Update()
    {
        if (ContractManager.CurrentContract == "" || ContractManager.ContractComplete)
        {
            ContractManager.progressToCompletion = 0;

            slider.value = 0;
            slider.maxValue = 0;
        } else
        {
            slider.maxValue = ContractManager.completionRequirment;
            slider.value = ContractManager.progressToCompletion;
        }

        ContractManager.CheckForContractComplete();

        mytext.text = (ContractManager.CurrentContract != "")? ContractManager.CurrentContract : "No contract active.";
    }

    public void CreateContract()
    {
        ContractManager.ContractComplete = false;

        ContractManager.contractPay = Random.Range(10, 100 * GameManager.playerLevel());

        ContractManager.completionRequirment = Random.Range(1, 100 * GameManager.playerLevel());

        ContractManager.CurrentContract = DecideContract(ContractManager.completionRequirment, ContractManager.contractPay);
    }

    string DecideContract(float difficulty, float payment)
    {
        string stringA = Company[Random.Range(0, Company.Length)];
        string stringB = Contract[Random.Range(0, Contract.Length)];

        if (difficulty < 20) stringB = "Easy " + stringB;
        else if (difficulty > 100)
        {
            if (difficulty > 250)
            {
                if (difficulty > 500)
                {
                    if (difficulty > 670)
                    {
                        stringB = "Fricking impossible " + stringB;
                    }
                    else stringB = "Extremely Difficult " + stringB;
                }
                else stringB = "Very Difficult " + stringB;
            }
            else stringB = "Difficult " + stringB;
        }


        if (payment < 20) stringB = "Low paying " + stringB;
        else if (payment > 100)
        {
            if (payment > 250)
            {
                if (payment > 500)
                {
                    if (payment > 1000)
                    {
                        stringB = "Great paying " + stringB;
                    }
                    else stringB = "Very well paying " + stringB;
                }
                else stringB = "Well paying " + stringB;
            }
            else stringB = "Okay paying " + stringB;
        }

        return (stringA + " " + stringB);
    }
}


public static class ContractManager
{
    public static float progressToCompletion;

    public static float completionRequirment;

    public static bool ContractComplete;

    public static float contractPay;

    public static void CheckForContractComplete()
    {
        if (progressToCompletion >= completionRequirment)
        {
            ContractComplete = true;

            CurrentContract = "";

            GameManager.AddExperience(progressToCompletion);

            progressToCompletion = 0;

            Money.Value += contractPay;
        }
    }

    public static string CurrentContract;
}
