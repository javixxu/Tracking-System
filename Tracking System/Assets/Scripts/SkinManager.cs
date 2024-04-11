using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinManager : MonoBehaviour
{
    [SerializeField]
    Skins_SO skins;

    [SerializeField]
    Transform skinParent;

    GameObject skinGFX;

    int skin;
    public void setActivatedSkin(int skin){
        this.skin = skin;

        DeactivateAllSkins();

        skinGFX = Instantiate(skins[skin].skin, skinParent);
        GetComponent<HitsController>().setGfx(skinGFX);
    }
    public GameObject skinActive() { return skinGFX; }


    void DeactivateAllSkins()
    {
        if (skinGFX == null) return;
        var parent = skinGFX.transform.parent;

        for (int i = 1; i < parent.childCount; i++)
        {
            Destroy(parent.GetChild(i).gameObject);
        }
    }
}
