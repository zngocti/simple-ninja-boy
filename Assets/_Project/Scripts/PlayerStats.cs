using UnityEngine;
using UnityEngine.Events;
using UnityEngine.TextCore;

[RequireComponent(typeof(PersistentUniqueID))]
public class PlayerStats : MonoBehaviour, ISaveableData
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

    public int CurrentHealth { get => _currentHealth; }
    public int CurrentAttack { get => _currentAttack; }
    public int CurrentMagic { get => _currentMagic; }

    PersistentUniqueID _persistentID;

    public string SaveID
    {
        get
        {
            if (_persistentID == null)
            {
                _persistentID = GetComponent<PersistentUniqueID>();
            }

            return _persistentID.ID;
        }
    }

    void Awake()
    {
        _currentHealth = _startingHealth;
        _currentAttack = _startingAttack;
        _currentMagic = _startingMagic;

        _onHealthChanged?.Invoke(_currentHealth.ToString());
        _onAttackChanged?.Invoke(_currentAttack.ToString());
        _onMagicChanged?.Invoke(_currentMagic.ToString());

        if (!_persistentID)
        {
            _persistentID = GetComponent<PersistentUniqueID>();
        }
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

    public void OnLoad(SaveManager manager)
    {
        Vector3Int stats;
        if (manager.PlayerStatsData.Stats(_persistentID.ID, out stats))
        {
            _currentHealth = stats.x;
            _currentAttack = stats.y;
            _currentMagic = stats.z;
        }
    }

    public void OnSave(SaveManager manager)
    {
        manager.PlayerStatsData.SaveData(this);
    }

    public void SetVariablesToSave()
    {
        if (!_persistentID)
        {
            _persistentID = GetComponent<PersistentUniqueID>();
        }
    }
}
