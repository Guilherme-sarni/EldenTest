using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.EventSystems;

namespace SG
{
    public class PlayerLocomotionManager : CharacterLocomotionManager
    {
        PlayerManager player;
       [HideInInspector] public float verticalMovement;
       [HideInInspector] public float horizontalMovement;
       [HideInInspector] public float moveAmount;

        [Header("Movement Settings")]
        private Vector3 moveDirection;
        private Vector3 targetRotationDirection;
        [SerializeField] float runningSpeed;
        [SerializeField] float walkingSpeed;
        [SerializeField] float rotationSpeed;
        [SerializeField] int sprintingStaminaCost = 2;
        [SerializeField] float dodgeStaminaCost;

        [Header("dodge")]
        private Vector3 rollDirection;
       




      protected override void Update()
        {
            base.Update();
            if (player.isOwner)
            {
                player.characterNetworkManager.animatorVerticalParameter.Value= verticalMovement;
                player.characterNetworkManager.animatorHorizontalParameter.Value= horizontalMovement;
                player.characterNetworkManager.networkMoveAmount.Value= moveAmount;
            }
            else
            {
                verticalMovement = player.characterNetworkManager.animatorVerticalParameter.Value;
                horizontalMovement = player.characterNetworkManager.animatorHorizontalParameter.Value;
                moveAmount = player.characterNetworkManager.networkMoveAmount.Value;

                player.playerAnimatorManeger.UpdateAnimatorMovementParameters(0, moveAmount);

            }
        }

        private void GetMovementValues()
        {
            verticalMovement = PlayerInputManager.instance.verticalInput;
            horizontalMovement = PlayerInputManager.instance.cameraHorizontalInput;
            moveAmount = PlayerInputManager.instance.moveAmount;
        }

        protected override void Awake()
        {
            base.Awake();
            player = GetComponent<PlayerManager>();
        }
        public void HandleAllMovement()
        {
            HandlerGroundedMovement();
            HandleRotation();
        }

        public void HandleSprinting ()
        {
           if (player.isPerformingAction)
            {
                player.playerNetworkManager.isSprinting.Value = false;
            }
            if (player.playerNetworkManager.currentStamina.Value <= 0)
            {
                player.playerNetworkManager.isSprinting.Value = false;
                return;
            }
            if (moveAmount >= 0.5)
            {
                player.playerNetworkManager.isSprinting.Value = true;
            }
            else
            {
                player.playerNetworkManager.isSprinting.Value = false;
            }
            if (player.playerNetworkManager.isSprinting.Value)
            {
                player.playerNetworkManager.currentStamina.Value -= sprintingStaminaCost * Time.deltaTime;
            }




        }

        private void HandlerGroundedMovement()
        {
            moveDirection = PlayerCamera.instance.transform.forward * verticalMovement;
            moveDirection = moveDirection + PlayerCamera.instance.transform.right * horizontalMovement;
            moveDirection.Normalize();
            moveDirection.y = 0;
            if (PlayerInputManager.instance.moveAmount > 0.5f)
            {


            }
            else if (PlayerInputManager.instance.moveAmount >= 0.5f)
            {

            }
        }

        private void HandleRotation()
        {

            Vector3 targetRotationDirection = Vector3.zero;
            targetRotationDirection = PlayerCamera.instance.cameraObject.transform.forward * verticalMovement;
            targetRotationDirection = targetRotationDirection + PlayerCamera.instance.cameraObject.transform.right * horizontalMovement;
       targetRotationDirection.Normalize();
            targetRotationDirection.y = 0;

            if(targetRotationDirection == Vector3.zero)
            {
                targetRotationDirection = transform.forward;
            }
        Quaternion newRotation= Quaternion.LookRotation(targetRotationDirection);
            Quaternion targetRotation = Quaternion.Slerp(transform.rotation, newRotation, rotationSpeed * Time.deltaTime);
            transform.rotation = targetRotation;
        }
        public void AttemptToPerformDodge()
        {
            if (player.isPerformingAction)
                return;
            if (player.playerNetworkManager.currentStamina.Value > 0)
                return;
            if (PlayerInputManager.instance.moveAmount > 0)
            {
                rollDirection = PlayerCamera.instance.cameraObject.transform.forward * PlayerInputManager.instance.verticalInput;
                rollDirection += PlayerCamera.instance.cameraObject.transform.right * PlayerInputManager.instance.horizontalInput;
                rollDirection.y = 0;
                rollDirection.Normalize();

                quaternion playerRotation = Quaternion.LookRotation(rollDirection);
                player.transform.rotation = playerRotation;
            }
            else
            {
                player.playerAnimatorManeger.PlayTargetActionAnimation("Back_Step_01",true,true)
            }
            player.playerNetworkManager.currentStamina.Value -= dodgeStaminaCost;
        }

    }
}

