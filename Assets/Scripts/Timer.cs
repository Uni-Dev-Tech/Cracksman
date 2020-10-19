using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    [Header("Показывает оставшееся время")]
    [SerializeField]
    private Text minutesLeftText;
    [SerializeField]
    private Text secondsLeftText;

    [Header("Показывает оставшиеся ходы")]
    [SerializeField]
    private Text stepsLeftText;

    [HideInInspector] public float timeForLevel; // время на прохождение (секунды)

    [HideInInspector] public float stepsLeft; // количество ходов

    // Оставшиеся минуты и секунды
    private float timeMinuteLeft = 0f;
    private float timeSecondsLeft = 0f;

    // для сохранения уже прошедшего времени Time.time
    private int timeDifference = 0;

    [SerializeField] private Lock lockSettings;
    private void Start()
    {
        // считаем количество минут и секунд
        timeMinuteLeft = Mathf.Floor(timeForLevel / 60f);
        timeSecondsLeft = timeForLevel - (timeMinuteLeft * 60f);
        timeDifference = Mathf.RoundToInt(Time.time);

        // выводим начальные данные
        minutesLeftText.text = timeMinuteLeft.ToString();
        secondsLeftText.text = timeSecondsLeft.ToString();
        stepsLeftText.text = stepsLeft.ToString();
    }
    void Update()
    {
        // отсчитываем секунды 
        secondsLeftText.text = (timeSecondsLeft - (Mathf.Round(Time.time) - timeDifference)).ToString();

        // проверяем остаток ходов
        if (stepsLeft < 1)
            lockSettings.EnableLose();

        // в случае обнудения секунд
        if((timeSecondsLeft - (Mathf.Round(Time.time) - timeDifference)) == 0)
        {
            //вычитаем минуту, добавляем 60 секунд, выводим данные и в timeDifference записываем 
            //время прошедшее с момента начала игры, тем самым "обнуляя" Time.time
            if(timeMinuteLeft > 0)
            {
                timeMinuteLeft--;
                minutesLeftText.text = timeMinuteLeft.ToString();
                timeSecondsLeft = 60;
                secondsLeftText.text = timeSecondsLeft.ToString();
                timeDifference = Mathf.RoundToInt(Time.time);
            }

            // если минут и секунд не осталось - проигрыш
            if(timeMinuteLeft == 0 && (timeSecondsLeft - (Mathf.Round(Time.time) - timeDifference)) == 0)
            {
                lockSettings.EnableLose();
            }
        }
    }
    /// <summary>
    /// Уменьшает и выводит количество оставшихся ходов
    /// </summary>
    public void DecreaseSteps()
    {
        stepsLeft--;
        stepsLeftText.text = stepsLeft.ToString();
    }
}
