using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class PlayerSkin : ScriptableObject
{
    public string pname;
    public float speed;
    public int maxhp;
    public int attack;
    public float scale;
    public Sprite img;
}
