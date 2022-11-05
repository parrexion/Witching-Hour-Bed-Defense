using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterTower : MonoBehaviour {
    public GameObject projectile;
    public float firingDelay;
    public float bulletSpeed;
    public enum Targetting{Random, CloseToTower, CloseToBed, Weak, Strong};
    public Targetting targetting;

    private void Start() {
        StartCoroutine(fire());  
    }

    IEnumerator fire() {
        yield return new WaitForSeconds(firingDelay);
        Transform enemy = getEnemy();
        if(enemy == null) {StartCoroutine(fire());}
        Vector2 angle = enemy.position - transform.position;
        angle = Vector3.Normalize(angle);
        GameObject tempBullet = Instantiate(projectile, transform.position, Quaternion.identity);
        tempBullet.GetComponent<Rigidbody2D>().velocity = angle*bulletSpeed;
        StartCoroutine(fire());
    }

    private Transform getEnemy() {
        switch((int)targetting) {
            case 0:
                return(getRandomEnemy());
        }
        return null;
    }

    private Transform getRandomEnemy() {
        List<Enemy> enemies = EnemySpawner.instance.getEnemies();
        if(enemies.Count <= 0) {return null;}
        int i = Random.Range(0, enemies.Count);
        return(enemies[i].transform);
    }

}
