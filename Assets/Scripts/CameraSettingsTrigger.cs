using System;
using Cinemachine;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class CameraSettingsTrigger : MonoBehaviour
{
    private Camera m_camera;
    private CinemachineVirtualCamera virtualCamera;
    private CinemachineFramingTransposer framingComposer;

    public bool changeRotation;
    public Quaternion targetRotation;
    public bool changeFieldOfView;
    public float fieldOfView;
    public bool changeDistance;
    public float distance;

    // Start is called before the first frame update
    void Start()
    {
        var cameraObject = GameObject.FindGameObjectWithTag("MainCamera");
        m_camera = cameraObject.GetComponent<Camera>();

        var virtualCameraObject = GameObject.FindGameObjectWithTag("PlayerFollowCamera");
        virtualCamera = virtualCameraObject.GetComponent<CinemachineVirtualCamera>();

        framingComposer = virtualCamera.GetCinemachineComponent<CinemachineFramingTransposer>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (changeRotation)
            {
                virtualCamera.transform.rotation = targetRotation;
            }

            if (changeFieldOfView)
            {
                virtualCamera.m_Lens.FieldOfView = fieldOfView;
            }

            if (changeDistance)
            {
                framingComposer.m_CameraDistance = distance;
            }
        }
    }
}
