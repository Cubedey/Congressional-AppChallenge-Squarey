using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileScript : MonoBehaviour
{
    private GameObject player;
    private GameObject enemy;
    private float speed;
    private Vector2 target;
    private bool move = false;

    void Update()
    {
        if (move)
        {
            speed+=1;
            target = enemy.transform.position;
            float step = speed * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, target, step);
        }
        
    }
      private void OnCollisionEnter2D(Collision2D collision)
{
    if (collision.gameObject.tag=="Enemy") {
            Destroy(gameObject);
            move=false;
    }
}

    public void Shoot(GameObject p, GameObject e, float s)
    {
        player = p;
        enemy = e;
        speed = s;
        move = true;
    }
}
