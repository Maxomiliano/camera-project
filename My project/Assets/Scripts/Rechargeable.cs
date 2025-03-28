using System;
using UnityEngine;

public interface IRechargeable
{
    void RechargeBattery(float ammount){}

    void DecreaseBattery(float ammount){}

    float CurrentBatteryPercentage { get; }
    float MaxBatteryPercentage { get; }
}
