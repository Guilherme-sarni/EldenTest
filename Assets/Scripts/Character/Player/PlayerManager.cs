using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

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

                playerNetworkManager.maxHealth = playerStatsManager.CalculeteHealthBaseOnVitakytiLavel(
                playerNetworkManager.maxStamina.Value = playerStatsManager.CalculateStaminaBasedOnEndurancelevel(playerNetworkManager.endurance.Value);
                PlayerUIManager.instance.playerUIHudManager.SetMaxStaminaValue(playerNetworkManager.maxStamina.Value);

                playerNetworkManager.currentStamina.Value = playerStatsManager.CalculateStaminaBasedOnEndurancelevel(playerNetworkManager.endurance.Value);
            }
        }
        public void saveGameDataToCurrentCharacterData(ref CharacterSaveData CurentCharachterData)
        {
            currentCharacterData.sceneIndex = SceneManager.GetActiveScene().buildIndex;

            currentCharacterData.characterName = playerNetworkManager.characterName.Value.ToString();
            currentCharacterData.xPostion = transform.position.x;
            currentCharacterData.yPostion = transform.position.y;
            currentCharacterData.zPosition= transform.position.z;

        }
        public void LoadGameDataFromCurrentCharacterData(ref CharacterSaveData currentCharacterData)
        {
            playerNetworkManager.characterName,value = currentCharacterData.characterName.Value;
            Vector3 myPostion = new Vector3(currentCharacterData.xpsition, currentCharacterData.ypostion,currentCharacterData.zPosition);
            transform.position = myPostion;

            playerNetworkManager.maxHealth = playerStatsManager.CalculeteHealthBaseOnVitalityLavel(currentCharacterData.vitality);
            playerNetworkManager.maxStamina = playerStatsManager.CalculateStaminaBasedOnEndurancelevel(currentCharacterData.endurence);
            playerNetworkManager.currentStamina.Value = playerStatsManager.CalculateStaminaBasedOnEndurancelevel(currentCharacterData.endurence.value);
            PlayerUIManager.instance.playerUIHudManager.SetMaxStaminaValue(playerNetworkManager.maxStamina.Value);
        }


    }    
}