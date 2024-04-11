using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour{

    float timer;
    float myBestTime;
    public Text textTimer;
    public Text textRecord;

    public GameObject newRecord;
    public GameObject oldRecord;
    public Text textNewRecordNum;

    public Guardar guardar;
    GameManager manager;
    // Start is called before the first frame update
    void Start(){
        manager=GameManager.GetInstance();
        guardar.Load();       
        timer = 0.0f;
        
        // guardar.record = -1;
        // guardar.Safe();
        myBestTime = guardar.record;
        actualizarRecord();
    }

    // Update is called once per frame
    void Update(){
        if (manager.isGameOver()) return;
        timer += Time.deltaTime;
        actualizarTimer();
        
    }
    private void OnTriggerEnter(Collider other){
        if (other.gameObject.GetComponent<ShipController>() != null){            
           stopTimer();
        }
    }
    public void actualizarRecord(){
        if (myBestTime != -1)
            textRecord.text = TimeSpan.FromSeconds(myBestTime).ToString(@"mm\:ss\:fff");
        else{
            oldRecord.SetActive(false);
        }
    }
    public void actualizarTimer() {
        textTimer.text = TimeSpan.FromSeconds(timer).ToString(@"mm\:ss\:fff");
    }
    public void stopTimer(){
        if (timer < myBestTime || myBestTime == -1)
        {
            myBestTime = timer;
            guardar.record = myBestTime;
            guardar.Safe();
            actualizarRecord();

            newRecord.SetActive(true);
            textNewRecordNum.text= TimeSpan.FromSeconds(myBestTime).ToString(@"mm\:ss\:fff");
            textTimer.text= "";
            textRecord.text = "";
        }
        timer = 0.0f;
        actualizarTimer();
    }
}
