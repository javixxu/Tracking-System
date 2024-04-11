using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour{
    Pinchos pinchos;
    private void Awake() 
    { 
        pinchos=GetComponent<Pinchos>();
    }
    private void OnTriggerStay(Collider other)
    {
        var player = other.GetComponent<HitsController>();
       
        if (player){
            if (pinchos == null || (pinchos != null && !other.GetComponent<ShipController>().jumping)){
                if (player.OnHit())
                {
                    if (transform.parent != null)
                        Destroy(transform.parent.gameObject);
                    else
                        Destroy(this.gameObject);
                }
            }
        }
    }
}
