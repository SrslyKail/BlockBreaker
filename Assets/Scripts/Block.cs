using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    //Config Params
    [SerializeField] AudioClip breakSound;
    [SerializeField] GameObject blockSparkleVFX;
    [SerializeField] Sprite[] hitSprites;
    [SerializeField] AudioSource audioSource;

    //Cached reference
    Level level;
    GameSession gameStatus;

    //State Variables
    [SerializeField] int timesHit; //Serialized for debug purposes


    private void Start()
    {
        CountBreakableBlocks();
        gameStatus = FindObjectOfType<GameSession>();
        audioSource = FindObjectOfType<AudioSource>();
        audioSource.clip = breakSound;

    }

    private void CountBreakableBlocks()
    {
        level = FindObjectOfType<Level>();
        if (tag == "Breakable")
        {
            level.CountBlocks();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (tag == "Breakable")
        {
            HandleHit();
        }
    }

    private void HandleHit()
    {
        timesHit++;
        int maxHits = hitSprites.Length + 1;
        if (timesHit >= maxHits)
        {
            DestroyBlock();
        }
        else
        {
            ShowNextHitSprite();
        }
    }

    private void ShowNextHitSprite()
    {
        int spriteIndex = timesHit - 1;
        if (hitSprites[spriteIndex] != null)
        {
            GetComponent<SpriteRenderer>().sprite = hitSprites[spriteIndex];
        }
        else
        {
            Debug.LogError("Block sprite is missing from array " + gameObject.name);
        }
    }

    private void DestroyBlock()
    {
    PlayBlockDestroySFX();
    Destroy(gameObject);
    level.BlockDestroyed();
    TriggerSparklesVFX();
    }

    private void PlayBlockDestroySFX()
    {
        gameStatus.AddToScore();
        audioSource.Play();
        //AudioSource.PlayClipAtPoint(breakSound, Camera.main.transform.position, volume);
    }

    private void TriggerSparklesVFX()
    {
        GameObject sparkles = Instantiate(blockSparkleVFX, transform.position, transform.rotation);
        Destroy(sparkles, 2);
    }
}
