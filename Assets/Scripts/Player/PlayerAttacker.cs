using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttacker : MonoBehaviour
{
  PlayerController player;
  Transform myTransform;

  public Transform weapon;
  public float attackRange = 1f;
  public float cooldown = 1f;
  public bool attacking;

  void Awake()
  {
    player = GetComponent<PlayerController>();
    myTransform = transform;
  }

  void Start()
  {
    attacking = false;
  }

  public void Tick()
  {
    // if the attack input is pressed but no attack is happening, start an attack
    if (player.attacking && !attacking)
    {
      Attack();
    }
  }

  public void Attack()
  {
    if (!attacking) StartCoroutine(DoAttack());
  }

  IEnumerator DoAttack()
  {
    attacking = true;
    weapon.Translate(new Vector3(-0.5f,0.3f,0.5f));
    weapon.Rotate(new Vector3(45f,0f,0f));
    RaycastHit hitInfo;
    if (Physics.Raycast(myTransform.position, myTransform.forward, out hitInfo, attackRange, (1<<11), QueryTriggerInteraction.Ignore))
    {
      if (hitInfo.collider.CompareTag("NPC"))
      {
        hitInfo.collider.GetComponent<NPCAgent>().ReceiveAttack();
      }
    }
    yield return new WaitForSeconds(cooldown);
    attacking = false;
    yield return null;
  }
}
