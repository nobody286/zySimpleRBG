using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerAttack : MonoBehaviour
{
    public Weapon weapon; //当前装备的武器
    void Start()
    {
        
    }
    void Update()
    {
       if(weapon != null && Input.GetKeyDown(KeyCode.E))
        {
            weapon.Attack();
        }
    }
    public void LoadWeapon(Weapon Weapon)
    {
        this.weapon = Weapon;
    }
    public void UnloadWeapon()
    {
        weapon = null;
    }
}
