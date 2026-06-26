using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Javalin : Weapon
{
    public float bulletSpeed = 5;
    public GameObject bulletPrefab;
    private GameObject bulletGo;

    public Transform PlayerTransform;
    private Animator animator;

    [Header("攻击设置")]
    private float attackAnimLength = 1.5f;
    private bool isAttacking = false;
    private float attackTimer;

    private void Start()
    {
        PlayerTransform = GameObject.FindGameObjectWithTag(TagManager.PLAYER).transform;
        animator = PlayerTransform.GetComponent<Animator>();

        // 不在 Start 时生成子弹，按下 E 时生成
    }

    void Update()
    {
        Attack();
    }

    public override void Attack()
    {
        // 攻击倒计时逻辑（播放动画期间等待）
        if (isAttacking)
        {
            attackTimer -= Time.deltaTime;
            if (attackTimer <= 0f)
            {
                isAttacking = false;
                animator.SetBool("isShooting", false);

                // 动画播放完毕后发射子弹（如果存在）
                if (bulletGo != null)
                {
                    // 先解除父子关系，然后让子弹移动
                    if (bulletGo.transform.parent != null)
                    {
                        bulletGo.transform.parent = null;
                    }

                    var rb = bulletGo.GetComponent<Rigidbody>();
                    if (rb != null)
                    {
                        rb.velocity = bulletGo.transform.up * bulletSpeed;
                    }

                    bulletGo = null;
                    // 发射后半秒预生成下一发（保持原有行为）
                    Invoke(nameof(SpawnBullet), 0.5f);
                }
            }
        }

        // 攻击输入：按 E 键生成子弹并播放 shooting 动画
        if (Input.GetKeyDown(KeyCode.E))
        {
            // 如果当前正在播放攻击动画，则忽略额外按键
            if (isAttacking) return;

            // 如果当前没有挂载的子弹，则生成一发并等待动画结束再发射
            if (bulletGo == null)
            {
                SpawnBullet();
            }

            isAttacking = true;
            attackTimer = attackAnimLength;
            animator.SetBool("isShooting", true);
        }
    }

    private void SpawnBullet()
    {

        // 生成并作为武器的子对象，等待动画结束再发射
        bulletGo = GameObject.Instantiate(bulletPrefab, transform.position, transform.rotation);
        bulletGo.transform.parent = transform;
        // 确保刚体在生成时不会受其他力影响（可选，根据预制体设置）
        var rb = bulletGo.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = Vector3.zero;
            rb.isKinematic = false; // 保持可物理驱动（发射时使用 velocity）
        }

    }
}


