using UnityEngine;

public class CharacterStatus : MonoBehaviour
{
    [SerializeField]
    private float maxLife = 100f;
    [SerializeField]
    private float life = 100f;
    [SerializeField]
    private float damage = 10f; // Valor do dano que o personagem pode causar

    // Getter e Setter para 'Life'
    public float Life
    {
        get { return life; }
        set
        {
            life = Mathf.Clamp(value, 0, maxLife);
        }
    }

    // Getter e Setter para 'MaxLife'
    public float MaxLife
    {
        get { return maxLife; }
        set
        {
            maxLife = Mathf.Max(1, value);
            life = Mathf.Clamp(life, 0, maxLife);
        }
    }

    // Getter e Setter para 'Damage'
    public float Damage
    {
        get { return damage; }
        set
        {
            // Garantir que o dano seja pelo menos 0 (não pode causar dano negativo)
            damage = Mathf.Max(0, value);
        }
    }
}
