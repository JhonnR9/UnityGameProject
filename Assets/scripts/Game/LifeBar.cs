using Game.Events;
using UnityEngine;
using System;
using UnityEngine.UI;

public class LifeBar : Actor
{
    private string playerId;
    [SerializeField]
    private float MaxLife = 100f;
    [SerializeField]
    private float AmountLife = 100f;

    private float MinWidth;
    private float MaxWidth;
    private float minPosition;
    private float maxPosition;

    private float width;
    private float position;

    private RectTransform RectTransform;
    private Image image;

    [SerializeField] 
    private Color startColor;
    [SerializeField]
    private Color endColor;

    public override void Awake()
    {
        base.Awake();
        RectTransform = GetComponent<RectTransform>();
        image = GetComponent<Image>();

        MaxWidth = RectTransform.sizeDelta.x;
        maxPosition = RectTransform.anchoredPosition.x;

        MinWidth = 0;
        minPosition = 0;
    }

    public override void Start()
    {
        base.Start();

        UpdateLifeBar();

    }

    public override void BindEvents(EventManager e)
    {
        base.BindEvents(e);

        e.GetEventHandle<CharacterEventHandle>().OnCharacterDamage += OnDamage; 
        e.GetEventHandle<PlayerEventHandle>().OnPlayerGenerateID += OnPlayerGenerateID;

    }

    private void OnPlayerGenerateID (string id, CharacterStatus status)
    {
        playerId = id;
        MaxLife = status.MaxLife;
        AmountLife = status.Life;
      
    }
    public void OnDamage(string id, float damageAmount)
    {
      
        if (id == playerId)
        {
            AmountLife -= damageAmount;
            UpdateLifeBar();
        }

    }

    public void UpdateLifeBar()
    {
        float lifePercentage = AmountLife / MaxLife;

        width = Mathf.Lerp(MinWidth, MaxWidth, lifePercentage);
        position = Mathf.Lerp(minPosition, maxPosition, lifePercentage);

        Vector3 newPosition = RectTransform.anchoredPosition;
        newPosition.x = position;

        RectTransform.anchoredPosition = newPosition;
        RectTransform.sizeDelta = new Vector2(width, RectTransform.sizeDelta.y);

        UpdateColor(lifePercentage);
    }

    private void UpdateColor(float lifePercentage)
    {
        var newColor = Color.Lerp(endColor, startColor, lifePercentage);
        newColor.a = 1;
        image.color = newColor;
    }
}
