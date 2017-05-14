using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndGame : MonoBehaviour {

    //public GameObject gamoEndUI;
    public GameObject gamoPlayer;
    protected RigidbodyFirstPersonController rfps;
    protected bool boolWin = false;
    protected string strEndMsg;

    public void EndTheGame(string strMsg,bool booWin = false)
    {
        boolWin = booWin;
        gamoPlayer.GetComponent<ItemInHandsManager>().txtAM.text = strMsg;
        rfps = gamoPlayer.GetComponent<RigidbodyFirstPersonController>();
        rfps.mouseLook.lockCursor = false;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        StartCoroutine(DramaticPause());
    }
    
    protected IEnumerator DramaticPause()
    {
        yield return new WaitForSeconds(5f);
        rfps.movementSettings.ForwardSpeed = 0;
        rfps.movementSettings.BackwardSpeed = 0;
        rfps.movementSettings.StrafeSpeed = 0;
        if (boolWin) { strEndMsg = "Congratulations!!! You won!"; } else { strEndMsg = ""; }
        gamoPlayer.GetComponent<ItemInHandsManager>().txtAM.text = strEndMsg;
        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene("Main Menu");
    }

}
