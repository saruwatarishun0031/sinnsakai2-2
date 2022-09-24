using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interface : MonoBehaviour
{
    public interface IObserver<in T>
    {
        //�f�[�^�̔��s�������������Ƃ�ʒm����
        void OnCompleted();
        //�f�[�^�̔��s���ŃG���[�������������Ƃ�ʒm����
        void OnError(Exception error);
        //�f�[�^��ʒm����
        void OnNext(T value);
    }

    public interface IObservable<out T>
    {
        //�f�[�^�̔��s���w�ǂ���
        IDisposable Subscribe(IObserver<T> observer);
    }

}
