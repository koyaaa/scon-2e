using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using System.Collections.Generic;


public class Hunter : MonoBehaviour
{
    public OnSearchView onSearch;   //OnSearchViewスクリプトを使用する
   

    public bool LostFlg = false;
    public bool IsLostFlg = false;

    public float lostime = 10.0f;


    private float LostTime;

    void Update()
    {
        HunterCount();
    }

    void HunterCount()
    {
        
        if (onSearch.WANING == true)
        {
            if(LostFlg == false)
            {
                LostTime = Time.time;
                LostFlg = true;
            }

            if (lostime < Time.time - LostTime)
            {
                //Debug.Log(" " + LostTime);
                Debug.Log("見失った");
                onSearch.WANING = false;
                IsLostFlg = true;
              
            }
        }
     }


    
}
