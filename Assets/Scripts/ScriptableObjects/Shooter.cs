using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName="ScriptableObjects/Shooter")]
public class Shooter : Building {
    public float damagePerBullet;
    public float bulletSpeed;
    public float firingDelay;
    public enum Targetting{Random, CloseToTower, CloseToBed, Weak, Strong};
    public Targetting targetting;

}
