using UnityEngine;
using UnityEngine.Events;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] int _startingHealth = 5;
    [SerializeField] int _startingAttack = 2;
    [SerializeField] int _startingMagic = 1;

    [Space(10)]
    [SerializeField] UnityEvent<string> _onHealthChanged = new UnityEvent<string>();
    [SerializeField] UnityEvent<string> _onAttackChanged = new UnityEvent<string>();
    [SerializeField] UnityEvent<string> _onMagicChanged = new UnityEvent<string>();

    int _currentHealth;
    int _currentAttack;
    int _currentMagic;

    void Awake()
    {
        _currentHealth = _startingHealth;
        _currentAttack = _startingAttack;
        _currentMagic = _startingMagic;

        _onHealthChanged?.Invoke(_currentHealth.ToString());
        _onAttackChanged?.Invoke(_currentAttack.ToString());
        _onMagicChanged?.Invoke(_currentMagic.ToString());
    }

    public void ModifyStatsItemAdded(ItemSO item)
    {
        _currentAttack += item.Attack;
        _currentMagic += item.Magic;

        _onAttackChanged?.Invoke(_currentAttack.ToString());
        _onMagicChanged?.Invoke(_currentMagic.ToString());
    }

    public void ModifyStatsItemRemoved(ItemSO item)
    {
        _currentAttack -= item.Attack;
        _currentMagic -= item.Magic;

        _onAttackChanged?.Invoke(_currentAttack.ToString());
        _onMagicChanged?.Invoke(_currentMagic.ToString());
    }

    public void ModifyHealth(ItemSO item)
    {
        _currentHealth += item.HealthRestored;
        _onHealthChanged?.Invoke(_currentHealth.ToString());   
    }
}
