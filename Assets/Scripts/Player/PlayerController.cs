using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private float mouseX;
    private bool isDead;
    private bool canDrop;

    private float playerState;
    private float score;
    private float life;
    private float deadTime;

    private const float leftLimit = -4.68f;
    private const float rightLimit = 3.75f;

    [Header("Character States")]
    [SerializeField] private List<Sprite> characterState = new();

    [Header("Character UI Data")]
    [SerializeField] private Text scoreText;
    [SerializeField] private Text lifeText;

    [Header("Audio Effects")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private List<AudioClip> audios = new();

    [Header("Other Components")]
    [SerializeField] LoadSceneManager loadSceneManager;
    [SerializeField] Stack stack;
    [SerializeField] Spawner spawner;

    void Start()
    {
        score = 0;
        life = 3;

        loadSceneManager = FindObjectOfType<LoadSceneManager>();
        stack = FindObjectOfType<Stack>();
        spawner = FindObjectOfType<Spawner>();
    }

    
    void Update()
    {
        scoreText.text = "SCORE: " + score;
        lifeText.text = "LIFES: " + life;
        
        CharacterMovement();
        DropBags();

        if (life <= -1)
        {
            life = 0;
            loadSceneManager.LoadSceneAsync();
        }

    }

    private void CharacterMovement()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseX = Mathf.Clamp(mousePos.x, leftLimit, rightLimit);
        if (!isDead)
        {
            transform.position = new Vector3(mouseX, transform.position.y, transform.position.z);

            playerState = Mathf.Clamp(playerState, 0f, 6f);

            GetComponent<SpriteRenderer>().sprite = playerState switch
            {
                0 => characterState[0],
                1 => characterState[1],
                2 => characterState[2],
                3 => characterState[3],
                4 => characterState[4],
                5 => characterState[5],
                6 => characterState[6],
                _ => characterState[0]
            };

            if (playerState == 6)
                isDead = true;

        }
        else
        {
            deadTime += Time.deltaTime;
            if (deadTime >= 2f)
            {
                playerState = 0f;
                life -= 1f;
                deadTime = 0f;
                isDead = false;
            }
        }
    }

    private void DropBags()
    {
        if (canDrop == true)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (playerState > 0f)
                {
                    audioSource.clip = audios[0];
                    audioSource.Play();
                    playerState -= 1f;
                    score += 3;
                    stack.stackHeight += 1;
                }

            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!isDead)
        {
            switch (other.gameObject.tag)
            { 
                case "Bag":
                    playerState += 1f;
                    score += 2;
                    audioSource.clip = audios[1];
                    audioSource.Play();
                    break;

                case "Platform":
                    canDrop = true;
                    break;

                case "Anvil":
                case "Flower":
                case "Fish":
                    HandleEnemyCollision(other.gameObject.tag);
                    break;

                case "1-Up":
                    audioSource.clip = audios[2];
                    audioSource.Play();
                    life += 1;
                    break;
            }
        }
    }

    private void HandleEnemyCollision(string tag)
    {
        if (isDead)
            return;

        audioSource.clip = audios[3];
        audioSource.Play();

        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();

        int spriteIndex = tag switch
        {
            "Anvil" => 7,
            "Flower" => 8,
            "Fish" => 9,
            _ => 0
        };

        spriteRenderer.sprite = characterState[spriteIndex];

        isDead = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Platform"))        
            canDrop = false;        
    }
}
