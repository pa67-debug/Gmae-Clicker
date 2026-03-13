using UnityEngine;
using System.Collections;

public class PlayerAttack : MonoBehaviour
{
    private Animator anim;

    public Collider2D attackCollider;
    public Collider2D clickCollider; // Collider ตัวลูกที่ใช้คลิก

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

        var gm = GameManager.Instance;

        damage = 1 + gm.weaponLevel;
        critChance = gm.critChanceLevel * 0.5f;
        critMultiplier = 2f + gm.critDamageLevel;

        autoAttackInterval = 60f - (gm.speedLevel * 0.5f);

        if (autoAttackInterval < 1f)
            autoAttackInterval = 1f;
    }

    void Update()
    {
        // ตรวจคลิกเมาส์
        if (Input.GetMouseButtonDown(0) && !isAttacking)
        {
            CheckClick();
        }

        // ระบบตีอัตโนมัติ
        autoAttackTimer += Time.deltaTime;

        if (autoAttackTimer >= autoAttackInterval)
        {
            autoAttackTimer = 0f;

            if (!isAttacking)
                StartCoroutine(AttackRoutine());
        }
    }

    void CheckClick()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Collider2D hit = Physics2D.OverlapPoint(mousePos);

        if (hit == clickCollider)
        {
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

    public void ReduceAutoAttackTime(float amount)
    {
        autoAttackInterval -= amount;

        if (autoAttackInterval < 1f)
            autoAttackInterval = 1f;
    }

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