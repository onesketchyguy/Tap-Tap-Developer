using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour
{
    private void Update()
    {
        GameManager.CurrentScreen = (SceneManager.GetActiveScene().name);

        if (PlayerManagerHandler.GetWorkers() > 0)
        {
            if (aiJobProgress >= aiJobRequirement)
            {
                Money.Value += aiPayForJob;

                CreateAIJob();
            }
            else aiJobProgress += PlayerManagerHandler.GetWorkers() * PlayerManagerHandler.GetWorkerSkills();
        }
    }

    float aiJobProgress;

    float aiJobRequirement;

    float aiPayForJob;

    void CreateAIJob()
    {
        aiJobProgress = 0;
        aiJobRequirement = Random.Range(PlayerManagerHandler.GetWorkers() * 10, (PlayerManagerHandler.GetWorkers() * 100) * (PlayerManagerHandler.GetWorkerSkills() * 10));
        aiPayForJob = Random.Range(PlayerManagerHandler.GetWorkers() * 10, (PlayerManagerHandler.GetWorkers() * 100) * (PlayerManagerHandler.GetWorkerSkills() * 10));
    }

    public void LoadScene(string levelToLoad)
    {
        StartCoroutine(beginLoad(levelToLoad));
    }

    IEnumerator beginLoad(string levelToLoad)
    {
        yield return new WaitForSeconds(0.1f);

        SceneManager.LoadScene(levelToLoad);
    }
}
public static class GameManager
{
    public static string CurrentScreen = (SceneManager.GetActiveScene().name);

    #region PlayerLevel
    public static int playerLevel()
    {
        return PlayerPrefs.GetInt(C_LEVEL);
    }

    public static float Experience()
    {
        return PlayerPrefs.GetFloat(C_EXPERIENCE);
    }

    public static void AddExperience(float ExperienceToAdd)
    {
        PlayerPrefs.SetFloat(C_EXPERIENCE, Experience() + ExperienceToAdd);
    }

    const string C_EXPERIENCE = "PlayerExperience";

    const string C_LEVEL = "PlayerLEVEL";

    public static void CheckForLevelUp()
    {
        float reqExp = playerLevel() * 100;

        int reward = playerLevel() + 1;

        if (Experience() >= reqExp)
        {
            PlayerPrefs.SetInt(C_LEVEL, reward);

            PlayerPrefs.SetInt(C_EXPERIENCE, 0);
        }
    }
    #endregion

    #region Gun
    public static bool gunPurchased()
    {
        return PlayerPrefs.GetInt("GunPurchased") > 0 ? true : false;
    }

    public static void GunPurchased()
    {
        PlayerPrefs.SetInt("GunPurchased", 1);
    }
    #endregion

    #region First Launch
    const string C_HASSTARTED = "THEPLAYERHASALREADYLAUNCHEDTHEGAMEBEFORE";

    public static void StartLevels()
    {
        if (playerLevel() < 1)
        PlayerPrefs.SetInt(C_LEVEL, 1);
    }

    public static bool HasStarted()
    {
        return PlayerPrefs.GetInt(C_HASSTARTED) > 0 ? true : false;
    }

    public static void GameHasStarted()
    {
        PlayerPrefs.SetInt(C_HASSTARTED, 1);
    }
    #endregion

}

public static class PlayerManagerHandler
{
    const string C_WORKER = "PlayersEmployees";

    const string C_BUILDINGS = "PlayerBuildingCount";

    const string C_SKILLS = "PlayersEmployeesSKILLLEVELS";

    static int buildings()
    {
        return PlayerPrefs.GetInt(C_BUILDINGS);
    }

    static int workers()
    {
        return PlayerPrefs.GetInt(C_WORKER);
    }

    static float skills()
    {
        if (PlayerPrefs.GetFloat(C_SKILLS) > 0.0)
            return PlayerPrefs.GetFloat(C_SKILLS);
        else
        {
            PlayerPrefs.SetFloat(C_SKILLS, 0.1f);
            return PlayerPrefs.GetFloat(C_SKILLS);
        }
    }

    public static int GetBuildings()
    {
        return buildings();
    }

    public static float GetWorkerSkills()
    {
        return skills();
    }

    public static int GetWorkers()
    {
        return workers();
    }

    public static int GetMaxWorkers()
    {
        return buildings() * 10;
    }

    public static void AddBuilding()
    {
        float c_building = buildings();

        PlayerPrefs.SetFloat(C_BUILDINGS, c_building + 1f);
    }

    public static void AddBuilding(int buildingsToAdd)
    {
        float c_building = buildings();

        PlayerPrefs.SetFloat(C_BUILDINGS, c_building + buildingsToAdd);
    }

    public static void AddSkill()
    {
        float c_skills = skills();

        PlayerPrefs.SetFloat(C_SKILLS, c_skills + 0.1f);
    }

    public static void AddSkill(float skillAddition)
    {
        float c_skills = skills();

        PlayerPrefs.SetFloat(C_SKILLS, c_skills + skillAddition);
    }

    public static void AddWorker()
    {
        int c_workers = workers();

        PlayerPrefs.SetInt(C_WORKER, c_workers + 1);
    }

    public static void AddWorker(int addition)
    {
        int c_workers = workers();

        PlayerPrefs.SetInt(C_WORKER, c_workers + addition);
    }


    public static void ResetAll()
    {
        PlayerPrefs.SetFloat(C_SKILLS, 0);
        PlayerPrefs.SetInt(C_WORKER, 0);
    }
}
