using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using Opsive.Shared.Events;
using Opsive.UltimateCharacterController.Traits;

public class WorldMissionController : MonoBehaviour
{
    public float timeLeft;
    public float initialTimeLeft;

    public int totalPoints;
    private int totalPointsDeath;

    private int killPoints = 50;
    private int headShot = 100;
    private int remainingHPPoints = 50;
    private int deathPenalty = -1000;
    private int zeroPenalty = 0;
    private int pointsPerSecond = 100;
    public bool isDeath = false;

    public TextMeshProUGUI timePointsText;
    public TextMeshProUGUI killPointsText;
    public TextMeshProUGUI totalPointsText;

    public TextMeshProUGUI timePointsLostText;
    public TextMeshProUGUI killPointsLostText;
    public TextMeshProUGUI totalPointsLostText;

    public GameObject gameOverUI;

    public CharacterRespawner characterRespawner;

    [SerializeField] protected GameObject m_Character;

    void Start()
    {
        initialTimeLeft = timeLeft;
        gameOverUI.gameObject.SetActive(false);
    }

    void Update()
    {
        timeLeft -= Time.deltaTime;

        if (timeLeft <= 0)
        {
            // Uncomment the line below if you want to stop the time when it reaches 0
            // timeLeft = 0;
            MissionFailed();
        }

        UpdateUI();
    }

    void UpdateUI()
    {
        timePointsText.text = CalculateTimePoints().ToString();
        killPointsText.text = CalculateKillPoints().ToString();
        totalPointsText.text = totalPoints.ToString();
        
        timePointsLostText.text = CalculateTimePoints().ToString();
        killPointsLostText.text = CalculateKillPoints().ToString();
        totalPointsLostText.text = totalPointsDeath.ToString();
        
    }

    public void IncreaseTimeLeft(float time)
    {
        timeLeft += time;
    }

    public void setTimeLeft(float time)
    {
        timeLeft = time;
    }

    public void MissionFailed()
    {
        Time.timeScale = 0;
    }

    public void MissionFinished()
    {
        timeLeft = Mathf.Max(timeLeft, 0); // Ensure timeLeft is not negative
        totalPoints = CalculatePoints();
        Time.timeScale = 0;
    }

    public int CalculatePoints()
    {
        int points = 0;

        if (headShot > 0)
        {
            points += headShot;
        }

        if (killPoints > 0)
        {
            points += killPoints;
        }

        if (remainingHPPoints > 0)
        {
            points += remainingHPPoints;
        }

        if (timeLeft <= 0)
        {
            points += deathPenalty;
        }
        else
        {
            points += (int)(pointsPerSecond * timeLeft);
        }

        return points;
    }

    public int CalculateTimePoints()
    {
        return (int)(pointsPerSecond * timeLeft);
    }

    public int CalculateKillPoints()
    {
        return killPoints;
    }

    public void gameOver()
    {
        gameOverUI.gameObject.SetActive(true);
        isDeath = true;
        pauseMission();
        totalPointsDeath = CalculatePoints() - deathPenalty;
    }

    public void OnRespawn(Vector3 position, Quaternion rotation, bool transformChange)
    {
        CharacterRespawner characterRespawner = m_Character.GetComponent<CharacterRespawner>();
        characterRespawner.Respawn(position, rotation, transformChange);
        
        resumeMission();
    }

	public void pauseMission() 
	{
		Time.timeScale = 0;
	}
	
	public void resumeMission()
	{
        isDeath = false;
        gameOverUI.gameObject.SetActive(false);
	    Time.timeScale = 1;
	}
    
    public void GoToMainMenu()
    {
        Time.timeScale = 1;
        gameOverUI.gameObject.SetActive(false);
        SceneManager.LoadScene("MainMenu");
    }
}
