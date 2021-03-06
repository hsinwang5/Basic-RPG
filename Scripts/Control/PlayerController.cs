using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Movement;
using UnityEngine.InputSystem;
using RPG.Combat;
using System;

namespace RPG.Control 
{
    public class PlayerController : MonoBehaviour
    {
        Mover mover;
        Animator animator;
        Fighter fighter;
        PlayerInputActions playerInputActions;

        Vector3 playerCurrentVelocity;

        private void Awake() 
        {
            //input system
            playerInputActions = new PlayerInputActions();
            playerInputActions.Player.Enable(); 
            //playerInputActions.Player.ClickToMove.performed += InitiateCombat;
            //find components/objects
            mover = FindObjectOfType<Mover>();  
            animator = GetComponent<Animator>();  
            fighter = FindObjectOfType<Fighter>();
        }

        void Start()
        {
            
        }

        void Update()
        {
            if (InteractWithCombat()) return;
            if (MoveToCursor()) return;
        }

        bool InteractWithCombat()
        {
            //if (context.performed)
            RaycastHit[] hits = Physics.RaycastAll(GetMouseRay());
            foreach (RaycastHit hit in hits)
            {
                CombatTarget target = hit.transform.GetComponent<CombatTarget>();
                if (target == null) continue;

                if (playerInputActions.Player.ClickToMove.triggered)
                {
                    fighter.Attack(target);
                }
                return true;
            }
            return false;

            
        }

        bool MoveToCursor()
        {   
            RaycastHit hit;
            bool hasHit = Physics.Raycast(GetMouseRay(), out hit);
            if (hasHit)
            {
                if (MouseLeftActive())
                {   
                    mover.MoveAction(hit.point);
                }
                return true;
            }
            return false;
        }

        private Ray GetMouseRay()
        {
            Vector3 mousePos = Mouse.current.position.ReadValue();
            Ray ray = Camera.main.ScreenPointToRay(mousePos);
            return ray;
        }

        bool MouseLeftActive()
        {
            return playerInputActions.Player.ClickToMove.ReadValue<float>() == 1;
        }
            
    }
}
