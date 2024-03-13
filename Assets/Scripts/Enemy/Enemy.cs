using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    private NavMeshAgent enemyAgent;
    private EnemyState enemyState = EnemyState.NormalState;
    private EnemyState childState = EnemyState.RestingState;

    public float restingTime = 2f;
    private float restingTimer = 0;

    public float moveDistanceDownBound = 2f;
    public float moveDistanceUpBound = 5f;

    public int HP = 100;
    public int exp = 30;
    public enum EnemyState
    {
        NormalState, 
        MovingState,
        RestingState,
        FightingState
    }
    // Start is called before the first frame update
    void Start()
    {
        enemyAgent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (enemyState == EnemyState.NormalState)
        {
            if (childState == EnemyState.RestingState)
            {
                restingTimer += Time.deltaTime;
                if (restingTimer > restingTime)
                {
                    Vector3 randomPosition = FindRandomPosition();
                    enemyAgent.SetDestination(randomPosition);
                    childState = EnemyState.MovingState;
                }
            } else if (childState == EnemyState.MovingState)
            {
                if (enemyAgent.pathPending == false && enemyAgent.remainingDistance <= 0)
                {
                    restingTimer = 0;
                    childState = EnemyState.RestingState;
                }
            }
        }

        // 测试用
        //if (input.getkeydown(keycode.space))
        //{
        //    takedamage(30);
        //}
    }

    Vector3 FindRandomPosition()
    {
        Vector3 randomDir = new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f));
        return transform.position + randomDir * Random.Range(moveDistanceDownBound, moveDistanceUpBound);
    }

    public void TakeDamage(int damage)
    {
        HP -= damage;
        if (HP <= 0)
        {
            Died();
        }
    }

    public void Died()
    {
        GetComponent<CapsuleCollider>().enabled = false;
        int count = 4;
        for (int i = 0; i < count; i++)
        {
            SpawnPickableObject();
        }
        EventCenter.EnemyDied(this);
        EnemySpawner.enemyNumber--;
        Destroy(gameObject);
    }
    private void SpawnPickableObject()
    {
        ItemSO item = ItemDBManager.Instance.GetRandomItem();
        GameObject go = GameObject.Instantiate(item.prefab, transform.position, transform.rotation);
        go.tag = Tags.INTERACTABLE;
        PickableObject po = go.GetComponent<PickableObject>();
        if (po == null)
        {
             po = go.AddComponent<PickableObject>();
        }
        po.itemSO = item;
        // 由于武器具有动画，所以在 transform.position 生成武器后，武器会跑到动画指定的位置
        // 由于生成的武器父物体是整个场景，所以跑到相对于整个场景的动画指定的位置
        // 所以将动画组件禁用掉
        Animator animator = go.GetComponent<Animator>();
        if (animator != null)
        {
            animator.enabled = false;
        }
        go.transform.position = transform.position;

        // 禁用掉 NavMeshObstacle
        NavMeshObstacle navMeshObstacle = go.GetComponent<NavMeshObstacle>();
        if (navMeshObstacle != null)
        {
            navMeshObstacle.enabled = false;
        }
        // 支持碰撞
        Rigidbody rb = go.GetComponent<Rigidbody>();
        if (rb == null)
        {
            rb = go.AddComponent<Rigidbody>();
        }
        
        if (rb != null)
        {
            rb.isKinematic = false;
            rb.useGravity = true;
        }
        
        Collider collider = go.GetComponent<Collider>();
        if (collider != null)
        {
            collider.enabled = true;
            collider.isTrigger = false;
        }
    }
}
