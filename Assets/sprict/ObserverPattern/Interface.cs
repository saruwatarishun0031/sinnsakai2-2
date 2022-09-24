using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interface : MonoBehaviour
{
    public interface IObserver<in T>
    {
        //データの発行が完了したことを通知する
        void OnCompleted();
        //データの発行元でエラーが発生したことを通知する
        void OnError(Exception error);
        //データを通知する
        void OnNext(T value);
    }

    public interface IObservable<out T>
    {
        //データの発行を購読する
        IDisposable Subscribe(IObserver<T> observer);
    }

}
