using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventCenter : MonoBehaviour
{
    public static event Action<Enemy> onEnemyDied; // 基于返回值为 void 参数为一个 Enemy 类对象的委托，建立了一个事件
                                            // 之后每个需要监听敌人死亡的模块可以向 onEnemyDied 注册它们自己在敌人死亡时需要做的事
                                            // 之后在敌人死亡的时候，就可以调用 EventCenter 中的方法，来执行该事件上的注册过的委托
    public static void EnemyDied(Enemy enemy)
    {
        onEnemyDied?.Invoke(enemy);
    }
}
