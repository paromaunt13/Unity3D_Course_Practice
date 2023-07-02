using System.Collections;
using TMPro;
using UnityEngine;

public class CatapultLauncher : MonoBehaviour
{
    [SerializeField] private Rigidbody _weight;
    [SerializeField] private float _timeToLaunch;
    [SerializeField] private TMP_Text _timer;
    private bool isTimerStarted = false;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Core"))
        {
            _timer.gameObject.SetActive(true);
            isTimerStarted = true;
            StartCoroutine(CatapultLaunch());
        }
    }

    private void Start()
    {        
        _timer.text = _timeToLaunch.ToString("00:00");
    }

    private void Update()
    {
        if (isTimerStarted)
        {
            _timeToLaunch -= Time.deltaTime;
            _timer.text = _timeToLaunch.ToString("00:00");
            if (_timeToLaunch <= 0)
            {
                _timeToLaunch = 0;
            }
        }
    }
    private IEnumerator CatapultLaunch()
    {
        yield return new WaitForSeconds(_timeToLaunch);      
        _weight.isKinematic = false;
    }
}
