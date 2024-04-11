using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CaseCell : MonoBehaviour
{

    [SerializeField]
    Skins_SO skins;

    int result;

    public void SetUp(){
        Skins_SO.Rarities index = Randomize();

        var skinsInRarity = skins.GetSkinByRarity(index);

        int id = Random.Range(0, skinsInRarity.Count);
        GetComponent<Image>().sprite = skinsInRarity[id].image;
        transform.parent.GetComponent<Image>().color= skins.raritiesColors[(int)index];

        result = skinsInRarity[id].index;
    }

    private Skins_SO.Rarities Randomize(){
        int ind = 0;
        int rand = Random.Range(0, 101);
        for (int i = 0; i < skins.chances.Count; i++){
            if (rand <= skins.chances[i]) { return (Skins_SO.Rarities)i; }
            ind++;
        }
        return (Skins_SO.Rarities)ind;
    }  
    
    public int GetResult()
    {
        return result;
    }
}
