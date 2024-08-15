using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using FPLibrary;
using UFE3D;
using Unity.VisualScripting;

public class DefaultStageSelectionScreen : StageSelectionScreen
{
    public GameObject Stage;
    public GameObject StageButtonsNew;
    public Text namePlayer1;
    public Text namePlayer2;
    public Text nameStage;
    public Image portraitPlayer1;
    public Image portraitPlayer2;
    public Image screenshotStage;
    public Text titleStage;
    public MyStageSelection MyStageSelection;
    protected GameObject gameObjectPlayer1;
    protected GameObject gameObjectPlayer2;
    public Vector3 positionPlayer1 = new Vector3(-4, 0, 0);
    public Vector3 positionPlayer2 = new Vector3(4, 0, 0);

    #region public instance methods
    public virtual void NextStage()
    {
        if (this.moveCursorSound != null) UFE.PlaySound(this.moveCursorSound);
        this.SetHoverIndex((this.stageHoverIndex + 1) % UFE.config.stages.Length);
    }

    public virtual void PreviousStage()
    {
        int length = UFE.config.stages.Length;
        if (this.moveCursorSound != null) UFE.PlaySound(this.moveCursorSound);
        this.SetHoverIndex((this.stageHoverIndex + length - 1) % length);
    }

    public override void SetHoverIndex(int stageIndex)
    {
        int length = UFE.config.stages.Length;

        if (stageIndex >= 0 && stageIndex < length)
        {
            StageOptions stage = UFE.config.stages[stageIndex];
            base.SetHoverIndex(stageIndex);

            if (this.titleStage != null) this.titleStage.text = stage.stageName;
            if (this.nameStage != null) this.nameStage.text = stage.stageName;
            if (this.screenshotStage != null)
            {
                this.screenshotStage.sprite = Sprite.Create(
                    stage.screenshot,
                    new Rect(0f, 0f, stage.screenshot.width, stage.screenshot.height),
                    new Vector2(0.5f * stage.screenshot.width, 0.5f * stage.screenshot.height)
                );
            }
        }
    }
    #endregion

    #region public override methods
    public override void DoFixedUpdate(
    IDictionary<InputReferences, InputEvents> player1PreviousInputs,
    IDictionary<InputReferences, InputEvents> player1CurrentInputs,
    IDictionary<InputReferences, InputEvents> player2PreviousInputs,
    IDictionary<InputReferences, InputEvents> player2CurrentInputs
)
    {
        this.SpecialNavigationSystem(
            player1PreviousInputs,
            player1CurrentInputs,
            player2PreviousInputs,
            player2CurrentInputs,
            new UFEScreenExtensions.MoveCursorCallback(this.HighlightStage),
            new UFEScreenExtensions.ActionCallback(MyStageSelection.OnSelectPress),
            new UFEScreenExtensions.ActionCallback(this.TryDeselectStage)
        );
        
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (Stage.activeInHierarchy)
            {
                PreviousStage();
            }
        }
        // Check if the right arrow key is pressed
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (Stage.activeInHierarchy)
            {
                NextStage();
            }
        }
    }

    public override void OnShow()
    {
        base.OnShow();

        if (UFE.config.player1Character != null)
        {
            if (this.portraitPlayer1 != null)
            {
                this.portraitPlayer1.transform.GetChild(UFE.config.player1Character.age).gameObject.SetActive(true);
                // this.portraitPlayer1.sprite = Sprite.Create(
                //     UFE.config.player1Character.profilePictureBig,
                //     new Rect(0f, 0f, UFE.config.player1Character.profilePictureBig.width, UFE.config.player1Character.profilePictureBig.height),
                //     new Vector2(0.5f * UFE.config.player1Character.profilePictureBig.width, 0.5f * UFE.config.player1Character.profilePictureBig.height)
                // );
            }
            CreatePlayer1();
            if (this.namePlayer1 != null)
            {
                this.namePlayer1.text = UFE.config.player1Character.characterName.ToUpper();
            }
        }

        if (UFE.config.player2Character != null)
        {
            if (this.portraitPlayer2 != null)
            {
                this.portraitPlayer2.transform.GetChild(UFE.config.player2Character.age).gameObject.SetActive(true);
                // this.portraitPlayer2.sprite = Sprite.Create(
                //     UFE.config.player2Character.profilePictureBig,
                //     new Rect(0f, 0f, UFE.config.player2Character.profilePictureBig.width, UFE.config.player2Character.profilePictureBig.height),
                //     new Vector2(0.5f * UFE.config.player2Character.profilePictureBig.width, 0.5f * UFE.config.player2Character.profilePictureBig.height)
                // );
            }
            CreatePlayer2();
            if (this.namePlayer2 != null)
            {
                this.namePlayer2.text = UFE.config.player2Character.characterName.ToUpper();
            }
        }

        this.stageHoverIndex = 0;
        StageOptions stage = UFE.config.stages[this.stageHoverIndex];

        if (stage != null)
        {
            if (this.screenshotStage != null)
            {
                this.screenshotStage.sprite = Sprite.Create(
                    stage.screenshot,
                    new Rect(0f, 0f, stage.screenshot.width, stage.screenshot.height),
                    new Vector2(0.5f * stage.screenshot.width, 0.5f * stage.screenshot.height)
                );
            }

            if (this.nameStage != null)
            {
                this.nameStage.text = stage.stageName;
            }
            if (this.titleStage != null)
            {
                this.titleStage.text = stage.stageName;
            }
        }
    }
    public void CreatePlayer1()
    {
        //if (this.displayMode == DisplayMode.CharacterGameObject)
        {
            UFE3D.CharacterInfo characterInfo = UFE.config.player1Character;
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
            //portraitPlayer1.sprite = gameObjectPlayer1.GetComponent<SpriteRenderer>().sprite;

        }
    }

    public void CreatePlayer2()
    {
        UFE3D.CharacterInfo characterInfo = UFE.config.player2Character;
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
            //portraitPlayer2.sprite = gameObjectPlayer2.GetComponent<SpriteRenderer>().sprite;

        }
    }
    #endregion

    #region protected instance methods: methods required by the Special Navigation System (GUI)
    protected virtual void HighlightStage(
        Fix64 horizontalAxis,
        Fix64 verticalAxis,
        bool horizontalAxisDown,
        bool verticalAxisDown,
        bool confirmButtonDown,
        bool cancelButtonDown,
        AudioClip sound
    )
    {
        if (verticalAxisDown)
        {
            if (verticalAxis > 0)
            {
                //this.PreviousStage();
            }
            else if (verticalAxis < 0)
            {
                //this.NextStage();
            }
        }
    }

    protected virtual void TryDeselectStage(AudioClip sound)
    {
        this.TryDeselectStage();
    }

    protected virtual void TrySelectStage(AudioClip sound)
    {
        this.TrySelectStage();
    }
    #endregion
}
