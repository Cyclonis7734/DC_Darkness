using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class OverriddenPhysicsRaycaster : PhysicsRaycaster {
    public override void Raycast(PointerEventData eventData, List<RaycastResult> resultAppendList)
    {
        eventData.position = new Vector2(Screen.width / 2, Screen.height / 2);
        base.Raycast(eventData, resultAppendList);
    }
}
