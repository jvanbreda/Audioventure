using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Own_Scripts.ControlTypes {
    public abstract class AbstractController : MonoBehaviour {

        protected GameController gameController;

        public abstract void Move();
        public abstract void UpdateHeading();
    }
}
