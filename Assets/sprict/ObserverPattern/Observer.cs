using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Observer :IObserver<int>
{
    private string m_name;
    public Observer(string name)
    {
        m_name = name;
    }

    public void OnCompleted()
    {
        Console.WriteLine($"{m_name}が通知の受け取りを完了しました");
    }

    public void OnError(Exception error)
    {
        Console.WriteLine($"{m_name}が次のエラーを受信しました:{error.Message}");
    }

    public void OnNext(int value)
    {
        Console.WriteLine($"{m_name}が{value}を受け取りました");
        GameManager.Instance._Text.text = $"{m_name}が残り{value}";
    }
    
}
