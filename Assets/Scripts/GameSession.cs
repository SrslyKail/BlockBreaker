using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameSession : MonoBehaviour
{
    //config params
    [Range(0.5f, 10f)] [SerializeField] float gameSpeed = 1f;
    [SerializeField] int pointsPerBlockDestroyed = 15;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] bool isAutoPlayEnabled;
    //Set Cursor visibility
    [SerializeField] bool mouseVisible = false;



    //state variables
    [SerializeField] int gameScore = 0;

    //Makes sure the score/music doesnt get overwritten by new scenes
    private void Awake()
    {
        int gameStatusCount = FindObjectsOfType<GameSession>().Length;
        if (gameStatusCount > 1)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }
    //Sets score on screen to 0
    private void Start()
    {
        scoreText.text = gameScore.ToString();
        Cursor.visible = mouseVisible;
    }
    // Update is called once per frame
    void Update()
    {
        Time.timeScale = gameSpeed;
    }

    public void AddToScore()
    {
        gameScore = gameScore + pointsPerBlockDestroyed;
        scoreText.text = gameScore.ToString();
    }
    public void RestartGame()
    {
        Destroy(gameObject);
    }
    public bool IsAutoPlayEnabled()
    {
        return isAutoPlayEnabled;
    }
}
