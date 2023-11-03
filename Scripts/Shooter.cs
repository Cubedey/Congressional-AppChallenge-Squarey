using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    //drag in weapon prefabs
    public GameObject[] wPrefList;
    //drag in the player
    public GameObject player;

    //speediness
    public float speed;

    //takes in int value
    //0 for add, 1 for subtract, 2 for times, 3 for divide
    public void Shoot(int n)
    {
        GameObject projectile = Instantiate(wPrefList[n], player.transform.position, player.transform.rotation);
        projectile.GetComponent<ProjectileScript>().Shoot(player, player.GetComponent<SquareMove>().target, speed);
    }
}
