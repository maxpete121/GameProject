using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Unit : MonoBehaviour
{

    void Start()
    {
        SelectionManager.Instance.allUnitsList.Add(gameObject);
    }

    private void OnDestroy(){
        SelectionManager.Instance.allUnitsList.Remove(gameObject);
    }

}
