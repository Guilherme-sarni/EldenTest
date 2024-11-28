using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Unity.Mathematics;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SG
{
    public class PlayerInputManager : MonoBehaviour
    {
        public static PlayerInputManager instance;
        public PlayerManager player;
        PlayerControls playerControls;
        [Header("Mov Input")]
        [SerializeField] Vector2 movementInput;
        
        [SerializeField] public float verticalInput;
        [SerializeField] public float horizontalInput;
        [SerializeField] public float moveAmount;

        [Header("Camera Mov Input")]
        [SerializeField] Vector2 cameraInput;
        public float cameraVerticalInput;
        public float cameraHorizontalInput;

        [Header("PLAYER ACTION INPUT")]
        [SerializeField] bool dodgeInput = false;

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
        private void OnSceneChange(Scene oldScene, Scene newScene)
        {
            if (newScene.buildIndex == WorldSaveGameManager.instance.WorldIndex())
            {
                instance.enabled = true;
            }
            else
            {
                instance.enabled = false;
            }
      }

        private void Start()
        {
            DontDestroyOnLoad(gameObject);
            SceneManager.activeSceneChanged += OnSceneChange;SceneManager.activeSceneChanged += OnSceneChange;
            instance.enabled = false;
            
        }
        private void OnEnable()
        {
         if (playerControls == null) 
         {
                playerControls = new PlayerControls();
                playerControls.PlayerMovement.Movement.performed += i => movementInput = i.ReadValue<Vector2>();
                playerControls.PlayerCamera.CameraControls.performed += i => cameraInput = i.ReadValue<Vector2>();
                playerControls.PlayerActions.Dodge.performed += i => dodgeInput = true;
            
            }
            playerControls.Enable();
        }
        private void OnDestroy()
        {
            SceneManager.activeSceneChanged -= OnSceneChange;   
        }
        private void Update()
        {
            HandlePlayerMovementInput();
            HandleCameraMovementInput();
        }

        private void OnApplicationFocus(bool focus)
        {
            if (enabled)
            {
                playerControls.Enable();         
            }
            else
            {
                playerControls.Disable();
            }
        }
        private void HandlePlayerMovementInput()
        {
            verticalInput = movementInput.y;
            horizontalInput = movementInput.x;

            moveAmount = Mathf.Clamp01(Mathf.Abs(verticalInput) + Mathf.Abs(horizontalInput));

            if (movementInput.x < 0.5 && moveAmount > 0)
            {
                moveAmount = 0.5f;
            }
            else if (movementInput.x > 0.5 && moveAmount<= 1)
            {
                moveAmount =1; ;
                //movementInput = 1;
            }
            player.playerAnimatorManeger.UpdateAnimatorMovementParameters(0, moveAmount);
        }
        private void HandleCameraMovementInput()
        {
            cameraVerticalInput = cameraInput.y;
            cameraHorizontalInput = cameraInput.x;
        }
        private void HandleDogeInput()
        {
            if (dodgeInput)
            {
                dodgeInput = false;
            }
        }

        // Start is called before the first frame update

    }
} 