using System;
using UnityEngine;

[Serializable]
public class SerializableDateTime
{
    [SerializeField] private string _dateTimeString;

    public DateTime DateTime
    {
        get => DateTime.Parse(_dateTimeString);
        set => _dateTimeString = value.ToString("o");
    }

    public SerializableDateTime(DateTime dateTime)
    {
        DateTime = dateTime;
    }

    public override string ToString()
    {
        return DateTime.ToString();
    }
}