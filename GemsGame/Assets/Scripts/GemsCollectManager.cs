using TMPro;
using UnityEngine;

public class GemsCollectManager : MonoBehaviour
{
    [SerializeField] private AudioSource _collectSound;
    [SerializeField] private AudioSource _doorOpenSound;

    [SerializeField] private GameObject _yellowDoor;
    [SerializeField] private GameObject _greenDoor;
    [SerializeField] private GameObject _redDoor;

    [SerializeField] private TMP_Text _yellowGemsCounter;
    [SerializeField] private TMP_Text _greenGemsCounter;
    [SerializeField] private TMP_Text _redGemsCounter;

    private int _yellowGemsCount = 0;
    private int _greenGemsCount = 0;
    private int _redGemsCount = 0;
    
    private void OnEnable()
    {
        EventsControl.OnGemPicked += GemsCollectAction;
    }

    private void OnDisable()
    {
        EventsControl.OnGemPicked -= GemsCollectAction;
    }

    private void GemsCollectAction(Collider gem)
    {
        string gemName = gem.gameObject.name;

        _collectSound.Play();
        gem.gameObject.SetActive(false);

        switch (gemName)
        {
            case "YellowGem":
                _yellowGemsCount++;
                _yellowGemsCounter.text = $"Желтых камней собрано: {_yellowGemsCount}/4";

                if (_yellowGemsCount == 4)
                    OpenDoor(_yellowDoor);
                break;
            case "GreenGem":
                _greenGemsCount++;
                _greenGemsCounter.text = $"Зелёных камней собрано: {_greenGemsCount}/3";

                if (_greenGemsCount == 3)
                    OpenDoor(_greenDoor);
                break;
            case "RedGem":
                _redGemsCount++;
                _redGemsCounter.text = $"Красных камней собрано: {_redGemsCount}/3";

                if (_redGemsCount == 3)
                    OpenDoor(_redDoor);
                break;
        }
    }

    private void OpenDoor(GameObject door)
    {
        _doorOpenSound.Play();
        door.SetActive(false);
    }
}
