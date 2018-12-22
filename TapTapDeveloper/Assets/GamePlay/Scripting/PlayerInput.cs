using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class PlayerInput : MonoBehaviour
{
    public string[] secretWords;

    public string[] Cheats;

    public string[] CheatResults;

    public AudioClip[] keyPresses;

    private PlayerManager playerManager;

    private void Update()
    {
        playerManager = FindObjectOfType<PlayerManager>();

        if (GameManager.CurrentScreen == "ContractScreen")
        if (ContractManager.CurrentContract == "" || ContractManager.CurrentContract == " ") FindObjectOfType<ContractBehavior>().CreateContract();
    }

    public void AnyKeyPressed(string KeyPressed)
    {
        if (KeyPressed == " ") GetComponent<AudioSource>().PlayOneShot(keyPresses[1]); else GetComponent<AudioSource>().PlayOneShot(keyPresses[0]);

        if (GameManager.CurrentScreen == "ComputerScreen")
        {
            ContractManager.progressToCompletion += 1;

            PlayerText.AddLetter(KeyPressed);
        }
        else
        {
            PlayerText.AddLetter(KeyPressed);
        }
    }

    public void EnterKeyPressed()
    {
        GetComponent<AudioSource>().PlayOneShot(keyPresses[1]);

        if (GameManager.CurrentScreen == "ContractScreen")
        {
            if (PlayerText.CheckForWords("DECLINE", "N", "FUCK THAT"))
            {
                FindObjectOfType<ContractBehavior>().CreateContract();
            }

            if (PlayerText.CheckForWords("ACCEPT", "Y", "OKAY"))
            {
                playerManager.LoadScene("ComputerScreen");
            }
        }
        else if (GameManager.CurrentScreen == "ComputerScreen")
        {
            foreach (var word in secretWords)
            {
                if (PlayerText.CheckForWord(word))
                {
                    ContractManager.progressToCompletion += GameManager.playerLevel();
                }
            }

            CheckForCheats();
        }
        else if (GameManager.CurrentScreen == "StoreScreen")
        {
            if (PlayerText.CheckForWords("PREVIOUS", "LAST"))
            {
                FindObjectOfType<StoreManager>().PreviousItem();
            }

            if (PlayerText.CheckForWords("NEXT", "NO", "FUCK THAT"))
            {
                FindObjectOfType<StoreManager>().NextItem();
            }

            if (PlayerText.CheckForWords("Y", "OKAY", "BUY"))
            {
                FindObjectOfType<StoreManager>().BuyItem();
            }
        }
        else if (GameManager.CurrentScreen == "LoginScreen")
        {
            if (PlayerText.GetCharacterCount() > 9 && PlayerText.Text != "" && !PlayerText.CheckForWord(" ") && !PlayerText.CheckForWords("SHIT","FUCK", "BITCH", "CUNT") && !PlayerText.CheckForWords("TWAT", "SHIT", "ASS", "NIG") && !PlayerText.CheckForWords("ARSE", "BASTARD", "BOLLOCKS", "DAMN"))
                FindObjectOfType<LoginScreen>().DisplayLoginText();
            else FindObjectOfType<LoginScreen>().DisplayInvalidText();
        }
        PlayerText.ClearScreen();
    }

    public void EscKeyPressed()
    {
        GetComponent<AudioSource>().PlayOneShot(keyPresses[0]);
        if (GameManager.CurrentScreen == "ComputerScreen")
            if (PlayerText.Text == "") FindObjectOfType<PlayerManager>().LoadScene("ContractScreen"); else PlayerText.ClearScreen();
        else if (GameManager.CurrentScreen == "ContractScreen")
            if (PlayerText.Text == "") FindObjectOfType<PlayerManager>().LoadScene("StoreScreen"); else PlayerText.ClearScreen();
        else if (GameManager.CurrentScreen == "StoreScreen")
            if (PlayerText.Text == "") FindObjectOfType<PlayerManager>().LoadScene("CharacterScreen"); else PlayerText.ClearScreen();
        else if (GameManager.CurrentScreen == "CharacterScreen")
            if (PlayerText.Text == "") FindObjectOfType<PlayerManager>().LoadScene("ContractScreen"); else PlayerText.ClearScreen();
        else PlayerText.ClearScreen();

    }

    #region Cheats
    int i;

    void CheckForCheats()
    {
        while (i < Cheats.Length)
        {
            if (PlayerText.CheckForWord(Cheats[i]))
            {
                if (Cheats[i] == "RESET EVERYTHING")
                {
                    PlayerPrefs.DeleteAll();
                    playerManager.LoadScene("SplashScreen");
                    return;
                }

                if (Cheats[i] == "KILL EVERY ONE" || Cheats[i] == "KILL EVERYONE")
                {
                    if (GameManager.gunPurchased())
                        playerManager.LoadScene(CheatResults[i]);
                    return;
                }

                playerManager.LoadScene(CheatResults[i]);
            }

            i++;
        }

        if (i >= Cheats.Length) i = 0;
    }
    #endregion
}
public static class PlayerText
{
    public static string Text;

    private static string ExistingText;

    private static int CharacterCount;

    public static float GetCharacterCount()
    {
        return CharacterCount;
    }

    public static void AddLetter(string letterToAdd)
    {
        if (CharacterCount >= 130)
            ClearScreen();
        else
        {
            ExistingText = ExistingText + letterToAdd;
            CharacterCount++;
        }

        Text = ExistingText;
    }


    #region CheckForWords
    public static bool CheckForWord(string word)
    {
        if (ExistingText.Contains(word)) return true; else return false;
    }

    public static bool CheckForWords(string word1, string word2)
    {
        if (ExistingText.Contains(word1) || ExistingText.Contains(word2)) return true; else return false;
    }

    public static bool CheckForWords(string word1, string word2, string word3)
    {
        if (ExistingText.Contains(word1) || ExistingText.Contains(word2) || ExistingText.Contains(word3)) return true; else return false;
    }

    public static bool CheckForWords(string word1, string word2, string word3, string word4)
    {
        if (ExistingText.Contains(word1) || ExistingText.Contains(word2) || ExistingText.Contains(word3) || ExistingText.Contains(word4)) return true; else return false;
    }
    #endregion

    public static void ClearScreen()
    {
        CharacterCount = 0;
        ExistingText = "";

        Text = ExistingText;
    }
}

public static class PublicText
{
    public static string[] Text = new string[] { "", "", "", "", "", "", "", "" };

    private static string[] ExistingText = new string[] { "", "", "", "", "", "", "", "" };

    private static int[] CharacterCount = new int[] { 0, 0, 0, 0, 0, 0, 0, 0 };

    public static void AddLetter(string letterToAdd, int TextToAddItTo)
    {
        if (CharacterCount[TextToAddItTo] >= 130)
            ClearScreen(TextToAddItTo);
        else
        {
            ExistingText[TextToAddItTo] = ExistingText[TextToAddItTo] + letterToAdd;
            CharacterCount[TextToAddItTo]++;
        }

        Text[TextToAddItTo] = ExistingText[TextToAddItTo];
    }

    #region CheckForWords
    public static bool CheckForWord(string word, int TextToCheck)
    {
        if (ExistingText[TextToCheck].Contains(word)) return true; else return false;
    }

    public static bool CheckForWords(string word1, string word2, int TextToCheck)
    {
        if (ExistingText[TextToCheck].Contains(word1) || ExistingText[TextToCheck].Contains(word2)) return true; else return false;
    }

    public static bool CheckForWords(string word1, string word2, string word3, int TextToCheck)
    {
        if (ExistingText[TextToCheck].Contains(word1) || ExistingText[TextToCheck].Contains(word2) || ExistingText[TextToCheck].Contains(word3)) return true; else return false;
    }

    public static bool CheckForWords(string word1, string word2, string word3, string word4, int TextToCheck)
    {
        if (ExistingText[TextToCheck].Contains(word1) || ExistingText[TextToCheck].Contains(word2) || ExistingText[TextToCheck].Contains(word3) || ExistingText[TextToCheck].Contains(word4)) return true; else return false;
    }
    #endregion

    public static void ClearScreen(int screenToClear)
    {
        CharacterCount[screenToClear] = 0;
        ExistingText[screenToClear] = "";

        Text[screenToClear] = ExistingText[screenToClear];
    }
}
