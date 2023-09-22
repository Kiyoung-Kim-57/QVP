using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed;
    public GameObject[] weapons;
    public bool[] hasWeapons;
    public GameObject[] grenades;
    public int hasGrenade;

    float hAxis;
    float vAxis;
    float fireDelay;
    float reloadDelay = 3.0f;

    public int ammo;
    public int coin;
    public int health;
    

    public int maxAmmo;
    public int maxCoin;
    public int maxHealth;
    public int maxHasGrenade;



    bool wDown;
    bool iDown;
    bool sDown1;
    bool sDown2;
    bool fDown;
    bool rDown;
    bool isSwap;
    bool isFireReady;
    bool isBulletPresent;
    bool isReloadDone;
    
    

    Animator anim;
    GameObject nearObject;
    Weapon equipWeapon;

    Vector3 moveVec;
    
    void Awake()
    {
        anim = GetComponentInChildren<Animator>();
       
       
    }


    void Update()
    {
        GetInput();
        Interaction();
        Move();
        Attack();
        Swap();
        Reload();
        

    }

    void Swap()
    {
        int weaponIndex = -1;
        if (sDown1) weaponIndex = 0;
        if (sDown2) weaponIndex = 1;
        //if (sDown3) weaponIndex = 2;

        if (sDown1 || sDown2)
        {
            if(equipWeapon != null)
                equipWeapon.gameObject.SetActive(false);
            if (hasWeapons[weaponIndex] == true)
            {
                equipWeapon = weapons[weaponIndex].GetComponent<Weapon>();
                equipWeapon.gameObject.SetActive(true);

                anim.SetTrigger("doSwap");
                isSwap = true;

                Invoke("SwapOut", 0.4f);
            }
            else
            {
                Debug.Log("There is no Weapon");
            }

           
        }
    }

    void SwapOut()
    {
        isSwap = false;
    }

    void Move()
    {
        if (!isSwap)
        {
            moveVec = new Vector3(hAxis + vAxis, 0, vAxis - hAxis).normalized;
            transform.position += moveVec * speed * (wDown ? 0.3f : 1.5f) * Time.deltaTime;
            anim.SetBool("isRun", moveVec != Vector3.zero);
            anim.SetBool("isWalk", wDown);
            transform.LookAt(transform.position + moveVec);
        }

    }

    void Interaction()
    {
        if(iDown && nearObject != null)
        {
            if(nearObject.tag == "Weapon")
            {
                Item item = nearObject.GetComponent<Item>();
                int weaponIndex = item.value;
                hasWeapons[weaponIndex] = true;

                Destroy(nearObject);
            }
        }
    }



    void GetInput()
    {

        hAxis = Input.GetAxisRaw("Horizontal");
        vAxis = Input.GetAxisRaw("Vertical");
        wDown = Input.GetButton("Walk");
        iDown = Input.GetButtonDown("Interaction");
        fDown = Input.GetButtonDown("Fire1");
        rDown = Input.GetButtonDown("Reload");


        sDown1 = Input.GetButtonDown("Swap1");
        sDown2 = Input.GetButtonDown("Swap2");
        //sDown3 = Input.GetButtonDown("Swap3");
    }

    void Attack()
    {
        if (equipWeapon == null)
            return;

        fireDelay += Time.deltaTime;
        reloadDelay += Time.deltaTime;
        isFireReady = equipWeapon.rate < fireDelay;
        isBulletPresent = equipWeapon.currentAmmo > 0;
        isReloadDone = equipWeapon.reloadDelay < reloadDelay;

        if(fDown && isFireReady && isReloadDone)
        {
            Debug.Log("Attack");
            equipWeapon.Use();
            if(equipWeapon.type == Weapon.Type.Melee)
            {
                anim.SetTrigger("doSwing");
            }
            else if(equipWeapon.type == Weapon.Type.Range && isBulletPresent)
            {
                anim.SetTrigger("doShot");
            }
     
            Debug.Log(equipWeapon.type == Weapon.Type.Melee ? "doSwing" : "doShot");
            fireDelay = 0;
        }

        
    }

    void maxBullet()
    {
        equipWeapon.currentAmmo = equipWeapon.maxAmmo;
    }

    void Reload()
    {
        if (rDown && equipWeapon.type == Weapon.Type.Range && equipWeapon.currentAmmo < equipWeapon.maxAmmo)
        {
            anim.SetTrigger("doReload");

            Invoke("maxBullet", 3f);
            
            reloadDelay = 0;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "Weapon")
        {
            nearObject = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Weapon")
        {
            nearObject = null;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Item")
        {
            Item item = other.GetComponent<Item>();
            switch (item.type)
            {
                case Item.Type.Ammo:
                    ammo += item.value;
                    if (ammo > maxAmmo)
                        ammo = maxAmmo;
                    break;
                case Item.Type.Coin:
                    coin += item.value;
                    if (coin > maxAmmo)
                        coin = maxAmmo;
                    break;
                case Item.Type.Grenade:
                    grenades[hasGrenade].SetActive(true);
                    hasGrenade += 1;
                    if (hasGrenade > maxAmmo)
                        hasGrenade = maxAmmo;
                    break;
                case Item.Type.Heart:
                    health += item.value;
                    if (health > maxAmmo)
                        health = maxAmmo;
                    break;
            }
            Destroy(other.gameObject);
        }
    }
}
