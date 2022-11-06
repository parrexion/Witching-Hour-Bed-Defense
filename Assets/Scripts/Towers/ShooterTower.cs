using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterTower : MonoBehaviour {
    public GameObject projectile;
    public Shooter shooter;
    public Animator anim;

    private Transform target;

    private void Start() {
        StartCoroutine(fire());  
    }

    IEnumerator fire() {
        yield return new WaitForSeconds(shooter.firingDelay);
        if(target == null) {target = getRandomEnemy();}
        if(target == null) {anim.SetBool("Firing", false); StartCoroutine(fire()); yield break;}
        anim.SetBool("Firing", true);
        Vector2 angle = target.position - transform.position;
        angle = Vector3.Normalize(angle);
        GameObject tempBullet = Instantiate(projectile, transform.position, Quaternion.identity);
        tempBullet.GetComponent<Rigidbody2D>().velocity = angle*shooter.bulletSpeed;
        tempBullet.GetComponent<TowerProjectile>().damage = shooter.damagePerBullet;
        StartCoroutine(fire());
    }

    private Transform getEnemy() {
        switch((int)shooter.targetting) {
            case 0:
                return(getRandomEnemy());
        }
        return null;
    }

    private Transform getRandomEnemy() {
        List<Enemy> enemies = EnemySpawner.instance.GetEnemies();
        if(enemies.Count <= 0) {return null;}
        int i = Random.Range(0, enemies.Count);
        return(enemies[i].transform);
    }

}
