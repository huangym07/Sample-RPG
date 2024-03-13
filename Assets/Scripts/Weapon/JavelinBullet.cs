using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JavelinBullet : MonoBehaviour
{
    public int atkValue = 50;
    private Rigidbody rb;
    private Collider col;

    public float thrownExistTime = 20f; // 标枪丢出去后的存在时间
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        col = rb.GetComponent<Collider>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == Tags.PLAYER) return;
        transform.parent = collision.gameObject.transform;
        
        // rb.velocity = Vector3.zero; // 下面设置了 isKinematic = true，那么就不需要设置 velocity，所以这行应该注释掉
        rb.isKinematic = true; // 固定不动了
        col.enabled = false; // 投射出去撞到游戏物体的标枪，禁用它的碰撞器，让它不参与碰撞

        if (collision.gameObject.tag == Tags.ENEMY)
        {
            collision.gameObject.GetComponent<Enemy>().TakeDamage(atkValue);
        }

        Destroy(gameObject, 1f); // 标枪撞到游戏物体后，1s 后销毁
    }
}
