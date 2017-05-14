using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsPickup : PickupItem {


    protected override void PlayerGetsItem()
    {

        //if (MainBP.HaveItem(this.name))
        //{
        //    StartCoroutine(UpdateText("You already have a " + strThisItemName));
        //}
        //else
        //{
        //    StartCoroutine(UpdateText("You picked up a " + strThisItemName, true));
        //    MainBP.NewPickup(this.name, floPUQuantity);
        //    //GetComponent<Renderer>().enabled = false;
        //    gameObject.SetActive(false);
        //}



    }

    //protected IEnumerator UpdateText(string strMsg, bool booDestroy = false)
    //{
    //    txtAM.text = strMsg;
    //    yield return new WaitForSeconds(2f);
    //    txtAM.text = "";
    //    if (booDestroy) { Destroy(gameObject); }
    //}


}
