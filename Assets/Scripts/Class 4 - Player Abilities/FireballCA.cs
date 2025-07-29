using UnityEngine;

public class FireballCA : CombatActor
{
    [SerializeField] float speed = 25;
    Vector3 shootDirection = Vector3.zero;

    void Start()
    {
        Destroy(gameObject, 5);
    }
    void FixedUpdate()
    {
        transform.Translate(shootDirection * speed * Time.fixedDeltaTime);
    }

    public void SetShootDirection(Vector3 newDirection)
    {
        shootDirection = newDirection;
    }

    protected override void HitReceiver(CombatReceiver target)
    {
        base.HitReceiver(target);
        EffectsManager.instance.PlaySmallBoom(transform.position, 1);
    }
    protected override void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<CombatReceiver>() && !other.isTrigger)
        {
            if (other.GetComponent<CombatReceiver>().GetFactionID() != factionID)
            {
                HitReceiver(other.GetComponent<CombatReceiver>());
                Destroy(gameObject);
            }
        }
    }
}
