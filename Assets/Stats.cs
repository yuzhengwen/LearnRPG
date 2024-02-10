using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace YuzuValentine
{
    public class Stats : MonoBehaviour
    {
        public float health;
        public float damage;
        public float atkSpd;

        public void TakeDamage(float damage)
        {
            health -= damage;
            if (health < 0)
            {
                health = 0;
                Destroy(gameObject);
            }
        }
    }
}
