using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitsController : MonoBehaviour
{
    [SerializeField]
    float invinTime = 0.1f;
    [SerializeField]
    float invinEffectTime = 0.05f;

    [SerializeField]
    CameraShake cam;

    GameObject shipGfx=null;

    float cont;
    float effectCont = 0f;
    //Shield
    Shield shield;
    ShipController shipController;
    GameManager gameManager;

    private void Start(){
        shield = GetComponentInChildren<Shield>();
        shipController = GetComponentInChildren<ShipController>();
        gameManager=GameManager.GetInstance();
        cont = invinTime + 0.5f;
    }   
    private void Update()
    {
        if (cont < invinTime)
        {
            effectCont += Time.deltaTime;
            if (effectCont > invinEffectTime)
            {
                shipGfx.SetActive(!shipGfx.activeSelf);
                effectCont = 0;
            }

            cont += Time.deltaTime;
            if (cont > invinTime)
            {
                shipGfx.SetActive(true);
                effectCont = 0;
            }
        }

    }

    public bool OnHit()
    {
        if (cont > invinTime)
        {
            // Comportamiento de recibir un golpe
            cam.Shake(1, 1f);
            if (shield.getActive()) {
                shield.setActive(false);
                Debug.Log("SHIELD DOWN");
            }
            else{
                Debug.Log("MUERTO");
                gameManager.setGameOver(true);
                shipController.setDeath(true);
            }

            cont = 0;

            return true;
        }
        return false;
    }
    public void setGfx(GameObject x){
        shipGfx = x;
    }
}
