using NUnit.Framework.Interfaces;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [SerializeField] private GameObject _mainMenu;
    [SerializeField] private GameObject _inGame;
    [SerializeField] private GameObject _boss;
    [SerializeField] private GameObject _gameOver;

    [SerializeField] private GameObject _player;

    [SerializeField] private TextMeshProUGUI _hpText;
    [SerializeField] private int _hp;
	[SerializeField] private TextMeshProUGUI _bossHpText;
	[SerializeField] private int _bossHp;
	[SerializeField] private TextMeshProUGUI _timeLimitText;
	[SerializeField] private float _timeLimit;
    [SerializeField] private TextMeshProUGUI _gameOverText;

	[SerializeField] private AudioSource _winSound;
	[SerializeField] private AudioSource _loseSound;
    [SerializeField] private AudioSource _backgroundMusic;

	void Start()
    {
        if (Instance == null) Instance = this;
        UpdateHp(_hp);
        UpdateBossHp(_bossHp);
    }

    public void StartGame()
    {
        // Disable main menu
        _mainMenu.SetActive(false);
        // Enable player
        _player.SetActive(true);
        // Enable in game UI
        _inGame.SetActive(true);
        // Play music
        if (_backgroundMusic != null) _backgroundMusic.Play();
    }

    public void StartBoss()
    {
        _boss.SetActive(true);
    }

    public void GameOver(bool win)
    {
        // Disable in game UI
        _inGame.SetActive(false);
        // Enable Game Over UI
        _gameOver.SetActive(true);
        // Disable Player
        _player.SetActive(false);
        // Set game over text
        if (win)
        {
            _gameOverText.text = "\n" + "You Win";
			if (_winSound != null) _winSound.Play();
			if (_backgroundMusic != null) _backgroundMusic.Stop();
		}
        if (!win)
        {
            _gameOverText.text = "\n" + "You Lose";
            if (_loseSound != null) _loseSound.Play();
			if (_backgroundMusic != null) _backgroundMusic.Stop();
        }
	}

	public void UpdateHp(int hp)
    {
        _hpText.text = "HP: " + hp;
    }

    public void UpdateBossHp(int hp)
    {
        _bossHpText.text = "HP: " + hp;
	}

    private void UpdateTimeLimit()
    {
		if (_timeLimit > 0 && _inGame.activeSelf == true)
		{
			_timeLimit -= Time.deltaTime;
			_timeLimitText.text = _timeLimit.ToString();
		}
        if (_timeLimit <= 0)
        {
            GameOver(false);
        }
    }

    void Update()
    {
        UpdateTimeLimit();
    }
}
