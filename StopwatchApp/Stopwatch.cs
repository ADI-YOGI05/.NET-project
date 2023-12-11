internal interface IState
{
    string TextOnFirstButton { get; }
    string TextOnSecondButton { get; }

    IState PressFirstButton();
    IState PressSecondButton();
}

internal class Context : IState
{
    private IState currentState;

    public Context(IState initialState)
    {
        currentState = initialState;
    }

    public string TextOnFirstButton => currentState.TextOnFirstButton;
    public string TextOnSecondButton => currentState.TextOnSecondButton;

    public IState PressFirstButton()
    {
        currentState = currentState.PressFirstButton();
        return currentState;
    }

    public IState PressSecondButton()
    {
        currentState = currentState.PressSecondButton();
        return currentState;
    }
}

internal class InitialScreenState : IState
{
    public string TextOnFirstButton => "Start";
    public string TextOnSecondButton => "Lap";

    public IState PressFirstButton()
    {
        return new RunningState();
    }

    public IState PressSecondButton()
    {
        
        return this;
    }
}


internal class RunningState : IState
{
    public string TextOnFirstButton => "Stop";
    public string TextOnSecondButton => "Lap";

    public IState PressFirstButton()
    {
        return new DisplayingResultState();
    }

    public IState PressSecondButton()
    {
        return new LappingState();
    }
}

internal class LappingState : IState
{
    public string TextOnFirstButton => "Stop";
    public string TextOnSecondButton => "Lap";

    public IState PressFirstButton()
    {
        return new DisplayingResultState();
    }

    public IState PressSecondButton()
    {
        return new LappingState();
    }
}

internal class DisplayingResultState : IState
{
    public string TextOnFirstButton => "Start";
    public string TextOnSecondButton => "Reset";

    public IState PressFirstButton()
    {
        return new RunningState();
    }

    public IState PressSecondButton()
    {
        return new InitialScreenState();
    }
}
