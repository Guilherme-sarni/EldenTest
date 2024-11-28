using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using Unity.Mathematics;
using UnityEngine;


namespace SG
{
    public class CharacterStatsManager : MonoBehaviour
    {
        CharacterManager character;
        CharacterNetworkManager characterNetworkManager;
        [Header("Stamina Regeneration")]
        [SerializeField] float staminaRegenerationAmount = 2;
        private float staminaRegenerationTimer = 0;
        private float staminaTickTimer = 0;
        float staminaRegenerationDelay = 2;

        public int CalculeteHealthBaseOnVitakytiLavel(int vitality)
        {
            float health = 0;
            health = vitality * 10;
            return Mathf.RoundToInt(health);
        }

        public int CalculateStaminaBasedOnEndurancelevel(float endurance)
        {
            float  stamina = 0;
            stamina = endurance * 10;
            return Mathf.RoundToInt(stamina);
        }


        public virtual void RegenerateStamina()
        {
            if (!character.IsOwner)
                return;
            if (characterNetworkManager.isSprinting.Value)
                return;
            if (character.isPerformingAction)
                return;
            staminaRegenerationTimer += Time.deltaTime;
            if (staminaRegenerationTimer > staminaRegenerationDelay)
            {
                if (characterNetworkManager.currentStamina.Value < characterNetworkManager.maxStamina.Value)
                {
                    staminaTickTimer += Time.deltaTime;
                    if (staminaTickTimer >= 0.1)
                    {
                        staminaTickTimer = 0;
                        characterNetworkManager.currentStamina.Value += staminaRegenerationAmount;
                    }
                }

            }


        }
        public virtual void ResetStaminaRegenTimer(float PreviosStaminaAmount,float currentStaminaAmount)
        {
            if (currentStaminaAmount < PreviosStaminaAmount)
            {
                staminaRegenerationTimer = 0; 
            }
        }
      

    }
}

