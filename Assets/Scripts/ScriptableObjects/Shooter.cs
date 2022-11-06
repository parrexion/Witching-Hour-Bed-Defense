using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName="ScriptableObjects/Shooter")]
public class Shooter : Building {
    public enum Targetting{Random, CloseToTower, CloseToBed, Weak, Strong};

    [Header("Stats")]
    public float damagePerBullet;
    public float bulletSpeed;
    public float firingDelay;
    public Targetting targetting;

}
