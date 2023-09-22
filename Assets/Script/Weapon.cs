using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public enum Type { Melee, Range };
    public Type type;
    public int damage;
    public int maxAmmo;
    public int currentAmmo;
    public float rate;
    public float reloadDelay;
    
    public BoxCollider meleeArea;
    public TrailRenderer trailEffect;
    public Transform bulletPosition;
    public GameObject bullet;
    public Transform bulletCasePosition;
    public GameObject bulletCase;

    public void Use()
    {
        if (type == Type.Melee)
        {
            StopCoroutine("Swing");
            StartCoroutine("Swing");

        }
        else if (type == Type.Range && currentAmmo > 0)
        {
            currentAmmo--;
            StartCoroutine("Shot");
        }
    }

    

    IEnumerator Swing()
    {
        //1
        yield return new WaitForSeconds(0.1f); //0.1초 대기
        meleeArea.enabled = true;
        trailEffect.enabled = true;
        Debug.Log("TrailEffect Enabled");
        //2
        yield return new WaitForSeconds(0.3f); //1프레임 대기
        meleeArea.enabled = false;

        yield return new WaitForSeconds(0.3f);
        trailEffect.enabled = false;
        trailEffect.Clear();
        Debug.Log("TrailEffect Disabled");

    }

    IEnumerator Shot()
    {
        //1. bullet fire
        GameObject instantBullet = Instantiate(bullet, bulletPosition.position, bulletPosition.rotation);
        Rigidbody bulletRigid = instantBullet.GetComponent<Rigidbody>();
        bulletRigid.velocity = bulletPosition.forward * 50;
        yield return null;
        //2. bullet case
        GameObject instantCase = Instantiate(bulletCase, bulletCasePosition.position, bulletCasePosition.rotation);
        Rigidbody caseRigid = instantCase.GetComponent<Rigidbody>();
        Vector3 caseVec = bulletCasePosition.forward * Random.Range(-3, -2) + Vector3.up * Random.Range(2, 3);
        caseRigid.AddForce(caseVec, ForceMode.Impulse);
        caseRigid.AddTorque(Vector3.up * 10, ForceMode.Impulse);
    }

    
}
