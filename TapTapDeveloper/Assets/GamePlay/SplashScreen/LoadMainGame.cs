using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LoadMainGame : MonoBehaviour
{
    public void StartLoad()
    {
        if (GameManager.HasStarted()) SceneManager.LoadScene("ContractScreen");
        else
        {
            SceneManager.LoadScene("TutorialScreen");
        }
    }
}
