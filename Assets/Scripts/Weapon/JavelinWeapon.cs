using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JavelinWeapon : Weapon
{
    public float bullletSpeed = 1;
    public GameObject bulletPrefab;
    private GameObject bulletGo = null;
    public void Start()
    {
        SpawnBullet();
    }
    public override void Attack()
    {
        if (bulletGo != null)
        {
            bulletGo.transform.parent = null;
            bulletGo.GetComponent<Rigidbody>().velocity = transform.forward * bullletSpeed;
            bulletGo.GetComponent<Collider>().enabled = true; // 修复语句3
            Destroy(bulletGo, bulletGo.GetComponent<JavelinBullet>().thrownExistTime);
            bulletGo = null;
            Invoke("SpawnBullet", 0.5f); // 0.5s 后调用 SpawnBullet() 方法
        }
        return;
    }
    void SpawnBullet()
    {
        bulletGo = GameObject.Instantiate(bulletPrefab, transform.position, transform.rotation);
        bulletGo.transform.parent = transform;

        // 修复手持的标枪碰到物体会发生碰撞的问题
        // 修复方式为：
        // 生成的手持标枪不启用 Collider（修复语句1）
        // 当发射出去后，才启用标枪的 Collider
        // 注意，生成的 Interactable 作为物品的标枪可以发生碰撞（修复语句2）
        bulletGo.GetComponent<Collider>().enabled = false; // 修复语句1
        if (tag == Tags.INTERACTABLE)
        {
            Destroy(bulletGo.GetComponent<JavelinBullet>());

            bulletGo.tag = Tags.INTERACTABLE;
            bulletGo.GetComponent <Collider>().enabled = true;  // 修复语句2
            Rigidbody rd = bulletGo.GetComponent<Rigidbody>();
            if (rd != null)
            {
                rd.useGravity = true;
                rd.isKinematic = false;
                rd.constraints = ~RigidbodyConstraints.FreezeAll;
            }
            PickableObject po = bulletGo.AddComponent<PickableObject>(); 
            po.itemSO = GetComponent<PickableObject>().itemSO;
            // 销毁自身，把 ItemSO 交给 JavelinBullet，通过捡起 JavelinBullet 来捡起 JavelinWeapon 这个 item
            bulletGo.transform.parent = null;
            Destroy(this.gameObject);
        }
    }
}
