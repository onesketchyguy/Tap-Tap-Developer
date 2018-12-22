using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreManager : MonoBehaviour
{
    [SerializeField] private string[] Item;
    [SerializeField] private string[] ItemDescription;
    [SerializeField] private string[] ItemReward;
    [SerializeField] private float[] ItemCost;

    int SelectedItem;

    private void Start()
    {
        DisplayItem();
    }

    public void DisplayItem()
    {
        if (PublicText.Text[0] != Item[SelectedItem] + " " + ItemDescription[SelectedItem] + " " + (ItemCost[SelectedItem] > 0 ? ItemCost[SelectedItem].ToString() : "FREE"))
        {
            PublicText.Text[0] = Item[SelectedItem] + " " + ItemDescription[SelectedItem] + " " + (ItemCost[SelectedItem] > 0 ? ItemCost[SelectedItem].ToString() : "FREE");
        }
    }

    public void DisplayBoughtText()
    {
        StartCoroutine(displayBoughtText());
    }

    IEnumerator displayBoughtText()
    {
        if (PublicText.Text[0] != Item[SelectedItem] + " BOUGHT FOR " + (ItemCost[SelectedItem] > 0 ? ItemCost[SelectedItem].ToString() : "FREE"))
        {
            PublicText.Text[0] = Item[SelectedItem] + " BOUGHT FOR " + (ItemCost[SelectedItem] > 0 ? ItemCost[SelectedItem].ToString() : "FREE");
        }
        yield return new WaitForSeconds(2);
        if (PublicText.Text[0] == Item[SelectedItem] + " BOUGHT FOR " + (ItemCost[SelectedItem] > 0 ? ItemCost[SelectedItem].ToString() : "FREE"))
            DisplayItem();
    }

    IEnumerator displayCantAffordText()
    {
        if (PublicText.Text[0] != Item[SelectedItem] + " Costs " + ItemCost[SelectedItem] + " You have " + ((Money.Value > 0)? Money.Value.ToString() : "NOTHING"))
        {
            PublicText.Text[0] = Item[SelectedItem] + " Costs " + ItemCost[SelectedItem] + " You have " + ((Money.Value > 0) ? Money.Value.ToString() : "NOTHING");
        }
        yield return new WaitForSeconds(2);
        if (PublicText.Text[0] == Item[SelectedItem] + " Costs " + ItemCost[SelectedItem] + " You have " + ((Money.Value > 0) ? Money.Value.ToString() : "NOTHING"))
            DisplayItem();
    }

    public void DisplayCantAffordText()
    {
        StartCoroutine(displayCantAffordText());
    }

    public void BuyItem()
    {
        if (Money.Value >= ItemCost[SelectedItem])
        {
            if (PlayerManagerHandler.GetBuildings() > 0 && ItemReward[SelectedItem].Contains("worker"))
            {
                if (PlayerManagerHandler.GetWorkers() + 1 <= PlayerManagerHandler.GetMaxWorkers())
                {
                    if (ItemReward[SelectedItem] == "1worker") PlayerManagerHandler.AddWorker();
                    Bought();
                }
                    
                if (PlayerManagerHandler.GetWorkers() + 2 <= PlayerManagerHandler.GetMaxWorkers())
                {
                    if (ItemReward[SelectedItem] == "1worker") PlayerManagerHandler.AddWorker(2);
                    Bought();
                }

                if (PlayerManagerHandler.GetWorkers() > 0)
                {
                    if (ItemReward[SelectedItem] == "levelUpWorkers") PlayerManagerHandler.AddSkill();

                    if (ItemReward[SelectedItem] == "levelUpWorkers2") PlayerManagerHandler.AddSkill(0.2f);

                    Bought();
                }
            }

            if (ItemReward[SelectedItem] == "building")
            {
                PlayerManagerHandler.AddBuilding();
                Bought();
            }

            if (ItemReward[SelectedItem] == "buildings5")
            {
                PlayerManagerHandler.AddBuilding(5);
                Bought();
            }

            if (ItemReward[SelectedItem] == "gun" && !GameManager.gunPurchased())
            {
                Bought();
                GameManager.GunPurchased();
            }

            if (ItemReward[SelectedItem] == "nothing")
            {
                Bought();
            }
        } else
        {
            DisplayCantAffordText();
        }
    }

    void Bought()
    {
        DisplayBoughtText();

        Money.Value -= ItemCost[SelectedItem];
    }

    public void NextItem()
    {
        if (Item.Length - 1 > SelectedItem) SelectedItem += 1; else SelectedItem = 0;

        DisplayItem();
    }

    public void PreviousItem()
    {
        if (SelectedItem > 0) SelectedItem -= 1; else SelectedItem = Item.Length - 1;

        DisplayItem();
    }
}
