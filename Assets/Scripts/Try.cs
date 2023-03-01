using System;

public enum TryType
{
    Move
}

public class Try
{
    public Action<Creature> Action;
    public TryType Type;

    public Try(TryType type, Action<Creature> action)
    {
        Type = type;
        Action = action;
    }
}