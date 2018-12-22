using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterScreenScript : MonoBehaviour
{
    private string CharacterName;
    private string CharacterLevel;
    private string EmployeeCount;
    private string Buildings;

    private void Start()
    {
        GetStats();
        DisplayMainText();
    }

    public void GetStats()
    {
        CharacterName = "Name: administrator";
        CharacterLevel = "Skill: " + GameManager.playerLevel();
        EmployeeCount = "Employee count: " + PlayerManagerHandler.GetWorkers() + "/" + (PlayerManagerHandler.GetBuildings() * 10);
        Buildings = "Owned buildings: " + PlayerManagerHandler.GetBuildings();
    }

    public void DisplayMainText()
    {
        PublicText.Text[0] = CharacterName;
        PublicText.Text[1] = CharacterLevel;
        PublicText.Text[2] = EmployeeCount;
        PublicText.Text[3] = Buildings;
    }
}
