using Game.Events;
using UnityEngine;
using UnityEngine.UI;

public class LifeBar : MonoBehaviour
{
    float MaxLife = 100f;
    float AmountLife = 100f;

    private float MinWidth = 0;
    private float MaxWidth = 1.139f;
    private float minPosition = -161.693f;
    private float maxPosition = -17.42505f;

    private float width;
    private float position;

    private RectTransform RectTransform;
    private Image image;

    private void Awake()
    {
        RectTransform = GetComponent<RectTransform>();
        image = GetComponent<Image>();
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

        //atualiza tudo
        Vector3 newPosition = RectTransform.anchoredPosition;
        newPosition.x = position;

        RectTransform.anchoredPosition = newPosition;
        RectTransform.sizeDelta = new Vector2(width, RectTransform.sizeDelta.y);

        float h, s, v;


    }



}
