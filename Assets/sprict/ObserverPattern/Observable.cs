using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Observable : IObservable<int>
{
    //�w�ǂ��ꂽIObserver<int>�̃��X�g
    private List<IObserver<int>> m_observers = new List<IObserver<int>>();

    public IDisposable Subscribe(IObserver<int> observer)
    {
        if (!m_observers.Contains(observer))
            m_observers.Add(observer);
        //�w�ǉ����p�̃N���X��IDisposable�Ƃ��ĕԂ�
        return new Unsubscriber(m_observers, observer);
    }
    ///<summary>
    ///public�֐������A�����K�v�ȂƂ���ɌĂяo��
    ///<summary>
    public void SendNotice()
    {
        //���ׂĂ̔��s��ɑ΂���1,2,3�𔭍s����
        foreach (var observer in m_observers)
        {
            observer.OnNext(2);
        }
       
    }
}
 class Unsubscriber : IDisposable
{
    //���s�惊�X�g
    private List<IObserver<int>> m_observers;
    //Dispose���ꂽ�Ƃ���Remove����IObserver<int>
    private IObserver<int> m_observer;

    public Unsubscriber(List<IObserver<int>> observers, IObserver<int> observer)
    {
        m_observers = observers;
        m_observer = observer;
    }

    public void Dispose()
    {
        //Dispose���ꂽ�甭�s�惊�X�g����Ώۂ̔��s����폜����
        m_observers.Remove(m_observer);
    }
}