using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts {
    public class TimeChecker : MonoBehaviour {
        private SoundObject[] SoundObjectsArray;
        const float MIN_TIME_LOOK = 5;
        const float MAX_RANGE = 10;
        SoundObject CurrentLookedAtGO = null;
        double currentTimeLookedAt = 0;
        VRCameraController vrc;
        bool executed;
        public static bool audioMuted;

        public void Init(VRCameraController vrc) {
            this.vrc = vrc;
            SoundObjectsArray = vrc.soundObjects;
            executed = false;
            audioMuted = false;
        }

        public void Update() {
            SoundObject current = GetLookedAtObject(vrc.currentCameraAngle);
            Debug.Log("Test");
            if (current != CurrentLookedAtGO) {
                currentTimeLookedAt = 0;
                executed = false;
                audioMuted = false;
                // Reset de waarde omdat je er niet meer naar kijkt;
                CurrentLookedAtGO = current;
            } else {
                currentTimeLookedAt += Time.deltaTime;
            }

            if (currentTimeLookedAt > MIN_TIME_LOOK) {
                audioMuted = true;
                // TODO: Als de currentTimeLookedAt groter is dan 5; voer deze functie uit;
                if (!executed) {
                    foreach (SoundObject source in SoundObjectsArray) {
                        if (!source.Equals(current)) {
                            //StartCoroutine(AudioFadeOut.FadeOut(source.audioSource, 5f));   
                            source.SetVolume(0.1f);
                        }
                    }
                    executed = true;
                }
            }
        }

        private SoundObject GetLookedAtObject(float CurrentCameraAngle) {
            foreach (SoundObject source in SoundObjectsArray) {
                float AngleDifference = Math.Abs(CurrentCameraAngle - source.angle);
                if (AngleDifference < MAX_RANGE) {
                    return source;
                }
            }
            return null;
        }
    }
}
