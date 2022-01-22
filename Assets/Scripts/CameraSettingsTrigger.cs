using System;
using Cinemachine;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class CameraSettingsTrigger : MonoBehaviour
{
    private Camera camera;
    private CinemachineVirtualCamera virtualCamera;
    private CinemachineFramingTransposer framingComposer;

    public Quaternion targetRotation;
    public float fieldOfView;
    public float distance;

    // Start is called before the first frame update
    void Start() {
        var cameraObject = GameObject.FindGameObjectWithTag("MainCamera");
        camera = cameraObject.GetComponent<Camera>();

        var virtualCameraObject = GameObject.FindGameObjectWithTag("PlayerFollowCamera");
        virtualCamera = virtualCameraObject.GetComponent<CinemachineVirtualCamera>();

        framingComposer = virtualCamera.GetCinemachineComponent<CinemachineFramingTransposer>();
    }

    void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Player") {
            virtualCamera.transform.rotation = targetRotation;
            virtualCamera.m_Lens.FieldOfView = fieldOfView;
            framingComposer.m_CameraDistance = distance;
        }
    }
}
