using Cinemachine;
using UnityEngine;

public class VirtualCameraControl : MonoBehaviour
{
    [Header("Cineamchine Fields")]
    [SerializeField] private CinemachineVirtualCamera virtualCamera;
    [SerializeField] private CinemachineBrain cinemachineBrain;
    [Header("Automaticaly Filled")]
    [SerializeField] private CinemachineFramingTransposer cinemachineFraming;
    [SerializeField] private CinemachinePOV POV;

    [Header("Parameters")]
    [SerializeField] private float lookSpeed = 1f;
    [SerializeField] private float lookSpeed2 = 1f;
    [SerializeField] private float zoomSpeed = 10f;
    [SerializeField] private float minZoom = 0f;
    [SerializeField] private float maxZoom = 5f;
    [SerializeField] private float panningSpeed = 1f;
    [SerializeField] private KeyCode panningButton = KeyCode.Mouse2;
    private Vector3 StartPos, StartOffset;
    private Quaternion StartQuat;
    private float StartCameraDistance;
    private void Start()
    {
        SetStartValues();
    }
    private void Update()
    {
        // Obrót kamery
        if (Input.GetMouseButton(1)) // Prawy przycisk myszy - obrót
        {
            HandleRotation();
        }

        // Zoom kamerą
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll != 0.0f)
        {
            AdjustZoom(scroll);
            cinemachineBrain.ManualUpdate();
        }
        if (Input.GetKey(panningButton)) { HandlePan(); }
    }

    private void HandleRotation()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");
        if (POV == null) { POV = virtualCamera.GetCinemachineComponent<CinemachinePOV>(); }
        if (POV)
        {
            POV.m_VerticalAxis.Value -= mouseY * lookSpeed; // Odejmujemy, aby odwrócić kierunek
            POV.m_HorizontalAxis.Value += mouseX * lookSpeed2;
        }
        cinemachineBrain.ManualUpdate();
    }

    private void SetStartValues()
    {
        if (virtualCamera == null) return;
        { cinemachineFraming = virtualCamera.GetCinemachineComponent<CinemachineFramingTransposer>(); }
        if (POV == null) { POV = virtualCamera.GetCinemachineComponent<CinemachinePOV>(); }
        StartCameraDistance = cinemachineFraming.m_CameraDistance;
        StartOffset = cinemachineFraming.m_TrackedObjectOffset;
        StartPos = virtualCamera.transform.position;
        StartQuat = virtualCamera.transform.rotation;

    }
    [ContextMenu("Reset Test")]
    public void ResetVC()
    {
        virtualCamera.ForceCameraPosition(StartPos, StartQuat);
        cinemachineFraming.m_TrackedObjectOffset = StartOffset;
        cinemachineFraming.m_CameraDistance = StartCameraDistance;
        cinemachineBrain.ManualUpdate();
    }
    private void HandlePan()
    {
        Debug.Log("Pan called");
        if (cinemachineFraming == null)
        { cinemachineFraming = virtualCamera.GetCinemachineComponent<CinemachineFramingTransposer>(); }
        float mouseX = Input.GetAxis("Mouse X") * panningSpeed;
        float mouseY = Input.GetAxis("Mouse Y") * panningSpeed;

       
        var trackedObjectOffset = cinemachineFraming.m_TrackedObjectOffset;
     
        Vector3 cameraRight = virtualCamera.transform.right;
        Vector3 cameraUp = virtualCamera.transform.up;
        Vector3 cameraForward = virtualCamera.transform.forward;
        Vector3 displacement = (cameraRight * mouseX + cameraUp * mouseY) * Time.deltaTime;

    
        displacement = Vector3.ProjectOnPlane(displacement, cameraForward);
        trackedObjectOffset += displacement;
        cinemachineFraming.m_TrackedObjectOffset = trackedObjectOffset;
        cinemachineBrain.ManualUpdate();
    }

    private void AdjustZoom(float delta)
    {
        if (cinemachineFraming == null) { cinemachineFraming = virtualCamera.GetCinemachineComponent<CinemachineFramingTransposer>(); }
        cinemachineFraming.m_CameraDistance = Mathf.Clamp(cinemachineFraming.m_CameraDistance + delta * zoomSpeed, minZoom, maxZoom);

    }
}