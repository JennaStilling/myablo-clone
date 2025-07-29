using UnityEngine;
using UnityEngine.AI;

public class SkeletonAI : BasicAI
{
    enum SkeletonState { Wandering, Pursuing, Attacking, Dead }
    SkeletonState aiState = SkeletonState.Wandering;

    // Wander State Variables
    [SerializeField] float maxWanderDistance = 6;
    Vector3 startPosition = Vector3.zero;

    // Pursuit State Variables
    GameObject target;
    [SerializeField] float maxPursuiteDistance = 15f;
    [SerializeField] float attackRange = 1.5f;

    // Attack State Variables
    [SerializeField] float damage = 3;
    [SerializeField] float attackCooldown = 2.5f;
    float attackCooldownTimer = 0.0f;
    [SerializeField] GameObject attackprefab;

    // Death State Variables
    [SerializeField] float experienceValue = 10f;

    protected override void Start()
    {
        base.Start();
        startPosition = transform.position;
    }
    protected override void RunAI()
    {
        switch (aiState)
        {
            case SkeletonState.Wandering:
                RunWandering();
                break;
            case SkeletonState.Pursuing:
                RunPursuing();
                break;
            case SkeletonState.Attacking:
                RunAttacking();
                break;
            case SkeletonState.Dead:

                break;
        }
    }

    #region Wandering
    void TriggerWandering()
    {
        aiState = SkeletonState.Wandering;
        GetNewWanderDestination();
    }
    void RunWandering()
    {
        float x = agent.destination.x;
        float y = transform.position.y;
        float z = agent.destination.z;
        Vector3 checkPosition = new Vector3(x, y, z);

        if (Vector3.Distance(transform.position, checkPosition) < 1f)
            GetNewWanderDestination();
    }
    void GetNewWanderDestination()
    {
        float randomX = Random.Range(-maxWanderDistance, maxWanderDistance);
        float randomZ = Random.Range(-maxWanderDistance, maxWanderDistance);
        float x = randomX + startPosition.x;
        float y = startPosition.y;
        float z = randomZ + startPosition.z;
        agent.destination = new Vector3(x, y, z);
    }
    #endregion

    #region Pursuing
    void TriggerPursuing(GameObject targetToPursue)
    {
        aiState = SkeletonState.Pursuing;
        target = targetToPursue;
    }
    void RunPursuing()
    {
        agent.destination = target.transform.position;

        if (Vector3.Distance(transform.position, target.transform.position) <= attackRange)
            TriggerAttacking();
        else if (Vector3.Distance(transform.position, target.transform.position) >= maxPursuiteDistance)
            TriggerWandering();
    }
    #endregion

    #region Attacking
    void TriggerAttacking()
    {
        aiState = SkeletonState.Attacking;
        agent.destination = transform.position;
    }
    void RunAttacking()
    {
        attackCooldownTimer += Time.deltaTime;

        if(attackCooldownTimer >= attackCooldown)
        {
            attackCooldownTimer -= attackCooldown;
            SpawnAttackPrefab();
            GetComponent<EnemyAnimator>().TriggerAttack();
        }

        if (Vector3.Distance(target.transform.position, transform.position) > attackRange)
            TriggerPursuing(target);
    }
    void SpawnAttackPrefab()
    {
        Vector3 attackDirection = target.transform.position - transform.position;
        Vector3 spawnPosition = (attackDirection.normalized * attackRange) + transform.position;

        GameObject newAttack = Instantiate(attackprefab, spawnPosition, Quaternion.identity);
        newAttack.GetComponent<CombatActor>().SetFactionID(factionID);
    }
    #endregion

    #region Dead
    public override void TriggerDeath()
    {
        aiState = SkeletonState.Dead;
        base.TriggerDeath();
        // Add some experience!
        EventsManager.instance.onExperienceGranted.Invoke(experienceValue);
    }
    #endregion

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<CombatReceiver>())
        {
            if(other.GetComponent<CombatReceiver>().GetFactionID() != factionID)
            {
                TriggerPursuing(other.gameObject);
            }
        }
    }
}

