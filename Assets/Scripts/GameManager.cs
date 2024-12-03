using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

// Основной класс, управляющий логикой игры
public class GameManager : MonoBehaviour
{
    public int currentFrame = 1; // Текущий фрейм (от 1 до 10)
    public int currentRoll = 1; // Текущий бросок (1 или 2 в рамках одного фрейма)
    private int playerRollIndex = 0; // Индекс текущего броска игрока в массиве бросков
    private int computerRollIndex = 0; // Индекс текущего броска компьютера в массиве бросков
    public int[] playerRolls = new int[21]; // Массив бросков игрока (максимум 21 бросок)
    public int[] computerRolls = new int[21]; // Массив бросков компьютера (максимум 21 бросок)

    public int playerScore = 0; // Текущий счёт игрока
    public int computerScore = 0; // Текущий счёт компьютера

    private BallController ball; // Ссылка на компонент управления шаром
    public Pin[] pins; // Ссылки на все кегли
    private ScoreUIManager UI; // Ссылка на менеджер пользовательского интерфейса

    // Инициализация игры
    void Start()
    {
        pins = FindObjectsOfType<Pin>(); // Находим все кегли на сцене
        ball = FindObjectOfType<BallController>(); // Находим шар на сцене
        UI = FindObjectOfType<ScoreUIManager>(); // Находим интерфейсный менеджер
    }

    // Запись результатов после броска
    public void RecordPinsAfterThrow()
    {
        int pinsKnockedDown = 0; // Счётчик сбитых кеглей

        foreach (Pin pin in pins)
        {
            if (pin.hasFallen && !pin.IsFallen) // Если кегля упала и ещё не была учтена
            {
                pinsKnockedDown++;
                pin.IsFallen = true; // Отмечаем, что кегля сбита
            }
        }

        // Записываем результат в зависимости от текущего игрока
        if (ball.isPlayerTurn)
        {
            RecordPlayerRoll(pinsKnockedDown);
        }
        else
        {
            RecordComputerrRoll(pinsKnockedDown);
        }
    }

    // Запись броска игрока
    public void RecordPlayerRoll(int pinsKnockedDown)
    {
        playerRolls[playerRollIndex] = pinsKnockedDown; // Сохраняем результат броска
        bool isSpare = currentRoll == 2 && (playerRolls[playerRollIndex - 1] + pinsKnockedDown == 10); // Проверяем на спэр
        UI.DisplayPlayerScore(playerRollIndex, pinsKnockedDown, isSpare); // Обновляем интерфейс

        playerRollIndex++;

        Debug.Log($"Бросок игрока {currentRoll} в фрейме {currentFrame}: сбито {pinsKnockedDown} кеглей.");

        // Обработка завершения броска
        if (currentRoll == 1 && pinsKnockedDown == 10) // Страйк
        {
            ball.isPlayerTurn = false; // Передаём ход компьютеру
            currentRoll = 1;
            ball.ResetBall();
            ResetPins();
        }
        else if (currentRoll == 2) // Спэр или второй бросок завершён
        {
            ball.isPlayerTurn = false; // Передаём ход компьютеру
            currentRoll = 1;
            ball.ResetBall();
            ResetPins();
        }
        else
        {
            currentRoll++; // Переходим ко второму броску
            Shreder(); // Убираем сбитые кегли
            ball.ResetBall();
        }

        CalculatePlayerScore(); // Пересчитываем счёт игрока
    }

    // Запись броска компьютера
    public void RecordComputerrRoll(int pinsKnockedDown)
    {
        computerRolls[computerRollIndex] = pinsKnockedDown; // Сохраняем результат броска
        bool isSpare = currentRoll == 2 && (computerRolls[computerRollIndex - 1] + pinsKnockedDown == 10); // Проверяем на спэр
        UI.DisplayComputerScore(computerRollIndex, pinsKnockedDown, isSpare); // Обновляем интерфейс

        computerRollIndex++;

        Debug.Log($"Бросок компьютера {currentRoll} в фрейме {currentFrame}: сбито {pinsKnockedDown} кеглей.");

        // Обработка завершения броска
        if (currentRoll == 1 && pinsKnockedDown == 10) // Страйк
        {
            ball.isPlayerTurn = true; // Передаём ход игроку
            NextFrame(); // Переходим к следующему фрейму
        }
        else if (currentRoll == 2) // Спэр или второй бросок завершён
        {
            ball.isPlayerTurn = true; // Передаём ход игроку
            NextFrame(); // Переходим к следующему фрейму
        }
        else
        {
            currentRoll++; // Переходим ко второму броску
            Shreder(); // Убираем сбитые кегли
            ball.ResetBall();
        }

        CalculateComputerScore(); // Пересчитываем счёт компьютера
    }

    // Переход к следующему фрейму
    private void NextFrame()
    {
        currentFrame++;
        currentRoll = 1;

        if (currentFrame == 11) // Если фрейм 11-й, игра завершена
        {
            StartCoroutine(WaitBeforeEnd());
        }
        else
        {
            ball.ResetBall();
            ResetPins();
        }
    }

    // Подсчёт очков игрока
    private void CalculatePlayerScore()
    {
        int score = 0;
        int rollIndex = 0;

        for (int frame = 0; frame < 10; frame++)
        {
            if (playerRolls[rollIndex] == 10) // Страйк
            {
                score += 10 + playerRolls[rollIndex + 1] + playerRolls[rollIndex + 2];
                rollIndex++;
            }
            else if (playerRolls[rollIndex] + playerRolls[rollIndex + 1] == 10) // Спэр
            {
                score += 10 + playerRolls[rollIndex + 2];
                rollIndex += 2;
            }
            else // Опен фрейм
            {
                score += playerRolls[rollIndex] + playerRolls[rollIndex + 1];
                rollIndex += 2;
            }
        }

        playerScore = score;
        EndScore.playerScore = score;
        Debug.Log("Общий счёт игрока: " + score);
    }

    // Подсчёт очков компьютера
    private void CalculateComputerScore()
    {
        int score = 0;
        int rollIndex = 0;

        for (int frame = 0; frame < 10; frame++)
        {
            if (computerRolls[rollIndex] == 10) // Страйк
            {
                score += 10 + computerRolls[rollIndex + 1] + computerRolls[rollIndex + 2];
                rollIndex++;
            }
            else if (computerRolls[rollIndex] + computerRolls[rollIndex + 1] == 10) // Спэр
            {
                score += 10 + computerRolls[rollIndex + 2];
                rollIndex += 2;
            }
            else // Опен фрейм
            {
                score += computerRolls[rollIndex] + computerRolls[rollIndex + 1];
                rollIndex += 2;
            }
        }

        computerScore = score;
        EndScore.computerScore = score;
        Debug.Log("Общий счёт компьютера: " + score);
    }

    // Сброс состояния всех кеглей
    public void ResetPins()
    {
        foreach (Pin pin in pins)
        {
            pin.gameObject.SetActive(true);
            pin.ResetPinState();
        }
    }

    // Убирает сбитые кегли с поля
    private void Shreder()
    {
        foreach (Pin pin in pins)
        {
            if (pin.hasFallen)
            {
                pin.gameObject.SetActive(false);
            }
        }
    }

    // Ожидание перед завершением игры
    IEnumerator WaitBeforeEnd()
    {
        yield return new WaitForSecondsRealtime(3);

        Debug.Log("Игра завершена!");
        SceneManager.LoadScene("End"); // Переход к сцене завершения игры
    }
}
