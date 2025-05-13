using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSounds : MonoBehaviour
{

    public event EventHandler<EventArgs> OnFootstep;
    
    float footstepTimer = 0.3f;
    float footstepTime = 0.3f;

    void Update() {
        if(Player.Instance.IsMoving()) {
            footstepTimer+=Time.deltaTime;
            if(footstepTimer>=footstepTime) {
                footstepTimer=0;
                SoundManager.Instance.PlayFootstepSound(this.transform.position);
                OnFootstep?.Invoke(this, EventArgs.Empty);
            }
        }
    }
}
