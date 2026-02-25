using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class House : MonoBehaviour
{
    [Header("Amounts")]
    [SerializeField] private int woodAmount;
    [SerializeField] private Color startColor;
    [SerializeField] private Color colorNoWood;
    [SerializeField] private Color endColor;
    [SerializeField] private float timeAmount;

    [Header("Visual Settings")]
    [SerializeField, Range(0f, 1f)] private float noWoodAlpha = 0.5f;

    [Header("Components")]
    [SerializeField] private GameObject houseColl;
    [SerializeField] private SpriteRenderer houseSprite;
    [SerializeField] private Transform point;

    [SerializeField] private bool detectingPlayer;
    private PlayerAnim playerAnim;
    private PlayerItems playerItems;
    private Player player;

    private float timeCount;
    private bool isBegining;

    void Start()
    {
        player = FindObjectOfType<Player>();
        playerAnim = player.GetComponent<PlayerAnim>();
        playerItems = player.GetComponent<PlayerItems>();
    }

    
    void Update()
    {
        if (detectingPlayer && !isBegining)
        {
            if (playerItems.TotalWood < woodAmount)
            {
                Color c = colorNoWood;
                c.a = noWoodAlpha;
                houseSprite.color = c;
            }
            else
            {
                houseSprite.color = startColor;
            }
        }

        if (detectingPlayer && Input.GetKeyDown(KeyCode.E) && playerItems.TotalWood >= woodAmount)
        {//construção é inicializada
            isBegining = true;
            playerAnim.OnHammeringStarted();
            houseSprite.color = startColor;
            player.transform.position = point.position;
            player.isPaused = true;
            playerItems.TotalWood -= woodAmount;
        }


        if (isBegining)
        {
            timeCount += Time.deltaTime;

            if(timeCount >= timeAmount)
            {//casa é finalizada
                isBegining = false;
                playerAnim.OnHammeringEnded();
                houseSprite.color = endColor;
                houseSprite.enabled = true;
                player.isPaused = false;
                houseColl.SetActive(true);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            detectingPlayer = true;
            houseSprite.enabled = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            detectingPlayer = false;
            if (!isBegining)
            {
                houseSprite.enabled = false;
            }
        }
    }
}
