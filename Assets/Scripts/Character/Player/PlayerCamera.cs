using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Windows.WebCam;

namespace SG
{
    public class PlayerCamera : MonoBehaviour
    {
        public static PlayerCamera instance;
        public PlayerManager player;
        public Camera cameraObject;
        [SerializeField] Transform cameraPivotTransform;

        [Header("Camera Settings")]
        private float cameraSmoothSpeed = 1;
        [SerializeField] private float upAndDownRotationSpeed = 220;
        [SerializeField] private float leftAndRightRotationSpeed = 220;
        [SerializeField] float minimumPivot = -30;
        [SerializeField] float maximumPivot = 60;
        [SerializeField] float cameraCollisionOffset = 0.2f;
    


        [Header("Camera Values")]
        private Vector3 cameraVelocity;
        [SerializeField] float LeftAndRightLookAngle;
        [SerializeField] float upAndDownLookAngle;
        private float cameraZPosition;
        private float targetCameraZPosition;
        [SerializeField] Vector3 cameraObjectPosition;
        [SerializeField] LayerMask collideWithLayers;


        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
            else
            {
                Destroy(gameObject);
            }


        }

        private void Start()
        {
            DontDestroyOnLoad(gameObject);
            cameraZPosition = cameraObject.transform.localPosition.z;
        }

        public void HandleAllCameraActions()
        {
            FollowTarget();



        }
        void FollowTarget()
        {
            Vector3 targetCameraPosistion = Vector3.SmoothDamp(transform.position,
                player.transform.position, ref cameraVelocity, cameraSmoothSpeed * Time.deltaTime);
        }

        private void HandleRotations()
        {
            leftAndRightRotationSpeed += (PlayerInputManager.instance.cameraVerticalInput * leftAndRightRotationSpeed) * Time.deltaTime;
            upAndDownLookAngle -= (PlayerInputManager.instance.cameraHorizontalInput * upAndDownRotationSpeed) * Time.deltaTime;
            upAndDownLookAngle = Mathf.Clamp(upAndDownLookAngle, minimumPivot, maximumPivot);


            Vector3 cameraRotation = Vector3.zero;
            quaternion targetRotation;

            cameraRotation.y = LeftAndRightLookAngle;
            targetRotation = quaternion.Euler(cameraRotation);
            transform.rotation = targetRotation;

            cameraRotation = Vector3.zero;
            cameraRotation.x = upAndDownLookAngle;
            targetRotation = quaternion.Euler(cameraRotation);
            cameraPivotTransform.localRotation = targetRotation;
        }

        private void HandlerCollisions()
        {
            targetCameraZPosition = cameraZPosition;
            RaycastHit hit;
            Vector3 director = cameraObject.transform.position - cameraPivotTransform.position;
            director.Normalize();

            if (Physics.SphereCast(cameraPivotTransform.position, cameraCollisionOffset, director, out hit, Mathf.Abs(targetCameraZPosition), collideWithLayers))
            {
                float distanceFromHitObject = Vector3.Distance(cameraPivotTransform.position, hit.point);
                targetCameraZPosition = -(distanceFromHitObject - cameraCollisionOffset);
            }
            if (Mathf.Abs(targetCameraZPosition) < cameraCollisionOffset)
            {
                targetCameraZPosition = -cameraCollisionOffset;
            }
            cameraObjectPosition.z = Mathf.Lerp(cameraObject.transform.localPosition.z, targetCameraZPosition, cameraCollisionOffset);

        }
    }
}