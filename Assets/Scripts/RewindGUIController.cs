using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RewindGUIController : MonoBehaviour
{
    
    public List<SpriteData> sprites;
    
    [Serializable]
    public class SpriteData
    {
        public Image sprite;
        public int rewinds;
    }

    private int currentActive = 0;
    
    
    private Dictionary<int, Image> spriteDictionary;
    private void Awake()
    {
        spriteDictionary = new Dictionary<int, Image>();
        foreach (SpriteData spriteData in sprites)
        {
            if (spriteData.rewinds == 0) continue;
            spriteData.sprite.enabled = false;
            spriteDictionary.Add(spriteData.rewinds, spriteData.sprite);
        }
    }

    public void DisplayRewind(int quantity)
    {
        if (currentActive != 0)
        {
            spriteDictionary[currentActive].enabled = false;
        }
        
        currentActive = quantity;
        if (currentActive != 0)
        {
            spriteDictionary[currentActive].enabled = true;
        }
        
        
    }
    
}
