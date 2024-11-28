using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    public class CharacterAnimatorManager : MonoBehaviour
    {
        // Start is called before the first frame update
        CharacterManager character;
        float vertical;
        float horizontal;
        protected virtual void Awake () 
        {
            character = GetComponent<CharacterManager>();
        }
        public void UpdateAnimatorMovementParameters(float horizontalValue , float verticalValue)   
        {
            character.animator.SetFloat("Horizontal", horizontalValue);
            character.animator.SetFloat("Vertical", verticalValue);

        }


    }
}