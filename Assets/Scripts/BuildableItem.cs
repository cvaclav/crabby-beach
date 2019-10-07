using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemMaterial { Wood, Stone, Coral };

[CreateAssetMenu]
public class BuildableItem : ScriptableObject
{
    public Sprite sprite;

    public int endurance;

    public int stage;

    public ItemMaterial material;
}
