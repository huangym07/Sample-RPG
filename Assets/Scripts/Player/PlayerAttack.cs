using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public Weapon weapon;
    public Sprite weaponIcon;
    private ItemSO itemSO;
    private void Update()
    {
        if (weapon != null && Input.GetKeyDown(KeyCode.Space))
        {
            weapon.Attack(); 
        }
    }
    public void LoadWeapon(Weapon weapon)
    {
        this.weapon = weapon;
    }
    public void LoadWeapon(ItemSO itemSO, PlayerProperty playerProperty)
    {
        if (this.weapon != null)
        {
            Destroy(this.weapon.gameObject);
            foreach (Property property in this.itemSO.propertyList)
            {
                playerProperty.RemoveProperty(property.type, property.value);
            }
            this.weapon = null;
        }

        GameObject weaponGo = GameObject.Instantiate(itemSO.prefab);
        string weaponPositionName = itemSO.prefab.name + "Position"; // 注意这里不要使用 weaponGo.name，因为 GameObject.Instantiate() 出来的游戏物体的名字会带有 (Clone)
        //print("生成的名字是" + weaponPositionName);
        GameObject weaponPosition = transform.Find(weaponPositionName).gameObject;
        weaponGo.transform.SetParent(weaponPosition.transform);
        weaponGo.transform.localPosition = Vector3.zero; // 注意这里要用 localPosition
        weaponGo.transform.localRotation = Quaternion.identity; // localRotation
        
        foreach (Property property in itemSO.propertyList)
        {
            playerProperty.AddProperty(property.type, property.value);
        }
        this.weapon = weaponGo.GetComponent<Weapon>();
        this.weaponIcon = itemSO.icon;
        this.itemSO = itemSO;
    }
    public void UnloadWeapon()
    {
        this.weapon = null;
    }
}
