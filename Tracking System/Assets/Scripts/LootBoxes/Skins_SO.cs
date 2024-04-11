using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

[CreateAssetMenu]
public class Skins_SO : ScriptableObject
{
    public enum Rarities
    {
        Common,
        Rare,
        Epic,
        Legendary,
    }


    [Serializable]
    public struct SkinInfo
    {
        public GameObject skin;
        public Sprite image;
        public Rarities rarity;
        // [HideInInspector]
        public int index;
    }

    public List<SkinInfo> allSkins;
    public List<int> chances;
    public List<Color> raritiesColors;
    readonly List<SkinInfo>[] skinByRarity = new List<SkinInfo>[4];

    public List<SkinInfo> GetSkinByRarity(Rarities rarity)
    {
        if (skinByRarity[(int)rarity] == null) {
            skinByRarity[(int)rarity] = allSkins.FindAll((a) => a.rarity == rarity);
        }
        return skinByRarity[(int)rarity];
    }

    public SkinInfo this[int a]
    {
        get => allSkins[a];
    }
}
