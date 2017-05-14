using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PickupItem : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] public string strThisItemName;
    protected GameObject gamoPlayer;
    protected Backpack MainBP;
    protected ItemInHandsManager IIHM;
    [SerializeField] protected float floPUQuantity;
    [SerializeField] protected ParticleSystem psysMe;
    protected int intCount;
    public bool booIsSparkling { get; set; }
    protected GameObject gamoActionMsg;
    protected float floDistToPlayer;
    protected float floMaxDist4PU = 2.5f;
    protected Text txtAM;
    protected bool booItemsFound = false;

    private void Awake()
    {
        foreach(GameObject gamo in FindObjectsOfType<GameObject>())
        {
            string strNm = gamo.name;
            if (strNm == "RigidBodyFPSController") { gamoPlayer = gamo; MainBP = gamo.GetComponent<Backpack>().Instance(); IIHM = gamo.GetComponent<ItemInHandsManager>(); }
            else if (strNm == "Action Message") { gamoActionMsg = gamo; txtAM = gamoActionMsg.GetComponent<Text>(); }
        }
        booIsSparkling = false;
    }//End Awake Method


    protected virtual void PlayerGetsItem()
    {
        MainBP.NewPickup(this.name, floPUQuantity);
    }

    //Event System Handling
    //-------------------------------------------------------------------------------------------------
    //protected void OnMouseEnter() //On Mouse hover over item has started
    //{
    //    StartSparkling();
    //    //Debug.Log("gamoActionMsg = " + gamoActionMsg.name + " -text: " + gamoActionMsg.GetComponent<Text>().text);
    //    if(floDistToPlayer < floMaxDist4PU) { txtAM.text = "Left Click to pickup " + this.name; IIHM.booCanLeftClick = false; } else { txtAM.text = ""; }
    //} //END: On Mouse hover over item has started

    //protected void OnMouseDown() { if (floDistToPlayer < floMaxDist4PU) { PlayerGetsItem(); } } //On Mouse clicked while over item

    //protected void OnMouseExit() { StopSparkling(); txtAM.text = ""; IIHM.booCanLeftClick = true; } //On Mouse hover over item has stopped

    protected void OnMouseOver() {
        floDistToPlayer = Vector3.Distance(transform.position, gamoPlayer.transform.position);
        //Debug.Log("Dist to Player: " + floDistToPlayer.ToString());
    } //During Mouse hover over item

    protected virtual void StartSparkling() { psysMe.Play(); booIsSparkling = true; }

    protected virtual void StopSparkling() { StartCoroutine(EndSparkling()); booIsSparkling = false; }

    protected IEnumerator EndSparkling()
    {
        yield return new WaitForSeconds(4);
        psysMe.Stop();
    }

    public void OnPointerClick(PointerEventData eventData)
    {

    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        StartSparkling();
        //Debug.Log("gamoActionMsg = " + gamoActionMsg.name + " -text: " + gamoActionMsg.GetComponent<Text>().text);
        if (floDistToPlayer < floMaxDist4PU) { txtAM.text = "Left Click to pickup " + this.name; IIHM.booCanLeftClick = false; } else { txtAM.text = ""; }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        StopSparkling(); txtAM.text = ""; IIHM.booCanLeftClick = true;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (floDistToPlayer < floMaxDist4PU) { PlayerGetsItem(); }
    }

    public void OnPointerUp(PointerEventData eventData)
    {

    }
    //-------------------------------------------------------------------------------------------------




}
