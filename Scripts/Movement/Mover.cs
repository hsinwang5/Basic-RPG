using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using RPG.Combat;
using RPG.Core;

namespace RPG.Movement 
{
    public class Mover : MonoBehaviour
    {
        [SerializeField] Transform target;

        Animator animator;
        NavMeshAgent navMeshAgent;
        Fighter fighter;

        Ray lastRay;
        Vector3 mousePos;
        PlayerInputActions playerInputActions;
        Vector3 playerCurrentVelocity;

        private void Awake() 
        {
            playerInputActions = new PlayerInputActions();
            playerInputActions.Player.Enable(); 
            animator = GetComponent<Animator>();
            navMeshAgent = GetComponent<NavMeshAgent>();
            fighter = GetComponent<Fighter>();
        }

        void Start()
        {
        }

        void Update()
        {
            //MoveToCursor();
            UpdateAnimation();
            
        }

        public void MoveAction(Vector3 destination)
        {
            GetComponent<ActionScheduler>().StartAction(this);
            fighter.CancelAttack();
            MoveTo(destination);
        }

        public void MoveTo(Vector3 destination)
        {
            navMeshAgent.destination = destination;
            navMeshAgent.isStopped = false;
        }

        public void StopMovement()
        {
            navMeshAgent.isStopped = true;
        }

        void UpdateAnimation()
        {
            playerCurrentVelocity = GetComponent<NavMeshAgent>().velocity;
            playerCurrentVelocity = transform.InverseTransformDirection(playerCurrentVelocity);
            animator.SetFloat("ForwardMotion", playerCurrentVelocity.z);
        }

        // public void MoveToCursor(InputAction.CallbackContext context)
        // {
        //     Debug.Log(context.canceled);
        //     mousePos = Mouse.current.position.ReadValue();
        //     Ray ray = Camera.main.ScreenPointToRay(mousePos);
        //     RaycastHit hit;
        //     bool hasHit = Physics.Raycast(ray, out hit);
        //     if (hasHit)
        //     {
        //         navMesh.destination = hit.point;
        //     }
        // }

        // void DrawTestRay()
        // {
        //     if (mousePos != null)
        //     {
        //         lastRay = Camera.main.ScreenPointToRay(mousePos);
        //         Debug.DrawRay(lastRay.origin, lastRay.direction * 100); 
        //     }
        // }
    }
}
