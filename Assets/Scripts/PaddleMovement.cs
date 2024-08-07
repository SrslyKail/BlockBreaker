using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PaddleMovement : MonoBehaviour
{
    //Config Parameters go here
    [SerializeField] float screenWidthinUnits = 16f;
    [SerializeField] float paddlePosMin = 1;
    [SerializeField] float paddlePosMax = 15;

    //Cached references
    GameSession theGameSession;
    Ball gameBall;

    private void Start()
    {
        theGameSession = FindObjectOfType<GameSession>();
        gameBall = FindObjectOfType<Ball>();
    }
    void Update()
    {
        Vector2 paddlePos = new Vector2(transform.position.x, transform.position.y); //paddlePos = Paddle Position on screen
        paddlePos.x = Mathf.Clamp(GetXPos(), paddlePosMin, paddlePosMax);
        transform.position = paddlePos;
    }

    private float GetXPos()
    {
        if (theGameSession.IsAutoPlayEnabled())
        {
            return gameBall.transform.position.x;
        }
        else
        {
            return Input.mousePosition.x / Screen.width * screenWidthinUnits;
        }
    }
    
}

