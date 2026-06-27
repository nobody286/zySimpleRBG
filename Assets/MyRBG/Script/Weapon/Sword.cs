using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

public class Sword : Weapon
{
    public Transform PlayerTransform;
    private Animator animator;
    private NavMeshAgent playerAgent;
    private Collider col;

    //攻击相关参数
    [Header("攻击设置")]
    private float attackAnimLength = 1.25f;      //攻击动画长度
    private bool isAttacking = false;
    private float attackTimer;
    public int atkValue = 50;
    private void Start()
    {
        PlayerTransform = GameObject.FindGameObjectWithTag(TagManager.PLAYER).transform;
        animator = PlayerTransform.GetComponent<Animator>();
        playerAgent = PlayerTransform.GetComponent<NavMeshAgent>();
        col = GetComponent<Collider>();
        if(col != null && col.enabled == true)
        {
            col.enabled = false;
        }
    }
    void Update()
    {
        Attack();
        if(this.tag == TagManager.INTERACTABLE)
        {
            col.enabled = true;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == TagManager.ENEMY)
        {
            other.GetComponent<EnemyMove>().TakeDamage(atkValue);
        }
    }
    public override void Attack()
    {
        //======================攻击倒计时逻辑=======================
        if (isAttacking)
        {
     
            attackTimer -= Time.deltaTime;
            if (attackTimer <= 0.625 && attackTimer >= 0.5f && col.enabled == false)
            {
                col.enabled = true;
            }
            if(attackTimer <= 0.5 && attackTimer >= 0.25f && col.enabled == true)
            {
                col.enabled = false;
            }
            if(attackTimer <= 0.25 && attackTimer > 0f && col.enabled == false)
            {
                col.enabled = true;
            }

            if (attackTimer <= 0)
            {
                if(col.enabled == true)
                    col.enabled = false;
                isAttacking = false;
                animator.SetBool("isAttack", false);
            }

        }
        //playerAgent.isStopped = false; //非攻击时允许移动


        //========================攻击输入===========================
        if (Input.GetKeyDown(KeyCode.E) && !isAttacking)
        {
            isAttacking = true;
            attackTimer = attackAnimLength;
            animator.SetBool("isAttack", true);
        }
        //攻击逻辑在Update中处理，这里可以留空或者添加额外的攻击效果
    }
}
