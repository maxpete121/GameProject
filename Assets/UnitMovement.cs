using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.AI;

public class UnitMovement : MonoBehaviour
{
    // Start is called before the first frame update
    Camera cam;
    NavMeshAgent agent;
    public LayerMask ground;

    private void Start(){
        cam = Camera.main;
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update(){
        if(Input.GetMouseButtonDown(1)){
            RaycastHit hit;
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);

            if(Physics.Raycast(ray, out hit, Mathf.Infinity, ground)){
                agent.SetDestination(hit.point);
            }
        }
    }
}