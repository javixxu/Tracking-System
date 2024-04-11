using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealth : MonoBehaviour
{
    [SerializeField]
    GameObject explosion;

    [SerializeField]
    GameObject texto;

    GameManager gameManager;
    Timer timer;
    bool killed = false;
    private void Start()
    {
        gameManager = GameManager.GetInstance();
        timer= GetComponent<Timer>();
    }
    // Update is called once per frame
    void Update()
    {
        if (gameManager.GetEyeIndex() >= gameManager.getEyes().Count && !killed)
            Explosion();
    }

    private void Explosion()
    {
        killed = true;
        this.gameObject.GetComponentInChildren<MeshRenderer>().enabled = false;
        Instantiate(explosion, this.gameObject.GetComponentInChildren<MeshRenderer>().transform);
        //GameObject.Find("Ship").GetComponent<ShipController>().enabled = false;
        texto.SetActive(true);
        timer.stopTimer();
        timer.enabled= false;
        Invoke(nameof(Kill), 4.0f);
    }

    private void Kill()
    {
        this.gameObject.SetActive(false);
        GameManager.GetInstance().ChangeScene("MainMenu_Scene");
    }
}
