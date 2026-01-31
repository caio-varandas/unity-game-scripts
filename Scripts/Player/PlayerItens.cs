using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerItems : MonoBehaviour
{
    [Header("Amounts")]
    [SerializeField] private float totalWood;
    [SerializeField] private float currentWater;
    [SerializeField] private float totalCarrots;

    [Header("Limits")]
    private float waterLimit = 50;
    private float carrotsLimit = 10;
    private float woodLimit = 5;

    public float TotalWood { get => totalWood; set => totalWood = value; }
    public float CurrentWater { get => currentWater; set => currentWater = value; }
    public float TotalCarrots { get => totalCarrots; set => totalCarrots = value; }
    public float WaterLimit1 { get => waterLimit; set => waterLimit = value; }
    public float CarrotsLimit { get => carrotsLimit; set => carrotsLimit = value; }
    public float WoodLimit { get => woodLimit; set => woodLimit = value; }

    public void WaterLimit(float water)
    {
        if(currentWater <= WaterLimit1)
        {
            currentWater += water;
        }
    }

}
