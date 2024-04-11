using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GachaManager : MonoBehaviour
{
    AllMenuManager_PK allMenuManager;
    void Awake()
    { allMenuManager = GetComponentInParent<AllMenuManager_PK>(); }

    [SerializeField]
    GameObject scroll;
 
    public void Scroll(InputAction.CallbackContext context){
       
        scroll.GetComponent<CaseScroll>().Scroll();
        //else if (context.canceled)
    }
    public void Back(InputAction.CallbackContext context){
        allMenuManager.BackButtonHorizontal();
        scroll.GetComponent<CaseScroll>().limpiar();

    }
}
