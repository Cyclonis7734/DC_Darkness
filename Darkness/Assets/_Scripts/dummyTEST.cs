using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class dummyTEST : StandaloneInputModule {
    //protected override MouseState GetMousePointerEventData()
    //{
    //    //return 
    //    PointerInputModule.MouseState x = base.GetMousePointerEventData();
    //    var buttonState = x.GetButtonState(PointerEventData.InputButton.Left);
    //    buttonState.eventData.buttonData.position = new Vector2(Screen.width / 2, Screen.height / 2);
    //    x.SetButtonState(PointerEventData.InputButton.Left, StateForMouseButton(0),buttonState.eventData.buttonData);
    //    return x;

    //}

    protected override void ProcessMove(PointerEventData pointerEvent)
    {
        //base.ProcessMove(pointerEvent);
        GameObject newEnterTarget = pointerEvent.pointerCurrentRaycast.gameObject;
        Debug.Log("New enter target is " + newEnterTarget);
        HandlePointerExitAndEnter(pointerEvent, newEnterTarget);

    }



}
