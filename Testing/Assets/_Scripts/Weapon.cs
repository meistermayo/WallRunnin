using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Weapon", menuName = "Gameplay/Weapon", order = 1)]
public class Weapon : ScriptableObject {
    public float cooldown;
    public bool isAuto;
    public int burst;
    public bool isRaycast;
}
