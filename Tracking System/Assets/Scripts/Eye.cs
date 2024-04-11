using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Eye : MonoBehaviour
{
    int id;
    [SerializeField]
    GameObject explosion;
    [SerializeField]
    GameObject explosion_2;
    [SerializeField]
    GameObject boss;

    [SerializeField]
    Material functionalEye;
    [SerializeField]
    Material brokenEye;

    [SerializeField]
    MeshRenderer[] breakTheseParts;

    ShipController ship;

    bool golpeado = false;

    Vector3 lookTarget;

    [SerializeField]
    bool lookPlayer;

    [SerializeField]
    bool onMainMenu;

    private void Awake()
    {
        id = GameManager.GetInstance().getEyes().Count;
        GameManager.GetInstance().getEyes().Add(this);

        ship = FindObjectOfType<ShipController>();

        // Fix 
        //foreach (MeshRenderer item in breakTheseParts)
        //    item.material = functionalEye;


        if (!onMainMenu)
            lookTarget = ship.transform.position;
        else lookTarget = new Vector3(0, 0, 0);

        Invoke("ChangeTarget", 1);
    }

    void ChangeTarget()
    {
        int num = 200;
        float x = Random.Range(-num, num);
        float y = Random.Range(-num, num);
        float z = Random.Range(-num, num);

        if (!onMainMenu)
            lookTarget = ship.transform.position + new Vector3(x, y, z);
        else
            lookTarget = new Vector3(x, y, z);

        Invoke("ChangeTarget", Random.Range(.2f, 1f));
    }

    private void Update()
    {
        if (!golpeado)
        {
            if (lookPlayer)
                transform.LookAt(ship.transform.position);
            else
            {
                Quaternion currentRotation = transform.rotation;
                transform.LookAt(lookTarget);
                Quaternion targetRotation = transform.rotation;

                transform.rotation = Quaternion.Slerp(currentRotation, targetRotation, Time.deltaTime * 3);
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Missile misil = collision.gameObject.GetComponent<Missile>();
        if (misil != null && id == GameManager.GetInstance().GetEyeIndex())
        {
            GameManager.GetInstance().IncreaseEyeIndex();
            Instantiate(explosion, transform.position, Quaternion.identity, boss.transform);
            Instantiate(explosion_2, transform.position, Quaternion.identity, boss.transform);
            misil.Explode();
            //this.gameObject.SetActive(false);

            golpeado = true;
            foreach (MeshRenderer item in breakTheseParts)
                item.material = brokenEye;

            Camera.main.GetComponent<CameraShake>().Shake(1f, 1);

            AudioManager_PK.GetInstance().Play("Explosion", 1);//UnityEngine.Random.Range(.9f, 1.1f));
        }

    }
}
