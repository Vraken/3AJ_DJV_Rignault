using UnityEngine;
using System.Collections;

namespace UnityAsset.Characters.ThirdPerson
{
    public class CameraController
    {
        private float XSensitivity = 2f;
        private float YSensitivity = 2f;

        private Quaternion playerRotation;
        private Quaternion cameraRotation;

        
        public void Init(Transform playerTransform, Transform playerCamera)
        {
            playerRotation = playerTransform.localRotation;
            cameraRotation = playerCamera.localRotation;
        }

        public void LookRotation(Transform playerTransform, Transform playerCamera)
        {
            float yRot = Input.GetAxis("Mouse X") * XSensitivity;
            float xRot = Input.GetAxis("Mouse Y") * YSensitivity;

            playerRotation *= Quaternion.Euler(0f, yRot, 0f);
            cameraRotation *= Quaternion.Euler(-xRot, 0f, 0f);

            playerTransform.localRotation = playerRotation;
            playerCamera.localRotation = cameraRotation;
        }
    }

}
