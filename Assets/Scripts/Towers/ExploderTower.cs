using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExploderTower : MonoBehaviour {


    public Exploder exploder;
    private List<EnemyHealth> enemiesNearby = new List<EnemyHealth>();

    public void explode() {
        setEnemiesNearby();
        for(int i = enemiesNearby.Count-1; i >= 0; i--) {
            enemiesNearby[i].DamageEnemy(exploder.damage);
        }
        Destroy(gameObject);
    }
    private void setEnemiesNearby() {
        enemiesNearby.Clear();
        LayerMask mask = LayerMask.GetMask("Default");
        ContactFilter2D filter = new ContactFilter2D();
        filter.layerMask = mask;
        filter.useLayerMask = true;
        List<Collider2D> results = new List<Collider2D>();
        Physics2D.OverlapCircle(transform.position, exploder.reach, filter, results);
        foreach(Collider2D hit in results) {
            if(hit.GetComponent<Enemy>() != null) {
                enemiesNearby.Add(hit.GetComponent<Enemy>().enemyHealth);
            }
        }
    }

}
