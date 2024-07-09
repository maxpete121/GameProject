using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionManager : MonoBehaviour
{
    public static SelectionManager Instance {get; set;}

    public List<GameObject> allUnitsList = new List<GameObject>();
    public List<GameObject> unitsSelected = new List<GameObject>();
    public LayerMask clickableUnit;
    public LayerMask ground;
    public LayerMask attackable;
    public GameObject groundMarker;
    private Camera cam;
    UnitMovement unitMovement;
    private void Awake(){
        if(Instance != null && Instance != this){
            Destroy(gameObject);
        }
        else{
            Instance = this;
        }
    }

    private void Start(){
        cam = Camera.main;
    }

    private void Update(){
        if(Input.GetMouseButtonDown(0)){
            RaycastHit hit;
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);

            if(Physics.Raycast(ray, out hit, Mathf.Infinity, clickableUnit)){
                if(Input.GetKey(KeyCode.LeftShift)){
                    MultiSelect(hit.collider.gameObject);
                }
                else{
                SelectByClick(hit.collider.gameObject);
                }
            }
            else{
              if(!Input.GetKey(KeyCode.LeftShift)){
                DeselectAll();
              }
        }
        }      


        if(Input.GetMouseButtonDown(1) && unitsSelected.Count > 0){
            RaycastHit hit;
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);

            if(Physics.Raycast(ray, out hit, Mathf.Infinity, ground)){
                groundMarker.transform.position = hit.point;
                groundMarker.SetActive(false);
                groundMarker.SetActive(true);
            }
        } 


        if(unitsSelected.Count > 0 && HasOffensiveUnit(unitsSelected)){
          RaycastHit hit;
          Ray ray = cam.ScreenPointToRay(Input.mousePosition);

        if(Physics.Raycast(ray, out hit, Mathf.Infinity, attackable)){
            if(Input.GetMouseButtonDown(1)){
                Transform target = hit.transform;
                foreach(GameObject unit in unitsSelected){
                    if(unit.GetComponent<AttackController>()){
                        unit.GetComponent<AttackController>().targetToAttack = target;
                    }
                }
            }
        }
        }       
    }

    private void MultiSelect(GameObject unit)
    {
        if(unitsSelected.Contains(unit) == false){
            unitsSelected.Add(unit);
            TriggerSelectIndicator(unit, true);
            EnableUnitMovement(unit, true);
        }
        else{
            TriggerSelectIndicator(unit, false);
            EnableUnitMovement(unit, false);
            unitsSelected.Remove(unit);
        }
    }

    private void SelectByClick(GameObject unit){
        DeselectAll();

        unitsSelected.Add(unit);
        TriggerSelectIndicator(unit, true);
        EnableUnitMovement(unit, true);
    }

    public void DeselectAll(){
        foreach(var unit in unitsSelected){
            TriggerSelectIndicator(unit, false);
            EnableUnitMovement(unit, false);
            groundMarker.SetActive(false);
        }
        
        unitsSelected.Clear();
    }

    private void EnableUnitMovement(GameObject unit, bool movement){
        unit.GetComponent<UnitMovement>().enabled = movement;
    }

    private void TriggerSelectIndicator(GameObject unit, bool isVisible){
        unit.transform.GetChild(0).gameObject.SetActive(isVisible);
    }

    internal void DragSelect(GameObject unit)
    {
        if(unitsSelected.Contains(unit) == false){
            unitsSelected.Add(unit);
            TriggerSelectIndicator(unit, true);
            EnableUnitMovement(unit, true);
        }
    }
    private bool HasOffensiveUnit(List<GameObject> unitsSelected){
        foreach(GameObject unit in unitsSelected){
            if(unit.GetComponent<AttackController>()){
                return true;
            }
        }
        return false;
    }
}
