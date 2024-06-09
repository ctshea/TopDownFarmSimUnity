using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ChoppableArrays
{
    public int maxHp;           //for checking if for regen
    public int healthPoints;
    public Tile originalTile;   //if want to have weaker trees/rocks on hits

    public ChoppableArrays(Tile OGTile, int hp)
    {
        this.originalTile = OGTile;
        this.maxHp = hp;
        this.healthPoints = hp;
    }

    public void subtractHp(int dmg)
    {
        this.healthPoints -= dmg;
    }

    public int getHp()
    {
        return this.healthPoints;
    }
}
