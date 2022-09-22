using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisualizeLobbyCamera : MonoBehaviour
{
    public GameObject CameraProper;
    public float ZoomAmount = 0.0f;
    public bool isLive = false;
    Vector3 PanTarget;
    float PanSpeed = 1.0f;

    private void Awake()
    {
        PanTarget = this.transform.position;
        this.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(!isLive)
        {
            return;
        }
        ZoomAmount -= Input.GetAxis("Mouse ScrollWheel");
        ZoomAmount = Mathf.Clamp(ZoomAmount, 0.0f, 1.0f);
        Vector3 LocalPosition = CameraProper.transform.localPosition;
        LocalPosition.z = Mathf.Lerp(LocalPosition.z, -15.0f - (ZoomAmount * 20.0f), Time.deltaTime * 20.0f);
        CameraProper.transform.localPosition = LocalPosition;

       
        if (Input.GetMouseButton(0))
        {                
            float PanX = PanSpeed * Input.GetAxis("Mouse X");
            float PanZ = PanSpeed * Input.GetAxis("Mouse Y");
            PanTarget.x -= PanX;
            PanTarget.z -= PanZ;
            PanTarget.x = Mathf.Clamp(PanTarget.x, -245.0f, -155.0f);
            PanTarget.z = Mathf.Clamp(PanTarget.z, 55.0f, 145.0f);
        }
        if(Vector3.Distance(PanTarget, this.transform.position) > 3.0f)
        {
            this.transform.position = Vector3.Lerp(this.transform.position, PanTarget, Time.deltaTime * 3.0f);
        }
    }

    public void ToggleCamera(bool inState)
    {
        isLive = inState;
        this.gameObject.SetActive(isLive);
    }
}
