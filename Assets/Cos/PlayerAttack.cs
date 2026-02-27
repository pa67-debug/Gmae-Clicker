using UnityEngine;
using System.Collections;

public class PlayerAttack : MonoBehaviour
{
    private Animator anim;

    public Collider2D attackCollider;
    public int damage = 1;

    [Header("Auto Attack Settings")]
    public float autoAttackInterval = 60f;

    private float autoAttackTimer = 0f;
    private bool isAttacking = false;

    [Header("Critical Settings")]
    public float critChance = 0f;
    public float critMultiplier = 2f;

    void Start()
    {
        anim = GetComponent<Animator>();
        attackCollider.enabled = false;

        // โหลดค่าจากเซฟ
        if (GameManager.Instance != null)
            GameManager.Instance.LoadGame(this);
    }

    void Update()
    {
        // กดโจมตีเอง
        if (Input.GetMouseButtonDown(0) && !isAttacking)
        {
            StartCoroutine(AttackRoutine());
        }

        // ระบบตีอัตโนมัติแบบแม่นยำ
        autoAttackTimer += Time.deltaTime;

        if (autoAttackTimer >= autoAttackInterval)
        {
            autoAttackTimer = 0f;

            if (!isAttacking)
                StartCoroutine(AttackRoutine());
        }
    }

    IEnumerator AttackRoutine()
    {
        isAttacking = true;

        anim.ResetTrigger("Attack");
        anim.SetTrigger("Attack");

        attackCollider.enabled = true;
        yield return new WaitForSeconds(0.2f);
        attackCollider.enabled = false;

        yield return new WaitForSeconds(0.1f);

        isAttacking = false;
    }

    // =========================
    // ⚡ ลดเวลาโจมตีอัตโนมัติ
    // =========================
    public void ReduceAutoAttackTime(float amount)
    {
        autoAttackInterval -= amount;

        if (autoAttackInterval < 1f)
            autoAttackInterval = 1f;
    }

    // =========================
    // 💥 ระบบคริติคอล
    // =========================
    public int GetDamage(out bool isCritical)
    {
        isCritical = false;

        float roll = Random.Range(0f, 100f);

        if (roll <= critChance)
        {
            isCritical = true;
            return Mathf.RoundToInt(damage * critMultiplier);
        }

        return damage;
    }
}