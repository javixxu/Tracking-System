using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaseScroll : MonoBehaviour{
    [SerializeField]
    private GameObject prefab;

    [SerializeField]
    GameObject getReward;

    public float speedMin=1;
    public float speedMax=3;

    private float speed;
    private bool isCrolling=false;

    private List<CaseCell> cells =new List<CaseCell>();

    public void Scroll(){
        if (isCrolling) return;

        limpiar();
       
        for (int i = 0; i < 500; i++)
            generateCell();
        
        foreach (var cell in cells)
            cell.SetUp();

        isCrolling = true;
        speed = Random.Range(speedMin, speedMax);
    }

    private void Update(){
        if(!isCrolling) return;

        transform.position = Vector2.MoveTowards(transform.position, transform.position + Vector3.left * 100, speed*Time.deltaTime*50);
        if(speed>0)
            speed-=Time.deltaTime;
        else{
            speed= 0;            
            isCrolling = false;
            getReward.SetActive(true);
        }
    }
    public void generateCell(){
        cells.Add(Instantiate(prefab, transform).GetComponentInChildren<CaseCell>());
    }
    public void limpiar(){
        foreach (var cell in cells) Destroy(cell.gameObject.transform.parent.gameObject);
        cells.Clear();
        transform.position = new Vector3(15, 0, 25);
    }
}
