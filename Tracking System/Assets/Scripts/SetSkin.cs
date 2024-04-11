using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetSkin : MonoBehaviour
{
    [SerializeField]
    SkinManager ship;

    // Start is called before the first frame update
    void Start()
    {
        ship.setActivatedSkin(GameManager.GetInstance().GetSkin());
    }
}
