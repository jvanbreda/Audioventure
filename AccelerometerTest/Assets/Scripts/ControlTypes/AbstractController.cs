using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Own_Scripts.ControlTypes {
    public abstract class AbstractController : ScriptableObject {

        protected GameController gameController;
        public Vector3 orientation;
        public Vector3 heading;
        protected int currentCameraAngle;

        public abstract void Move();
        public abstract void UpdateOrientation();
    }
}
