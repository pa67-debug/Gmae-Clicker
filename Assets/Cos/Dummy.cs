using UnityEngine;

public class Dummy : MonoBehaviour
{
    [Header("Damage Popup")]
    public GameObject damagePopupPrefab;   // ใส่ Prefab TextMeshPro (3D)
    public Transform popupPoint;           // จุดลอยดาเมจเหนือหัว

    private Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("PlayerAttack"))
        {
            PlayerAttack player = other.GetComponentInParent<PlayerAttack>();

            if (player != null)
            {
                bool isCritical;
                int finalDamage = player.GetDamage(out isCritical);

                TakeDamage(finalDamage, isCritical);
            }
        }
    }

    void TakeDamage(int damage, bool isCritical)
    {
        if (anim != null)
            anim.SetTrigger("Hit");

        if (GameManager.Instance != null)
            GameManager.Instance.AddCoins(damage);

        ShowDamage(damage, isCritical);
    }

    void ShowDamage(int damage, bool isCritical)
    {
        if (damagePopupPrefab == null || popupPoint == null)
            return;

        GameObject popup = Instantiate(
            damagePopupPrefab,
            popupPoint.position,
            Quaternion.identity
        );

        DamagePopup dp = popup.GetComponent<DamagePopup>();
        if (dp != null)
            dp.Setup(damage, isCritical);
    }
}