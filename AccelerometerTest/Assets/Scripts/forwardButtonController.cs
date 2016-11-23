using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System;
using Assets.Own_Scripts;

public class forwardButtonController : MonoBehaviour, IPointerDownHandler {

    public void OnPointerDown(PointerEventData eventData) {
        GameController.controller.Move();
    }
}
