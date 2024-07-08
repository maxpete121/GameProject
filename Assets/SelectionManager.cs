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
    public GameObject groundMarker;
    private Camera cam;
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
    }

    private void MultiSelect(GameObject gameObject)
    {
        throw new NotImplementedException();
    }

    private void SelectByClick(GameObject unit){
        DeselectAll();

        unitsSelected.Add(unit);
        EnableUnitMovement(unit, true);
    }

    private void DeselectAll(){
        foreach(var unit in unitsSelected){
            EnableUnitMovement(unit, false);
        }
        
        unitsSelected.Clear();
    }

    private void EnableUnitMovement(GameObject unit, bool movement){
        unit.GetComponent<UnitMovement>().enabled = movement;
    }
}
