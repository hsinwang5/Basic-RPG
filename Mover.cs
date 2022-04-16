using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class Mover : MonoBehaviour
{
    [SerializeField] Transform target;

    Ray lastRay;
    Vector3 mousePos;
    NavMeshAgent navMesh;
    PlayerInputActions playerInputActions;

    private void Awake() 
    {
        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable(); 
    }

    // Start is called before the first frame update
    void Start()
    {
        navMesh = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        MoveToCursor();
    }

    // private void OnFire() 
    // {
    //     mousePos = Mouse.current.position.ReadValue();
    //     MoveToCursor();
    // }

    void MoveToCursor()
    {
        if (playerInputActions.Player.ClickToMove.ReadValue<float>() == 1)
        {
            mousePos = Mouse.current.position.ReadValue();
            Ray ray = Camera.main.ScreenPointToRay(mousePos);
            RaycastHit hit;
            bool hasHit = Physics.Raycast(ray, out hit);
            if (hasHit)
            {
                navMesh.destination = hit.point;
            }
        }
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
