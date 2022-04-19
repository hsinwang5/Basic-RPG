using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Movement;

namespace RPG.Combat 
{
    public class Fighter : MonoBehaviour
    {
        [SerializeField] float weaponRange = 2;
        Transform target;

        float distance;

        void Start()
        {
            
        }

        void Update()
        {
            if (target != null)
            {
                distance = Vector3.Distance(transform.position, target.position);
                if (distance > weaponRange)
                {
                    GetComponent<Mover>().MoveTo(target.position);
                }
                else 
                {
                    GetComponent<Mover>().StopMovement();
                }
            }
        }

        public void Attack(CombatTarget combatTarget) 
        {
            target = combatTarget.transform;
        }

        public void CancelAttack()
        {
            target = null;
        }
    }
}
