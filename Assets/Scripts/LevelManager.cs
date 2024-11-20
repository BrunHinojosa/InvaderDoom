using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public float score = 0;
    
    private TextMeshProUGUI scoreText;

    void Start()
    {
        scoreText = GameObject.FindWithTag("Score").GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        scoreText.text = "SCORE: " + score;
    }
    
    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
