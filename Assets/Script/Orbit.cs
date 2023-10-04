using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orbit : MonoBehaviour
{
    public Player player;
    public Transform target;
    public float orbitSpeed;
    Vector3 offset;

    public void GetGrenade()
    {
        player.grenades[player.hasGrenade].SetActive(true);
        player.hasGrenade += 1;
        if (player.hasGrenade > player.maxAmmo)
            player.hasGrenade = player.maxAmmo;
        //grenades[hasGrenade].SetActive(true);
        //hasGrenade += 1;
        //if (hasGrenade > maxAmmo)
        //    hasGrenade = maxAmmo;
    }

    void Start()
    {
        offset = transform.position - target.position;
    }

    
    void Update()
    {
        transform.position = target.position + offset;
        transform.RotateAround(target.position,
                               Vector3.up,
                               orbitSpeed * Time.deltaTime);
        offset = transform.position - target.position;
    }

    
}
