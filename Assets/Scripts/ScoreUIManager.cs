using UnityEngine;
using TMPro;

// Класс для управления интерфейсом пользователя (UI), отображающим счет игры
public class ScoreUIManager : MonoBehaviour
{
    // Текстовые элементы для отображения текущего счета игрока, компьютера и статуса текущего хода
    public TextMeshProUGUI playerScoreText;
    public TextMeshProUGUI computerScoreText;
    public TextMeshProUGUI currentPlayerText;

    // Массивы текстовых элементов для отображения счета по каждому фрейму (10 фреймов) для игрока и компьютера
    public TextMeshProUGUI[] playerFrameScores; // Массив для игрока
    public TextMeshProUGUI[] computerFrameScores; // Массив для компьютера

    // Счетчики пропущенных ячеек для корректного отображения страйков
    public int playerSkip = 0;
    public int computerSkip = 0;

    // Ссылки на компоненты GameManager и BallController
    private GameManager GM;
    private BallController ball;

    // Метод, вызываемый при старте, для получения ссылок на управляющие компоненты
    void Start()
    {
        GM = FindObjectOfType<GameManager>(); // Находим объект GameManager в сцене
        ball = FindObjectOfType<BallController>(); // Находим объект BallController в сцене
    }

    // Метод обновления, вызываемый каждый кадр, обновляет UI
    void Update()
    {
        UpdateUI(); // Обновляем текстовые элементы интерфейса
    }

    // Метод обновления текстовых элементов интерфейса
    public void UpdateUI()
    {
        playerScoreText.text = $"Игрок: {GM.playerScore}"; // Обновляем счет игрока
        computerScoreText.text = $"Компьютер: {GM.computerScore}"; // Обновляем счет компьютера
        currentPlayerText.text = ball.isPlayerTurn ? "Ход игрока" : "Ход компьютера"; // Обновляем текущий ход
    }

    // Метод для отображения счета игрока в текстовых элементах
    public void DisplayPlayerScore(int rollIndex, int pinsKnockedDown)
    {
        bool isSpare = rollIndex > 0 && (rollIndex + playerSkip) % 2 != 0 && (GM.playerRolls[rollIndex - 1] + pinsKnockedDown == 10); // Проверяем на спэр 

        // Проверяем различные условия для обновления текста ячейки
        if (pinsKnockedDown == 10 && (rollIndex + playerSkip) % 2 == 0) // Страйк
        {
            playerFrameScores[rollIndex + playerSkip].text = "X";
            playerSkip++; // Увеличиваем счетчик пропущенных ячеек
        }
        else if (pinsKnockedDown == 0) // Пустой бросок
        {
            playerFrameScores[rollIndex + playerSkip].text = "-";
        }
        else if (isSpare) // Спэр
        {
            playerFrameScores[rollIndex + playerSkip].text = "/";
        }
        else // Количество сбитых кеглей
        {
            playerFrameScores[rollIndex + playerSkip].text = pinsKnockedDown.ToString();
        }
    }

    // Метод для отображения счета компьютера в текстовых элементах
    public void DisplayComputerScore(int rollIndex, int pinsKnockedDown)
    {
        bool isSpare = rollIndex > 0 && (rollIndex + computerSkip) % 2 != 0 && (GM.computerRolls[rollIndex - 1] + pinsKnockedDown == 10); // Проверяем на спэр

        // Проверяем различные условия для обновления текста ячейки
        if (pinsKnockedDown == 10 && (rollIndex + computerSkip) % 2 == 0) // Страйк
        {
            computerFrameScores[rollIndex + computerSkip].text = "X";
            computerSkip++; // Увеличиваем счетчик пропущенных ячеек
        }
        else if (pinsKnockedDown == 0) // Пустой бросок
        {
            computerFrameScores[rollIndex + computerSkip].text = "-";
        }
        else if (isSpare) // Спэр
        {
            computerFrameScores[rollIndex + computerSkip].text = "/";
        }
        else // Количество сбитых кеглей
        {
            computerFrameScores[rollIndex + computerSkip].text = pinsKnockedDown.ToString();
        }
    }
}