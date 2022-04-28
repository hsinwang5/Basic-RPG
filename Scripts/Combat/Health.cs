using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Combat
{
    public class Health : MonoBehaviour
    {
        [SerializeField] float health = 100f;

        bool isDead = false;

        public void TakeDamage(float damage)
        {
            health = Mathf.Max(health - damage, 0); //returns largest of 2 or more values
            if (health == 0 && !isDead) 
            {
                GetComponent<Animator>().SetTrigger("Die");
                isDead = true;
            }
            print(health);
        }
    }
}
