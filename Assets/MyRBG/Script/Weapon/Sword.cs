using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.AI;

public class Sword : Weapon
{
    public Transform PlayerTransform;
    private Animator animator;
    private NavMeshAgent playerAgent;

    //攻击相关参数
    [Header("攻击设置")]
    private float attackAnimLength = 1.25f;      //攻击动画长度
    private bool isAttacking = false;
    private float attackTimer;
    private void Start()
    {
        animator = PlayerTransform.GetComponent<Animator>();
        playerAgent = PlayerTransform.GetComponent<NavMeshAgent>();
    }
    void Update()
    {
        Attack();
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == TagManager.ENEMY)
        {
           print(other.name);
        }
    }
    public override void Attack()
    {
        //======================攻击倒计时逻辑=======================
        if (isAttacking)
        {
            attackTimer -= Time.deltaTime;
            if (attackTimer <= 0)
            {
                isAttacking = false;
                animator.SetBool("isAttack", false);
            }

            //playerAgent.isStopped = true;  //攻击时停止移动
            //return;

        }
        //playerAgent.isStopped = false; //非攻击时允许移动


        //========================攻击输入===========================
        if (Input.GetKeyDown(KeyCode.E))
        {
            isAttacking = true;
            attackTimer = attackAnimLength;
            animator.SetBool("isAttack", true);
        }
        //攻击逻辑在Update中处理，这里可以留空或者添加额外的攻击效果
    }
}
