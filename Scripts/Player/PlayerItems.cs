using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerItems : MonoBehaviour
{
    [Header("Amounts")]
    [SerializeField] private int totalWood;
    [SerializeField] private float currentWater;
    [SerializeField] private int totalCarrots;
    [SerializeField] private int fishes;

    [Header("Limits")]
    [SerializeField] private float waterLimit = 50;
    [SerializeField] private float carrotsLimit = 10;
    [SerializeField] private float woodLimit = 5;
    [SerializeField] private float fishesLimit = 3f;

    public int TotalWood { get => totalWood; set => totalWood = value; }
    public float CurrentWater { get => currentWater; set => currentWater = value; }
    public int TotalCarrots { get => totalCarrots; set => totalCarrots = value; }
    public int Fishes { get => fishes; set => fishes = value; }
    public float WaterLimit1 { get => waterLimit; set => waterLimit = value; }
    public float CarrotsLimit { get => carrotsLimit; set => carrotsLimit = value; }
    public float WoodLimit { get => woodLimit; set => woodLimit = value; }
    public float FishesLimit { get => fishesLimit; set => fishesLimit = value; }

    public void WaterLimit(float water)
    {
        if(currentWater <= WaterLimit1)
        {
            currentWater += water;
        }
    }
}
