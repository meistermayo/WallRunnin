using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Bullet", menuName = "Gameplay/Bullet", order = 1)]
public class Bullet : ScriptableObject {
    public float moveSpeed;
    public float damage;
    public float inaccuracy;
}
