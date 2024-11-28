using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace SG {
    public class PlayerManager : CharacterManager
    {
        [HideInInspector] public PlayerAnimatorManager playerAnimatorManeger;
        [HideInInspector] public PlayerLocomotionManager playerLocomotionManger;
        public bool isOwner;
        [HideInInspector] public CharacterNetworkManager characterNetworkManager;
        [HideInInspector] public PlayerNetworkManager playerNetworkManager;
        [HideInInspector] public PlayerStatsManager playerStatsManager;
        public bool isPerformingAction;
        protected override void Awake()
        {
            base.Awake();
            playerLocomotionManger = GetComponent<PlayerLocomotionManager>();
            playerAnimatorManeger = GetComponent<PlayerAnimatorManager>(); 
            playerNetworkManager = GetComponent<PlayerNetworkManager>();
            playerStatsManager = GetComponent<PlayerStatsManager>();
        }
        protected override void Update()
        {
            base.Update();
            playerLocomotionManger.HandleAllMovement();


        }
        protected override void LateUpdate()
        {
            if (!IsOwner)
                return;
            base.LateUpdate();
            PlayerCamera.instance.HandleAllCameraActions();
        }

        public override void OnNetworkSpawn()
        {
            base.OnNetworkSpawn();
            if (IsOwner)
            {
                PlayerCamera.instance.player = this;
                PlayerInputManager.instance.player = this;


                playerNetworkManager.currentStamina.OnValueChanged += PlayerUIManager.instance.playerUIHudManager.SetNewStaminaValue;
                playerNetworkManager.currentStamina.OnValueChanged -= playerStatsManager.ResetStaminaRegenTimer;


                playerNetworkManager.maxStamina.Value = playerStatsManager.CalculateStaminaBasedOnEndurancelevel(playerNetworkManager.endurance.Value);
                PlayerUIManager.instance.playerUIHudManager.SetMaxStaminaValue(playerNetworkManager.maxStamina.Value);

                playerNetworkManager.currentStamina.Value = playerStatsManager.CalculateStaminaBasedOnEndurancelevel(playerNetworkManager.endurance.Value);
            }
        }
    }    
}