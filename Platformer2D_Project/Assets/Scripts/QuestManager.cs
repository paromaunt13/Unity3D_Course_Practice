using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class QuestManager : MonoBehaviour
{   
    [SerializeField] private TMP_Text _firstGoal;
    [SerializeField] private TMP_Text _secondGoal;
    [SerializeField] private GameObject _questCompleteDialogue;
    [SerializeField] private GameObject _exclamationSign;
    [SerializeField] private List<GameObject> _wraithSouls;
    [SerializeField] private GameObject _darkForestLocation;
    [SerializeField] private GameObject _reapersCaveLocation;

    private int _mushroomCount = 0;
    private int _mushroomTotalAmount = 0;

    private int _wraithSoulCount = 0;
    private int _wraithSoulTotalAmount = 0;

    private void Start()
    {
        SetCollectableItemsAmount();
        foreach (var item in _wraithSouls)
            item.SetActive(false);
        _darkForestLocation.SetActive(false);
        _reapersCaveLocation.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        Debug.Log(collider.gameObject);
        if (collider.CompareTag("CollectableItem"))
        {
            QuestCompletion(collider.gameObject.name);
            Destroy(collider.gameObject);
        }
    }

    void SetCollectableItemsAmount()
    {
        var collectableItemTotalAmount = GameObject.FindGameObjectsWithTag("CollectableItem");

        var mushrooms = new List<GameObject>();
        var wraithSouls = new List<GameObject>();

        foreach (var item in collectableItemTotalAmount.Where(g => g.name == "Mushroom"))
            mushrooms.Add(item);
        foreach (var item in collectableItemTotalAmount.Where(g => g.name == "WraithSoul"))
            wraithSouls.Add(item);

        _mushroomTotalAmount = mushrooms.Count;
        _wraithSoulTotalAmount = wraithSouls.Count;

        foreach (var item in _wraithSouls)
        {
            item.SetActive(false);
        }

        _firstGoal.text = $"- Синие грибы: {_mushroomCount}/{_mushroomTotalAmount}";
        _secondGoal.text = $"- Призрачные души: {_wraithSoulCount}/{_wraithSoulTotalAmount}";

    }

    public void QuestCompletion(string name)
    {
        switch (name)
        {
            case "Mushroom":
                _mushroomCount++;
                _firstGoal.text = $"- Синие грибы: {_mushroomCount}/{_mushroomTotalAmount}";
                if (_mushroomCount >= _mushroomTotalAmount)
                {
                    _mushroomCount = _mushroomTotalAmount;
                    _firstGoal.text = $"- Синие грибы: {_mushroomCount}/{_mushroomTotalAmount}";
                    _firstGoal.color = new Color(0, 255, 213);
                }
                break;
            case "WraithSoul":
                _wraithSoulCount++;
                _secondGoal.text = $"- Призрачные души: {_wraithSoulCount}/{_wraithSoulTotalAmount}";
                if (_wraithSoulCount >= _wraithSoulTotalAmount)
                {
                    _wraithSoulCount = _wraithSoulTotalAmount;
                    _secondGoal.text = $"- Призрачные души: {_wraithSoulCount}/{_wraithSoulTotalAmount}";
                    _secondGoal.color = new Color(0, 255, 213);
                }
                break;
            default:
                break;
        }
        if (_mushroomCount == _mushroomTotalAmount && _wraithSoulCount == _wraithSoulTotalAmount)
        {
            _questCompleteDialogue.SetActive(true);
            _exclamationSign.SetActive(true);
        }           
    }
}
