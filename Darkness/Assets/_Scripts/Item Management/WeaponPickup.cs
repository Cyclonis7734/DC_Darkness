using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : PickupItem {


    protected override void PlayerGetsItem()
    {
        bool booDoMsg = false;
        if (!booItemsFound)
        {
            if (strThisItemName.Equals("Sharpened Stick"))
            {
                foreach (GameObject gamo in FindObjectsOfType<GameObject>())
                {
                    Rigidbody rbdDoor;
                    if (gamo.layer == 10 && gamo.name.Contains("Door Main"))
                    {
                        rbdDoor = gamo.GetComponent<Rigidbody>();
                        if (rbdDoor.mass > 5000)
                        {
                            rbdDoor.mass = 5000;
                            booDoMsg = true;
                        }
                        else
                        {
                            rbdDoor.mass = 50;
                            booItemsFound = true;
                        }
                    }
                }
            }
        }

        if (MainBP.HaveItem(this.name))
        {
            StartCoroutine(UpdateText("You already have a " + strThisItemName));
        }
        else
        {
            StartCoroutine(UpdateText("You picked up a " + strThisItemName,2f, true));
            MainBP.NewPickup(this.name, floPUQuantity);
            GetComponent<Renderer>().enabled = false;
            if (booDoMsg)
            {
                StartCoroutine(UpdateText(""));
                StartCoroutine(UpdateText("This place is creepy... I should look for some sort of light source...",10f));
                booDoMsg = false;
            }
        }

    }

    protected IEnumerator UpdateText(string strMsg, float floWaitFor = 2f ,bool booDestroy = false)
    {
        txtAM.text = strMsg;
        yield return new WaitForSeconds(2f);
        txtAM.text = "";
        if (booDestroy) { Destroy(gameObject); }
    }

}
