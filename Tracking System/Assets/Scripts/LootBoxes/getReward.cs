using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class getReward : MonoBehaviour{

    private void Start()
    {
    }

    private void OnTriggerStay2D(Collider2D collision){
        var cmp = collision.GetComponent<CaseCell>();
        
        if (cmp != null){            
           
            GameManager.GetInstance().setReward(cmp.GetResult());
            gameObject.SetActive(false);

            cmp.transform.parent.GetChild(1).gameObject.SetActive(true);

            var t = cmp.GetComponent<AudioSource>();

            if (t != null)
            {
                t.pitch = Random.Range(2.9f, 3f);
                t.Play();
            }

        }
    }
}
