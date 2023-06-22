using DG.Tweening;
using System.Collections;
using UnityEngine;

public class SwordEnhancer : MonoBehaviour
{
    [SerializeField] private GameObject _sword;
    [SerializeField] private GameObject _slashFX;
    [SerializeField] private ParticleSystem _enhanceEffect;

    [SerializeField] private AudioSource _currentAttackSound;
    [SerializeField] private AudioSource _enhanceAttackSound;

    private int _currentDamage;
    private int _enhanceDamage;
    private int _damageMultiplier = 8;

    private void Start()
    {
        gameObject.TryGetComponent<PlayerCombat>(out var playerCombat);
        _currentDamage = playerCombat._damage;
        _enhanceDamage = _currentDamage * _damageMultiplier;

        _currentAttackSound = playerCombat._attackSound;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(1))
            StartCoroutine(SwordEnhance());
    }

    IEnumerator SwordEnhance()
    {
        gameObject.TryGetComponent<PlayerCombat>(out var playerCombat);

        playerCombat._damage = _enhanceDamage;
        playerCombat._attackSound = _enhanceAttackSound;

        _enhanceEffect.gameObject.SetActive(true);
        _enhanceEffect.Play();

        _slashFX.GetComponent<Transform>().DOScale(3f, 1f);
        _slashFX.GetComponent<SpriteRenderer>().color = new Color(255, 0, 0);

        _sword.GetComponent<Transform>().DOScale(4f, 1f);
        _sword.GetComponent<SpriteRenderer>().color = new Color(255, 0, 0);

        yield return new WaitForSeconds(3f);

        playerCombat._damage = _currentDamage;
        playerCombat._attackSound = _currentAttackSound;

        _enhanceEffect.Stop();
        _enhanceEffect.gameObject.SetActive(false);

        _slashFX.GetComponent<Transform>().DOScale(1, 1f);
        _slashFX.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255);

        _sword.GetComponent<Transform>().DOScale(3.7f, 1f);
        _sword.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255);
    }
}
