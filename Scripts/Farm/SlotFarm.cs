using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotFarm : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Sprite hole;
    [SerializeField] private Sprite carrot;

    [Header("Settings")]
    [SerializeField] private int digAmount; //tempo para o player cavar para o buraco aparecer
    [SerializeField] private float waterAmount; //quantidade de aágua para nascer uma cenoura
    private bool isWatering;

    private int initialDigAmount;
    private float currentWater;
    private bool dugHole;
    private bool handInside;
    private bool hasCarrot;

    PlayerItems playerItems;

    void Start()
    {
        playerItems = FindObjectOfType<PlayerItems>();
        initialDigAmount = digAmount;
    }
    void Update()
    {
        if (!dugHole) return;

        HandleWatering();
        HandleHarvest();
        
    }

    public void HandleWatering()
    {
        if (!isWatering || hasCarrot) return;

        currentWater += 0.01f;

        if (currentWater >= waterAmount) //encheu total de água necessaria
        {
            GrowCarrot();
        }

    }

    public void GrowCarrot()
    {
        hasCarrot = true;
        spriteRenderer.sprite = carrot;
    }
    public void HandleHarvest()
    {
        if (!hasCarrot || !handInside) return;

        if (Input.GetKeyDown(KeyCode.E))
        {
            Harvest();
        }
    }
    public void Harvest()
    {
        hasCarrot = false;
        currentWater = 0f;
        spriteRenderer.sprite = hole;
        playerItems.TotalCarrots++;
    }
    public void OnHit()
    {
        digAmount--;

        if (digAmount <= initialDigAmount / 2) //cria o buraco
        { 
            spriteRenderer.sprite = hole;
            dugHole = true;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Dig"))
        {
            OnHit();
        }

        if (collision.CompareTag("Water"))
        {
            isWatering = true;
        }

        if (collision.CompareTag("Hand"))
        {
            handInside = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Water"))
        {
            isWatering = false;
        }
        if (collision.CompareTag("Hand"))
        {
            handInside = false;
        }
    }
}
