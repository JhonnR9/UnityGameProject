using Game.Events;
using UnityEngine;
using UnityEngine.UI;

public class LifeBar : MonoBehaviour
{
    float MaxLife = 100f;
    float AmountLife = 100f;

    private float MinWidth;
    private float MaxWidth;
    private float minPosition;
    private float maxPosition;

    private float width;
    private float position;

    private RectTransform RectTransform;
    private Image image;

    private void Awake()
    {
        RectTransform = GetComponent<RectTransform>();
        image = GetComponent<Image>();

        MaxWidth = RectTransform.sizeDelta.x;
        maxPosition = RectTransform.anchoredPosition.x;

        MinWidth = 0;
        minPosition = 0;
    }

    void Start()
    {
        var handle = EventManager.Instance.GetEventHandle<CharacterEventHandle>();
        handle.OnCharacterDamage += OnDamage;
        UpdateLifeBar();
    }

    public void OnDamage(string id, float damageAmount)
    {
        AmountLife -= damageAmount;
        UpdateLifeBar();
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
        Color startColor = Color.green; 
        Color endColor = Color.red; 

        image.color = Color.Lerp(endColor, startColor, lifePercentage);

    }
}
