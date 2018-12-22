using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BreakDukeNukemGame : MonoBehaviour
{
    public void activateAnimation()
    {
        GetComponent<Animator>().SetTrigger("BreakGame");
    }

    public void LoadMainGame(string whatIsMainScene)
    {
        SceneManager.LoadScene(whatIsMainScene);
    }
}
