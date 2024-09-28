// Djaleen Malabonga
// Student #3128901
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

// stabilizes the camera
public class RoundCameraPos : CinemachineExtension
{
    [SerializeField] private float _ppu = 32; // pixels per unit
    protected override void PostPipelineStageCallback(CinemachineVirtualCameraBase vcam, CinemachineCore.Stage stage, ref CameraState state, float deltaTime)
    {
        if (stage == CinemachineCore.Stage.Body) {
            Vector3 pos = state.FinalPosition;
            Vector3 pos2 = new Vector3(Round(pos.x), Round(pos.y), pos.z);
            state.PositionCorrection += pos2 - pos;
            
        }
    }

    private float Round(float x) {
        return Mathf.Round(x * _ppu) / _ppu;
    }
}
