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
    public void LoadWeapon(Weapon weapon)
    {
        this.weapon = weapon;
    }
    public void LoadWeapon(ItemScriptObject itemSO)
    {

        if(weapon != null)
        {
            Destroy(weapon.gameObject);
            weapon = null;
        }
        string prefabName = itemSO.prefab.name;

        Transform weaponParent;
        if(itemSO.prefab.name == "桃木剑model")
        {
            weaponParent = GameObject.FindGameObjectWithTag("p1").transform;
            
        }
        else
        {
            weaponParent = GameObject.FindGameObjectWithTag("p2").transform;

        }
        GameObject weaponGo =  GameObject.Instantiate(itemSO.prefab);
        weaponGo.transform.parent = weaponParent;
        weaponGo.transform.localPosition = Vector3.zero;
        weaponGo.transform.localRotation = Quaternion.identity;
        this.weapon = weaponGo.GetComponent<Weapon>();
    }
    public void UnloadWeapon()
    {
        weapon = null;
    }
}
