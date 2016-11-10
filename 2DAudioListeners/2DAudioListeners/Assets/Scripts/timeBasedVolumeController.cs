using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts;
using UnityEngine;
using System.Timers;
using System.Threading;

namespace Assets.Scripts {
    public class timeBasedVolumeController {
        private SoundObject[] SoundObjectsArray;
        private SoundObject[] LastSeenSoundObject;
        Dictionary<SoundObject, float> SoundObjectsDifference;

        System.Threading.Timer timer;
        int counter;
        VRCameraController vrc;
        Thread MainThread;
        
        public timeBasedVolumeController(VRCameraController vrc) {

            SoundObjectsDifference = new Dictionary<SoundObject, float>();
            this.vrc = vrc;
            MainThread = Thread.CurrentThread;
            
            timer = new System.Threading.Timer(new TimerCallback(OnTimedEvent), null, 0, 1000);
            timer.Change(0, 1000); //enable

            counter = 0;
            LastSeenSoundObject = new SoundObject[5];
            SoundObjectsArray = vrc.soundObjects;
        }

        public SoundObject GetClosestSoundSource(float CurrentCameraAngle) {
            SoundObjectsDifference = new Dictionary<SoundObject, float>();
            foreach (SoundObject source in SoundObjectsArray) {
                float AngleDifference = Math.Abs(CurrentCameraAngle - source.angle);
                SoundObjectsDifference.Add(source, AngleDifference);
            }

            var MinimalSoundObject = SoundObjectsDifference.FirstOrDefault(x => x.Value == SoundObjectsDifference.Values.Min()).Key;
            SoundObjectsDifference.Remove(MinimalSoundObject);
            return MinimalSoundObject;
        }

        public void OnTimedEvent(object source) {
            LastSeenSoundObject[counter] = GetClosestSoundSource(vrc.currentCameraAngle);
            counter++;

            Debug.Log(counter);
            if (counter == 5) {
                bool allSame = true;
                for (int i = 0; i < LastSeenSoundObject.Length; i++) {
                    if (!LastSeenSoundObject[0].Equals(LastSeenSoundObject[i])) {   
                        allSame = false;
                        break;
                    }
                }
                if (allSame) {
                    foreach (SoundObject key in SoundObjectsDifference.Keys) {
                        key.SetVolume(key.GetVolume() - 0.5f);
                    }
                }
            }
        }
    }
}
