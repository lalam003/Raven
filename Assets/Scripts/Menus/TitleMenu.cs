using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleMenu : MenuBase
{
    public bool StartGameAnimation = false;
    [SerializeField]
    private Transform AirplaneSprite;
    [SerializeField]
    private Transform EndPosition;
    private Vector3 StartPosition;
    [SerializeField]
    private float AnimationTime = 3;
    [SerializeField]
    private GameObject TitleSprites;

    protected override void Awake()
    {
        StartPosition = AirplaneSprite.transform.localPosition;
        base.Awake();
        Blackboard.Title = this;
    }
    
    protected override void OnEnable()
    {
        TitleSprites.SetActive(true);
        SetMenuInput();
        Blackboard.Player.PlayerMovement.Menu = () => { };
        currentText.color = selectedColor;
    }

    protected override void toggleMenuUp()
    {
        // does nothing
    }

    protected override void toggleMenuDown()
    {
        // does nothing
    }

    protected override void toggleMenuLeft()
    {
        base.toggleMenuUp();
    }

    protected override void toggleMenuRight()
    {
        base.toggleMenuDown();
    }

    public override void closeMenu()
    {
        if (!StartGameAnimation)
        {
            base.closeMenu();
        }
        else
        {
            Blackboard.Player.PlayerMovement.canMove = false;
            StartCoroutine(AirplaneAnimation());
        }
    }

    public void showTitleSprites()
    {
        TitleSprites.SetActive(false);
    }

    private IEnumerator AirplaneAnimation()
    {
        // Animate airplane
        float startTime = Time.time;
        Vector3 startPos = AirplaneSprite.transform.localPosition;
        while ((Time.time - startTime) < (AnimationTime/2))
        {
            float frac = ((Time.time - startTime) / (AnimationTime / 2));
            frac *= (frac * 1.1f) + 0.01f;
            AirplaneSprite.transform.localPosition = startPos - (startPos - EndPosition.localPosition) * frac;
            yield return null;
        }
        // Fade to black
        startTime = Time.time;
        while ((Time.time - startTime) < (AnimationTime / 4))
        {
            float frac = ((Time.time - startTime) / (AnimationTime / 4));
            Blackboard.GM.BlackScreen.color = new Color(Blackboard.GM.BlackScreen.color.r, Blackboard.GM.BlackScreen.color.g, Blackboard.GM.BlackScreen.color.b, frac);
            yield return null;
        }
        TitleSprites.SetActive(false);
        // Fade to game
        startTime = Time.time;
        while ((Time.time - startTime) < (AnimationTime / 4))
        {
            float frac = ((Time.time - startTime) / (AnimationTime / 4));
            Blackboard.GM.BlackScreen.color = new Color(Blackboard.GM.BlackScreen.color.r, Blackboard.GM.BlackScreen.color.g, Blackboard.GM.BlackScreen.color.b, (1-frac));
            yield return null;
        }
        AirplaneSprite.transform.localPosition = StartPosition;
        base.closeMenu();
        Blackboard.Player.PlayerMovement.canMove = true;
    }
}