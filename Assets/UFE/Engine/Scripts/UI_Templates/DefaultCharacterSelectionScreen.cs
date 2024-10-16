﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using FPLibrary;
using UFE3D;
using System;
using UnityEngine.TextCore.Text;
using UnityEngine.UIElements;
using Button = UnityEngine.UI.Button;
using Image = UnityEngine.UI.Image;

public class DefaultCharacterSelectionScreen : CharacterSelectionScreen
{
    #region public enum definitions
    public enum DisplayMode
    {
        CharacterPortrait,
        CharacterGameObject,
    }
    #endregion

    #region public instance fields
    public Text namePlayer1;
    public Button selectButton;
    public Text namePlayer2;
    public DisplayMode displayMode = DisplayMode.CharacterPortrait;
    public Image portraitPlayer1;
    public Image portraitPlayer2;
    public GameObject background3dPrefab;
    public Vector3 positionPlayer1 = new Vector3(-4, 0, 0);
    public Vector3 positionPlayer2 = new Vector3(4, 0, 0);
    public Image[] characters;
    public Animator hudPlayer1;
    public Animator hudPlayer2;
    public Animator hudBothPlayers;
    public Sprite noCharacterSprite;

    public int defaultCharacterPlayer1 = 0;
    public int defaultCharacterPlayer2 = 999;

    public GameObject title1;
    public GameObject title2;
    public Vector2 hudOffset = Vector2.zero;
    
    #endregion

    #region protected instance fields
    protected List<Selectable> characterButtonsWhiteList = new List<Selectable>();

    protected GameObject background;
    protected GameObject gameObjectPlayer1;
    protected GameObject gameObjectPlayer2;
    #endregion

    private int selectedCharacterIndex = -1;
    private bool isPLayer2Allowed = false;
    private bool isPLayer1Allowed = true;

    #region public override methods
    public override void DoFixedUpdate(
        IDictionary<InputReferences, InputEvents> player1PreviousInputs,
        IDictionary<InputReferences, InputEvents> player1CurrentInputs,
        IDictionary<InputReferences, InputEvents> player2PreviousInputs,
        IDictionary<InputReferences, InputEvents> player2CurrentInputs
    )
    {
        //base.DoFixedUpdate(player1PreviousInputs, player1CurrentInputs, player2PreviousInputs, player2CurrentInputs);

        if (UFE.gameMode != GameMode.StoryMode && UFE.gameMode != GameMode.TrainingRoom && !UFE.GetCPU(2))
        {
            // If both characters will be controlled by human players...
            this.SpecialNavigationSystem(
                player1PreviousInputs
                ,
                player1CurrentInputs
                ,
                new UFEScreenExtensions.MoveCursorCallback(
                delegate (
                    Fix64 horizontalAxis,
                    Fix64 verticalAxis,
                    bool horizontalAxisDown,
                    bool verticalAxisDown,
                    bool confirmButtonDown,
                    bool cancelButtonDown,
                    AudioClip sound
                )
                {
                    this.MoveCursor(
                        1,
                        horizontalAxis,
                        verticalAxis,
                        horizontalAxisDown,
                        verticalAxisDown,
                        confirmButtonDown,
                        cancelButtonDown,
                        sound
                    );
                })
                ,
                new UFEScreenExtensions.ActionCallback(delegate (AudioClip sound)
                {
                    this.TrySelectCharacter(this.p1HoverIndex, 1);
                })
                ,
                new UFEScreenExtensions.ActionCallback(delegate (AudioClip sound)
                {
                    this.TryDeselectCharacter(1);
                })
            );

            this.SpecialNavigationSystem(
                player2PreviousInputs
                ,
                player2CurrentInputs
                ,
                new UFEScreenExtensions.MoveCursorCallback(
                delegate (
                    Fix64 horizontalAxis,
                    Fix64 verticalAxis,
                    bool horizontalAxisDown,
                    bool verticalAxisDown,
                    bool confirmButtonDown,
                    bool cancelButtonDown,
                    AudioClip sound
                )
                {
                    this.MoveCursor(
                        2,
                        horizontalAxis,
                        verticalAxis,
                        horizontalAxisDown,
                        verticalAxisDown,
                        confirmButtonDown,
                        cancelButtonDown,
                        sound
                    );
                })
                ,
                new UFEScreenExtensions.ActionCallback(delegate (AudioClip sound)
                {
                    this.TrySelectCharacter(this.p2HoverIndex, 2);
                })
                ,
                new UFEScreenExtensions.ActionCallback(delegate (AudioClip sound)
                {
                    this.TryDeselectCharacter(2);
                })
            );
        }
        else
        {
            // If at least one characters will be controlled by the CPU...
            this.SpecialNavigationSystem(
                player1PreviousInputs
                ,
                player1CurrentInputs
                ,
                new UFEScreenExtensions.MoveCursorCallback(delegate (
                    Fix64 horizontalAxis,
                    Fix64 verticalAxis,
                    bool horizontalAxisDown,
                    bool verticalAxisDown,
                    bool confirmButtonDown,
                    bool cancelButtonDown,
                    AudioClip sound
                )
                {
                    this.MoveCursor(
                        UFE.config.player1Character == null ? 1 : 2,
                        horizontalAxis,
                        verticalAxis,
                        horizontalAxisDown,
                        verticalAxisDown,
                        confirmButtonDown,
                        cancelButtonDown,
                        sound
                    );
                })
                ,
                new UFEScreenExtensions.ActionCallback(this.TrySelectCharacter),
                new UFEScreenExtensions.ActionCallback(this.TryDeselectCharacter)
            );
        }
    }

    public override void SetHoverIndex(int player, int characterIndex)
    {
        Debug.Log("SetHoverIndex. player: " + player + " characterIndex: " + characterIndex);
        int maxCharacterIndex = this.GetMaxCharacterIndex();
        this.p1HoverIndex = Mathf.Clamp(this.p1HoverIndex, 0, maxCharacterIndex);
        this.p2HoverIndex = Mathf.Clamp(this.p2HoverIndex, 0, maxCharacterIndex);
        base.SetHoverIndex(player, characterIndex);

        if (characterIndex >= 0 && characterIndex <= maxCharacterIndex)
        {
            UFE3D.CharacterInfo character = this.selectableCharacters[characterIndex];

            // First, update the big portrait or the character 3D model (depending on the Display Mode)
            if (player == 1 && isPLayer1Allowed)
            {
                if (this.namePlayer1 != null)
                {
                    this.namePlayer1.text = character.characterName;
                }
                this.namePlayer1.text = character.characterName;
                if (this.displayMode == DisplayMode.CharacterPortrait)
                {
                    if (this.portraitPlayer1 != null)
                    {
                        this.portraitPlayer1.sprite = Sprite.Create(
                            character.profilePictureBig,
                            new Rect(0f, 0f, character.profilePictureBig.width, character.profilePictureBig.height),
                            new Vector2(0.5f * character.profilePictureBig.width, 0.5f * character.profilePictureBig.height)
                        );
                    }
                }
                else if (this.displayMode == DisplayMode.CharacterGameObject)
                {
                    UFE3D.CharacterInfo characterInfo = UFE.config.characters[characterIndex];
                    if (this.gameObjectPlayer1 != null)
                    {
                        GameObject.Destroy(this.gameObjectPlayer1);
                    }


                    AnimationClip clip = characterInfo.selectionAnimation != null ? characterInfo.selectionAnimation : null;


                    if (characterInfo.characterPrefabStorage == StorageMode.Prefab)
                    {
                        this.gameObjectPlayer1 = GameObject.Instantiate(characterInfo.characterPrefab);
                    }
                    else
                    {
                        this.gameObjectPlayer1 = GameObject.Instantiate(Resources.Load<GameObject>(characterInfo.prefabResourcePath));
                    }
                    //this.gameObjectPlayer1 = GameObject.Instantiate(characterInfo.characterPrefab);
                    this.gameObjectPlayer1.transform.position = this.positionPlayer1;
                    this.gameObjectPlayer1.transform.SetParent(this.transform, true);

                    HitBoxesScript hitBoxes = this.gameObjectPlayer1.GetComponent<HitBoxesScript>();
                    if (hitBoxes != null)
                    {
                        foreach (HitBox hitBox in hitBoxes.hitBoxes)
                        {
                            if (hitBox != null && hitBox.bodyPart != BodyPart.none && hitBox.position != null)
                            {
                                hitBox.position.gameObject.SetActive(hitBox.defaultVisibility);
                            }
                        }
                        hitBoxes.hitBoxes = null;
                    }

                    if (characterInfo.animationType == AnimationType.Legacy)
                    {
                        Animation animation = this.gameObjectPlayer1.GetComponent<Animation>();
                        if (animation == null)
                        {
                            animation = this.gameObjectPlayer1.AddComponent<Animation>();
                        }

                        animation.AddClip(clip, "Idle");
                        animation.wrapMode = WrapMode.Loop;
                        animation.Play("Idle");
                    }
                    else
                    {
                        Animator animator = this.gameObjectPlayer1.GetComponent<Animator>();
                        if (animator == null)
                        {
                            animator = this.gameObjectPlayer1.AddComponent<Animator>();
                        }

                        AnimatorOverrideController overrideController = new AnimatorOverrideController();
                        overrideController.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("MC_Controller");
                        overrideController["State1"] = clip;

                        animator.avatar = characterInfo.avatar;
                        animator.applyRootMotion = characterInfo.applyRootMotion;
                        animator.runtimeAnimatorController = overrideController;
                        animator.Play("State1");
                    }

                    gameObjectPlayer1.transform.localRotation = characterInfo.initialRotation.ToQuaternion();
                    gameObjectPlayer1.transform.localScale *= 1.1f;
                }
            }
            else if (player == 2 && isPLayer2Allowed)
            {
                if (this.namePlayer2 != null)
                {
                    this.namePlayer2.text = character.characterName;
                }

                if (this.displayMode == DisplayMode.CharacterPortrait)
                {
                    if (this.portraitPlayer2 != null)
                    {
                        this.portraitPlayer2.sprite = Sprite.Create(
                            character.profilePictureBig,
                            new Rect(0f, 0f, character.profilePictureBig.width, character.profilePictureBig.height),
                            new Vector2(0.5f * character.profilePictureBig.width, 0.5f * character.profilePictureBig.height)
                        );
                    }
                }
                else if (this.displayMode == DisplayMode.CharacterGameObject)
                {
                    UFE3D.CharacterInfo characterInfo = UFE.config.characters[characterIndex];
                    if (this.gameObjectPlayer2 != null)
                    {
                        GameObject.Destroy(this.gameObjectPlayer2);
                    }

                    if (UFE.gameMode != GameMode.StoryMode)
                    {

                        AnimationClip clip = characterInfo.selectionAnimation != null ? characterInfo.selectionAnimation : null;

                        if (characterInfo.characterPrefabStorage == StorageMode.Prefab)
                        {
                            this.gameObjectPlayer2 = GameObject.Instantiate(characterInfo.characterPrefab);
                        }
                        else
                        {
                            this.gameObjectPlayer2 = GameObject.Instantiate(Resources.Load<GameObject>(characterInfo.prefabResourcePath));
                        }
                        //this.gameObjectPlayer2 = GameObject.Instantiate(characterInfo.characterPrefab);
                        this.gameObjectPlayer2.transform.position = this.positionPlayer2;


                        this.gameObjectPlayer2.transform.SetParent(this.transform, true);

                        HitBoxesScript hitBoxes = this.gameObjectPlayer2.GetComponent<HitBoxesScript>();
                        if (hitBoxes != null)
                        {
                            foreach (HitBox hitBox in hitBoxes.hitBoxes)
                            {
                                if (hitBox != null && hitBox.bodyPart != BodyPart.none && hitBox.position != null)
                                {
                                    hitBox.position.gameObject.SetActive(hitBox.defaultVisibility);
                                }
                            }
                            hitBoxes.hitBoxes = null;
                        }

                        if (characterInfo.animationType == AnimationType.Legacy)
                        {
                            Animation animation = this.gameObjectPlayer2.GetComponent<Animation>();
                            if (animation == null)
                            {
                                animation = this.gameObjectPlayer2.AddComponent<Animation>();
                            }

                            this.gameObjectPlayer2.transform.localScale = new Vector3(
                                -this.gameObjectPlayer2.transform.localScale.x,
                                this.gameObjectPlayer2.transform.localScale.y,
                                this.gameObjectPlayer2.transform.localScale.z
                            );

                            animation.AddClip(clip, "Idle");
                            animation.wrapMode = WrapMode.Loop;
                            animation.Play("Idle");
                        }
                        else
                        {
                            Animator animator = this.gameObjectPlayer2.GetComponent<Animator>();
                            if (animator == null)
                            {
                                animator = this.gameObjectPlayer2.AddComponent<Animator>();
                            }

                            // Mecanim, mirror via Animator...
                            AnimatorOverrideController overrideController = new AnimatorOverrideController();
                            overrideController.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("MC_Controller");
                            overrideController["State3"] = clip;

                            animator.avatar = characterInfo.avatar;
                            animator.applyRootMotion = characterInfo.applyRootMotion;
                            animator.runtimeAnimatorController = overrideController;
                            animator.Play("State3");
                        }

                        if (characterInfo.animationType == AnimationType.Mecanim2D)
                        {
                            float xScale = Mathf.Abs(gameObjectPlayer2.transform.localScale.x) * -1;
                            gameObjectPlayer2.transform.localScale = new Vector3(xScale, gameObjectPlayer2.transform.localScale.y, gameObjectPlayer2.transform.localScale.z);
                        }
                        else
                        {
                            float invertedY = characterInfo.initialRotation.ToQuaternion().eulerAngles.y;
                            gameObjectPlayer2.transform.localRotation = Quaternion.Euler(characterInfo.initialRotation.ToQuaternion().eulerAngles.x, -invertedY, characterInfo.initialRotation.ToQuaternion().eulerAngles.z);
                        }
                        gameObjectPlayer2.transform.localScale *= 1.1f;
                    }
                }
            }

            // Deal with alternative colors if both players have selected the same character
            /*if (this.gameObjectPlayer2 != null && this.displayMode == DisplayMode.CharacterGameObject){
				UFE3D.CharacterInfo p2CharacterInfo = UFE.config.characters[this.p2HoverIndex];
				if (p2CharacterInfo.enableAlternativeColor && this.p1HoverIndex == this.p2HoverIndex){
					foreach(Renderer renderer in this.gameObjectPlayer2.GetComponentsInChildren<Renderer>()){
						renderer.material.color = p2CharacterInfo.alternativeColor;
					}
				}else{
					Renderer[] originalRenderers = p2CharacterInfo.characterPrefab.GetComponentsInChildren<Renderer>(true);
					Renderer[] instanceRenderers = this.gameObjectPlayer2.GetComponentsInChildren<Renderer>(true);

					for (int i = 0; i < originalRenderers.Length && i < instanceRenderers.Length; ++i){
						instanceRenderers[i].material.color = originalRenderers[i].sharedMaterial.color;
					}
				}
			}*/

            // Then, update the cursor position
            if (this.hudPlayer1 != null)
            {
                RectTransform rt = this.hudPlayer1.transform as RectTransform;
                if (rt != null)
                {
                    rt.anchoredPosition = this.characters[this.p1HoverIndex].rectTransform.anchoredPosition + hudOffset;
                }
                else
                {
                    this.hudPlayer1.transform.position = this.characters[this.p1HoverIndex].transform.position;
                }
            }

            if (this.hudPlayer2 != null)
            {
                RectTransform rt = this.hudPlayer2.transform as RectTransform;
                if (rt != null)
                {
                    rt.anchoredPosition = this.characters[this.p2HoverIndex].rectTransform.anchoredPosition + hudOffset;
                }
                else
                {
                    this.hudPlayer2.transform.position = this.characters[this.p2HoverIndex].transform.position;
                }
            }

            if (this.hudBothPlayers != null)
            {
                RectTransform rt = this.hudBothPlayers.transform as RectTransform;
                if (rt != null)
                {
                    rt.anchoredPosition = this.characters[this.p2HoverIndex].rectTransform.anchoredPosition + hudOffset;
                }
                else
                {
                    this.hudBothPlayers.transform.position = this.characters[this.p2HoverIndex].transform.position;
                }
            }
        }

        this.UpdateHud();
    }

    public override void OnCharacterSelectionAllowed(int characterIndex, int player)
    {
        Debug.Log("OnCharacterSelectionAllowed. characterIndex: " + characterIndex + "player: " + player);
        base.OnCharacterSelectionAllowed(characterIndex, player);
        this.UpdateHud();
    }

    public override void OnHide()
    {
        Debug.Log("OnHide");
        if (this.gameObjectPlayer1 != null)
        {
            GameObject.Destroy(this.gameObjectPlayer1);
        }
        if (this.gameObjectPlayer2 != null)
        {
            GameObject.Destroy(this.gameObjectPlayer2);
        }
        if (this.background != null)
        {
            GameObject.Destroy(this.background);
        }

        UFE.canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        UFE.canvas.worldCamera = null;
        base.OnHide();
    }

    public override void OnShow()
    {
        Debug.Log("OnShow");
        // Camera setup
        Camera.main.transform.position = UFE.config.cameraOptions.initialDistance;
        Camera.main.transform.eulerAngles = UFE.config.cameraOptions.initialRotation;
        Camera.main.fieldOfView = UFE.config.cameraOptions.initialFieldOfView;

        // Instantiate 3D background if in CharacterGameObject display mode
        if (this.displayMode == DisplayMode.CharacterGameObject)
        {
            if (background3dPrefab != null)
            {
                this.background = GameObject.Instantiate(background3dPrefab);
            }

            UFE.canvas.planeDistance = 0.1f;
            UFE.canvas.worldCamera = Camera.main;
            UFE.canvas.renderMode = RenderMode.ScreenSpaceCamera;
        }

        base.OnShow();

        // Setup character selection buttons and hover indices
        SetupCharacterSelection();

        // Update the HUD based on the game mode
        if (UFE.gameMode == GameMode.StoryMode)
        {
            if (this.namePlayer2 != null)
            {
                this.namePlayer2.text = "???";
            }

            if (this.portraitPlayer2 != null)
            {
                this.portraitPlayer2.gameObject.SetActive(false);
            }

            this.UpdateHud();
        }
        else
        {
            this.SetHoverIndex(2, Mathf.Clamp(this.defaultCharacterPlayer2, 0, this.selectableCharacters.Length - 1));

            if (this.portraitPlayer2 != null)
            {
                this.portraitPlayer2.gameObject.SetActive(true);
            }
        }
    }
    
    private void SetupCharacterSelection()
    {
        Debug.Log("SetupCharacterSelection");
        this.characterButtonsWhiteList.Clear();
        
        // Set the portraits of the characters
        if (this.characters != null)
        {
            // Update the portraits of the characters
            for (int i = 0; i < this.selectableCharacters.Length && i < this.characters.Length; ++i)
            {
                Image character = this.characters[i];
                UFE3D.CharacterInfo selectableCharacter = this.selectableCharacters[i];

                if (character != null)
                {
                    character.gameObject.SetActive(true);
                    character.sprite = Sprite.Create(
                        selectableCharacter.profilePictureSmall,
                        new Rect(0f, 0f, selectableCharacter.profilePictureSmall.width, selectableCharacter.profilePictureSmall.height),
                        new Vector2(0.5f * selectableCharacter.profilePictureSmall.width, 0.5f * selectableCharacter.profilePictureSmall.height)
                    );

                    Button button = character.GetComponent<Button>();
                    if (button == null)
                    {
                        button = character.gameObject.AddComponent<Button>();
                    }

                    int index = i;
                    //Debug.Log("index for: " + index);
                    //button.onClick.AddListener(() => { this.TrySelectCharacter(index); });
                    //button.targetGraphic = character;
                    //this.characterButtonsWhiteList.Add(button);
                    button.onClick.AddListener(() => { OnCharacterButtonClicked(index); });
                    button.targetGraphic = character;
                    this.characterButtonsWhiteList.Add(button);

                }
            }
            

            // Fill remaining slots with "No Character" sprite or hide them
            for (int i = this.selectableCharacters.Length; i < this.characters.Length; ++i)
            {
                Image character = this.characters[i];
                if (character != null)
                {
                    if (this.noCharacterSprite != null)
                    {
                        this.characters[i].gameObject.SetActive(true);
                        //this.characters[i].sprite = this.noCharacterSprite;
                    }
                    else
                    {
                        this.characters[i].gameObject.SetActive(false);
                    }
                }
            }
        }

        this.SetHoverIndex(1, Mathf.Clamp(this.defaultCharacterPlayer1, 0, this.selectableCharacters.Length - 1));
    }
    private void OnCharacterButtonClicked(int index)
    {
        selectedCharacterIndex = index;
        hudPlayer1.gameObject.SetActive(true);

        hudBothPlayers.gameObject.SetActive(true);
        if (this.hudBothPlayers != null)
        {
            this.hudBothPlayers.SetBool("IsHidden", this.p1HoverIndex != this.p2HoverIndex);

            this.hudBothPlayers.SetBool(
                "IsSelected",
                UFE.config.player1Character != null && UFE.config.player2Character != null
            );
        }


        if (isPLayer2Allowed)
        {
            namePlayer2.gameObject.SetActive(true);
        }
        Debug.Log("OnCharacterButtonClicked. index: " + index);
        
        if (UFE.config.player1Character == null)
        {
            this.SetHoverIndex(1, index);
                
        }
        else if (UFE.config.player2Character == null && UFE.gameMode != GameMode.StoryMode)
        {
            this.SetHoverIndex(2, index);
        }
        selectButton.onClick.RemoveAllListeners();
        selectButton.onClick.AddListener(() => { this.TrySelectCharacter(selectedCharacterIndex); });
        selectButton.onClick.AddListener(() => { this.characterSelectButton(); });

    }
    public void characterSelectButton()
    {
        isPLayer2Allowed = true;
        isPLayer1Allowed = false;

        
        title1.SetActive(false);
        title2.SetActive(true);
        
        hudPlayer2.gameObject.SetActive(true);
    }
    #endregion

    #region protected instance methods
    protected override int GetMaxCharacterIndex()
    {
        return Mathf.Min(this.selectableCharacters.Length, this.characters.Length) - 1;
    }

    protected virtual void UpdateHud()
    {
        Debug.Log("UpdateHud");
        if (UFE.gameMode == GameMode.StoryMode)
        {
            if (this.hudPlayer1 != null)
            {
                this.hudPlayer1.SetBool("IsHidden", false);
                this.hudPlayer1.SetBool("IsSelected", UFE.config.player1Character != null);
            }

            if (this.hudPlayer2 != null)
            {
                this.hudPlayer2.SetBool("IsHidden", true);
                this.hudPlayer2.SetBool("IsSelected", UFE.config.player2Character != null);
            }

            if (this.hudBothPlayers != null)
            {
                this.hudBothPlayers.SetBool("IsHidden", true);
                this.hudBothPlayers.SetBool("IsSelected", UFE.config.player1Character != null && UFE.config.player2Character != null);
            }
        }
        else
        {
            if (this.hudPlayer1 != null)
            {
                this.hudPlayer1.SetBool("IsHidden", this.p1HoverIndex == this.p2HoverIndex);
                this.hudPlayer1.SetBool("IsSelected", UFE.config.player1Character != null);
            }

            if (this.hudPlayer2 != null)
            {
                this.hudPlayer2.SetBool("IsHidden", this.p1HoverIndex == this.p2HoverIndex);
                this.hudPlayer2.SetBool("IsSelected", UFE.config.player2Character != null);
            }

            if (this.hudBothPlayers != null)
            {
                this.hudBothPlayers.SetBool("IsHidden", this.p1HoverIndex != this.p2HoverIndex);

                this.hudBothPlayers.SetBool(
                    "IsSelected",
                    UFE.config.player1Character != null && UFE.config.player2Character != null
                );
            }
        }
    }

    protected virtual void MoveCursor(int player, int characterIndex)
    {
        Debug.Log("MoveCursor. PLayer: " + player + " characterIndex: " + characterIndex);
        int previousIndex = this.GetHoverIndex(player);
        this.SetHoverIndex(player, characterIndex);
        int newIndex = this.GetHoverIndex(player);
        if (previousIndex != newIndex && this.moveCursorSound != null) UFE.PlaySound(this.moveCursorSound);
    }
    #endregion

    #region protected instance methods: methods required by the Special Navigation System (GUI)
    protected virtual void MoveCursor(
        int player,
        Fix64 horizontalAxis,
        Fix64 verticalAxis,
        bool horizontalAxisDown,
        bool verticalAxisDown,
        bool confirmButtonDown,
        bool cancelButtonDown,
        AudioClip sound
    )
    {
        bool characterSelected = true;
        int currentIndex = -1;

        if (player == 1)
        {
            currentIndex = this.p1HoverIndex;
            characterSelected = UFE.config.player1Character != null;
        }
        else if (player == 2)
        {
            currentIndex = this.p2HoverIndex;
            characterSelected = UFE.config.player2Character != null;
        }

        if (!characterSelected || currentIndex < 0)
        {
            Vector3 direction = Vector3.zero;

            if (horizontalAxisDown)
            {
                if (horizontalAxis > 0) direction = Vector3.right;
                else if (horizontalAxis < 0) direction = Vector3.left;
            }

            if (verticalAxisDown)
            {
                if (verticalAxis > 0) direction = Vector3.up;
                else if (verticalAxis < 0) direction = Vector3.down;
            }

            if (direction != Vector3.zero)
            {
                GameObject currentGameObject = this.characters[currentIndex].gameObject;
                GameObject nextGameObject = currentGameObject.FindSelectableGameObject(
                    direction,
                    this.wrapInput,
                    this.characterButtonsWhiteList
                );

                if (nextGameObject != null && nextGameObject != currentGameObject)
                {
                    int index = -1;

                    for (int i = 0; i < this.characters.Length; ++i)
                    {
                        if (this.characters[i].gameObject == nextGameObject)
                        {
                            index = i;
                            break;
                        }
                    }

                    this.MoveCursor(player, index);
                }
            }
        }
    }

    protected virtual void TryDeselectCharacter(AudioClip sound)
    {
        this.TryDeselectCharacter();
    }

    protected virtual void TrySelectCharacter(AudioClip sound)
    {
        this.TrySelectCharacter();
    }
    #endregion
}
