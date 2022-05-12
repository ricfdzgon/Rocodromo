using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class ContiniusMovement : MonoBehaviour
{

    public float speed;
    private XROrigin rig;
    public XRNode inputSource;
    private Vector2 inputAxis;
    public LayerMask groudLayer;
    public float additionalHeight = 0.2f;
    private CharacterController character;
    public float gravity = -9.81f;
    private float fallinSpeed;
    void Start()
    {
        character = GetComponent<CharacterController>();
        rig = GetComponent<XROrigin>();
    }

    void Update()
    {
        InputDevice device = InputDevices.GetDeviceAtXRNode(inputSource);
        device.TryGetFeatureValue(CommonUsages.primary2DAxis, out inputAxis);
    }

    private void FixedUpdate()
    {

        CapsuleFollowHeadset();
        Quaternion headYaw = Quaternion.Euler(0, rig.Camera.transform.eulerAngles.y, 0);
        Vector3 direction = headYaw * new Vector3(inputAxis.x, 0, inputAxis.y);

        character.Move(direction * Time.deltaTime * speed);
        //gravedad
        bool isGound = CheckIfGround();
        if (isGound)
        {
            fallinSpeed = 0;
        }
        else
        {
            fallinSpeed += gravity * Time.deltaTime; ;
        }
        character.Move(Vector3.up * fallinSpeed * Time.deltaTime);
    }

    bool CheckIfGround()
    {
        Vector3 rayStart = transform.TransformPoint(character.center);
        float rayLength = character.center.y + 0.01f;
        bool hasHit = Physics.SphereCast(rayStart, character.radius, Vector3.down, out RaycastHit hitInfo, rayLength, groudLayer);
        return hasHit;
    }

    void CapsuleFollowHeadset()
    {
        character.height = rig.CameraInOriginSpaceHeight + additionalHeight;
        Vector3 capsuleCenter = transform.InverseTransformPoint(rig.Camera.transform.position);
        character.center = new Vector3(capsuleCenter.x, character.height / 2 + character.skinWidth, capsuleCenter.z);
    }
}
