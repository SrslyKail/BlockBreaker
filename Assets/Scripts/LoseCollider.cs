using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoseCollider : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Loads the Game Over screen when the ball falls off the bottom
        SceneManager.LoadScene("GameOver");
    }
}
