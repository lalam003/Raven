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
    [SerializeField]
    private Image BlackScreen;

    protected override void Awake()
    {
        StartPosition = AirplaneSprite.transform.localPosition;
        base.Awake();
        Blackboard.Title = this;
    }
    
    protected override void OnEnable()
    {
        base.OnEnable();
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
            StartCoroutine(AirplaneAnimation());
        }
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
            BlackScreen.color = new Color(BlackScreen.color.r, BlackScreen.color.g, BlackScreen.color.b, frac);
            yield return null;
        }
        // Fade to game
        TitleSprites.SetActive(false);
        startTime = Time.time;
        while ((Time.time - startTime) < (AnimationTime / 4))
        {
            float frac = ((Time.time - startTime) / (AnimationTime / 4));
            BlackScreen.color = new Color(BlackScreen.color.r, BlackScreen.color.g, BlackScreen.color.b, (1-frac));
            yield return null;
        }
        AirplaneSprite.transform.localPosition = StartPosition;
        base.closeMenu();
    }
}