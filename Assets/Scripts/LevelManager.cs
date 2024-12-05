using System.Collections;
using System.Collections.Generic;
using System.IO.IsolatedStorage;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public float score = 0;
    
    private TextMeshProUGUI scoreText;

    public int targetsLeft;

    private float endTimer = 5f;
    private bool timerFinished = false;

    private GameObject endScreen;
    private GameObject star1;
    private GameObject star2;
    private GameObject star3;

    private LauncherController lc;

    public Sprite yellowStar;

    private float maxLevelScore = 0;

    private GameObject resetBtn;
    
    private static float THREE_STAR_PERCENT = 0.8f;
    private static float TWO_STAR_PERCENT = 0.6f;
    private static float ONE_STAR_PERCENT = 0.4f;
    

    void Start()
    {
        scoreText = GameObject.FindWithTag("Score").GetComponent<TextMeshProUGUI>();
        
        targetsLeft = FindObjectsOfType<TargetScript>().Length;

        endScreen = GameObject.Find("EndLevelUI");
        star1 = GameObject.Find("Star1");
        star2 = GameObject.Find("Star2");
        star3 = GameObject.Find("Star3");
        
        resetBtn = GameObject.Find("ResetLvlBtn");

        lc = FindObjectOfType<LauncherController>();

        ScoreScript[] allScores = FindObjectsOfType<ScoreScript>();

        for (int i = 0; i < allScores.Length; i++)
            maxLevelScore += allScores[i].scoreAdd;
    }

    void Update()
    {
        scoreText.text = "SCORE: " + score;

        if (targetsLeft == 0 || lc.projectileQueue.Count == 0)
        {
            endTimer -= Time.deltaTime;
        }

        if (!timerFinished && endTimer <= 0)
        {
            timerFinished = true;

            while (lc.projectileQueue.Count > 0)
            {
                score += lc.projectileQueue.Peek().GetComponent<ScoreScript>().scoreAdd;
                lc.projectileQueue.Dequeue();
            }
            
            if (score >= THREE_STAR_PERCENT * maxLevelScore)
            {
                star1.GetComponent<Image>().sprite = yellowStar;
                star2.GetComponent<Image>().sprite = yellowStar;
                star3.GetComponent<Image>().sprite = yellowStar;
            }
            else if (score >= TWO_STAR_PERCENT * maxLevelScore)
            {
                star1.GetComponent<Image>().sprite = yellowStar;
                star2.GetComponent<Image>().sprite = yellowStar;
            }
            else if (score >= ONE_STAR_PERCENT * maxLevelScore)
            {
                star1.GetComponent<Image>().sprite = yellowStar;
            }
        }
        
        if (timerFinished && endTimer <= -2)
        {
            endScreen.GetComponent<Image>().enabled = true;
            star1.GetComponent<Image>().enabled = true;
            star2.GetComponent<Image>().enabled = true;
            star3.GetComponent<Image>().enabled = true;
            
            scoreText.gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -10);
            resetBtn.gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -70);
        }
    }
    
    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
