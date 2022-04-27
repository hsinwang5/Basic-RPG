using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Movement;
using RPG.Core;

namespace RPG.Combat 
{
    public class Fighter : MonoBehaviour, IAction
    {
        [SerializeField] float weaponRange = 2;
        [SerializeField] float attackDelay = 1f;
        [SerializeField] float weaponDamage = 20f;
        Transform target;

        float timeSinceLastAttack = 0;
        float distance;

        void Start()
        {
            
        }

        void Update()
        {
            timeSinceLastAttack += Time.deltaTime;
            if (target != null)
            {
                distance = Vector3.Distance(transform.position, target.position);
                if (distance > weaponRange)
                {
                    GetComponent<Mover>().MoveTo(target.position);
                }
                else 
                {
                    GetComponent<Mover>().Cancel();
                    AttackBehaviour();
                }
            }
        }

        void AttackBehaviour() 
        {
            if (timeSinceLastAttack > attackDelay)
            {
                //this will trigger the Hit() event
                GetComponent<Animator>().SetTrigger("Attack");
                timeSinceLastAttack = 0;
            }
        }

        public void Attack(CombatTarget combatTarget) 
        {
            GetComponent<ActionScheduler>().StartAction(this);
            target = combatTarget.transform;
        }

        public void Cancel()
        {
            target = null;
        }

        //called during animation event
        void Hit() 
        {
            //called during attack so will always have a target
            Health targetHealth = target.GetComponent<Health>(); 
            targetHealth.TakeDamage(weaponDamage);
        }
    }
}
