using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public GameObject menuCam;
    public GameObject gameCam;
    public Player player;
    
    public int stage;
    public float playTime;
    public bool isBattle;
    public int enemyCntB;


    public GameObject menuPanel;
    public GameObject gamePanel;
    public GameObject skillTest;

    
    public TMP_Text maxScore;
    public TMP_Text playerCoin;
    public TMP_Text playerHealth;
    public TMP_Text playerAmmo;
    
    public Image weapon1Img;
    public Image weapon2Img;
    public Image weapon3Img;
    public Image weaponLImg;

    //public void SkillTestUp()
    //{
    //    skillTest.transform.position = Vector3.zero;
    //}

    //public void SkillTestDown()
    //{
    //    skillTest.transform.position = Vector3.down * 1000;
    //}

    void LateUpdate()
    {
        //playerHealth.text = player.health + " / " + player.maxHealth;
        //playerCoin.text = string.Format("{0:n0}", player.coin);

        if (player.equipWeapon == null)
            playerAmmo.text = "- / " + player.ammo;
        else if (player.equipWeapon.type == Weapon.Type.Melee)
            playerAmmo.text = "- / " + player.ammo;
        else if (player.equipWeapon.type == Weapon.Type.Range)
            playerAmmo.text = player.equipWeapon.currentAmmo + " / " + player.ammo;




    }


}
