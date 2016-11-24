using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System;
using Assets.Own_Scripts;

public class ClockwiseButtonController : MonoBehaviour, IPointerDownHandler, IPointerUpHandler {

    private bool isHolding = false;

    public void OnPointerDown(PointerEventData eventData) {
        isHolding = true;
    }

    public void OnPointerUp(PointerEventData eventData) {
        isHolding = false;
    }

    void Update() {
        if(isHolding)
            GameController.controller.UpdateHeading("ClockWise");
    }
}
