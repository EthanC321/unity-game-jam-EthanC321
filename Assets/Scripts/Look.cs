using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class Look : MonoBehaviour
{
    #region Variables
    public Transform Player;
    public Transform cams;

    public float xSensitivity;
    public float ySensitivity;
    public float maxAngle;

    private Quaternion camCenter;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        camCenter = cams.localRotation;
    }

    // Update is called once per frame
    void Update()
    {
       
        SetY();
        SetX();
    }

    void SetY()
    {
        float t_input = Input.GetAxis("Mouse Y") * ySensitivity;
        Quaternion t_adj = Quaternion.AngleAxis(t_input, -Vector3.right);
        Quaternion t_delta = cams.localRotation * t_adj;

        if (Quaternion.Angle(camCenter, t_delta) < maxAngle)
        {
            cams.localRotation = t_delta;
        }

        
    }

    void SetX()
    {
        float t_input = Input.GetAxis("Mouse X") * xSensitivity;
        Quaternion t_adj = Quaternion.AngleAxis(t_input, Vector3.up);
        Quaternion t_delta = Player.localRotation * t_adj;
        Player.localRotation = t_delta;


    }
}
