using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace YuzuValentine
{
    [RequireComponent(typeof(Movement))]
    [RequireComponent(typeof(Stats))]
    public class MeleeCombat : MonoBehaviour
    {
        private Movement movement;
        private Stats stats;
        private Animator anim;

        private float nextAtk;
        private float atkInterval;

        private GameObject targetEnemy;
        void Awake()
        {
            movement = GetComponent<Movement>();
            stats = GetComponent<Stats>();
            anim = GetComponent<Animator>();
        }

        // Update is called once per frame
        void Update()
        {
            atkInterval = stats.atkSpd / ((500 + stats.atkSpd) * 0.01f);
            targetEnemy = movement.target;

            if (targetEnemy != null && !anim.GetBool("isAttacking") && Time.time > nextAtk)
            {
                if (InAutoRange())
                {
                    StartCoroutine(MeleeAttackInterval());
                }
            }
        }
        private IEnumerator MeleeAttackInterval()
        {
            anim.SetBool("isAttacking", true);
            yield return new WaitForSeconds(atkInterval);

            // allow animation cancelling? have to check again
            if (targetEnemy == null)
            {
                anim.SetBool("isAttacking", false);
            }
        }
        private bool InAutoRange()
        {
            if (targetEnemy == null) return false;
            return Vector3.Distance(targetEnemy.transform.position, transform.position) <= movement.stoppingDist;
        }
        public void MeleeAttack()
        {
            nextAtk = Time.time + atkInterval;
            Debug.Log("Auto attack");
            anim.SetBool("isAttacking", false);
        }
    }
}
