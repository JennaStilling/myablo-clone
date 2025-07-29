using UnityEngine;

public class FireballEquippableAbility : EquippableAbility
{
    [SerializeField] protected float manaCost = 5;

    protected override void SuccessfulRaycastFunctionality(ref RaycastHit hit)
    {
        if (CanCastFireball(ref hit))
        {
            SpawnEquippedAttack(hit.point);
            myPlayer.Movement().MoveToLocation(myPlayer.transform.position);
            myPlayer.Combat().SpendMana(manaCost);
        }
        else
        {
            myPlayer.Movement().MoveToLocation(hit.point);
        }
    }

    private bool CanCastFireball(ref RaycastHit hit)
    {
        return myPlayer.Combat().GetMana() >= manaCost && (hit.collider.gameObject.GetComponent<Clickable>() || Input.GetKey(KeyCode.LeftShift));
    }

    protected override void SpawnEquippedAttack(Vector3 location)
    {
        myPlayer.GetAnimator().TriggerAttack();
        myPlayer.transform.LookAt(new Vector3(location.x, myPlayer.transform.position.y, location.z));
        Vector3 spawnPosition = myPlayer.transform.position + myPlayer.transform.forward;
        GameObject newAttack = Instantiate(spawnablePrefab, spawnPosition, Quaternion.identity);
        newAttack.GetComponent<FireballCA>().SetFactionID(myPlayer.GetFactionID());
        newAttack.GetComponent<FireballCA>().SetShootDirection(myPlayer.transform.forward);

        float calculatedDamage = 1 + (2 * skillLevel);
        newAttack.GetComponent<FireballCA>().InitializeDamage(calculatedDamage);
    }
}
