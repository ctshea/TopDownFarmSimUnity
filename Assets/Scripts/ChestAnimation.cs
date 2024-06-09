using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ChestAnimation : MonoBehaviour
{
    public getTileInfo getTile;   
    public Tilemap chestTM;

    Animator anim;

    AnimatorStateInfo animStateInfo;    //to check if animation is currently running

    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
        
    }
    void Update()
    {
        animStateInfo = anim.GetCurrentAnimatorStateInfo(0);
        if (getTile.GetInRange(chestTM, false))
        {
            if (Input.GetMouseButtonDown(0) && animStateInfo.IsName("ChestClosed"))     // and mouseposition is on a grid with a chest
            {

                anim.SetTrigger("Active");
            }
            else if (Input.GetMouseButtonDown(1))
            {

                anim.SetTrigger("NotActive");
            }

        }
    }
   
}
