using System;
using System.Collections.Generic;

interface IMediator
{
    void Notify(object sender, string ev);
}

class TextMemento
{
    public string Text { get; }
    public DateTime Timestamp { get; }

    public TextMemento(string text)
    {
        Text = text;
        Timestamp = DateTime.Now;
    }
}

class Canvas
{
    private IMediator mediator;
    public string Text { get; private set; } = "";

    public Canvas(IMediator mediator)
    {
        this.mediator = mediator;
    }

    public void SetText(string text)
    {
        Text = text;
        mediator.Notify(this, "TextChanged");
    }

    public void Restore(TextMemento memento)
    {
        Text = memento.Text;
    }
}

class Caretaker
{
    private Stack<TextMemento> history = new Stack<TextMemento>();

    public void Save(TextMemento memento)
    {
        history.Push(memento);
    }

    public TextMemento Undo()
    {
        if (history.Count > 0)
            return history.Pop();
        return null;
    }
}

class Toolbar
{
    private IMediator mediator;

    public Toolbar(IMediator mediator)
    {
        this.mediator = mediator;
    }

    public void ChangeText(string text)
    {
        mediator.Notify(text, "ChangeText");
    }

    public void Undo()
    {
        mediator.Notify(this, "Undo");
    }
}

class EditorMediator : IMediator
{
    public Canvas Canvas { get; set; }
    public Caretaker Caretaker { get; set; }

    public void Notify(object sender, string ev)
    {
        if (ev == "ChangeText")
        {
            Canvas.SetText((string)sender);
        }
        else if (ev == "TextChanged")
        {
            Caretaker.Save(new TextMemento(Canvas.Text));
        }
        else if (ev == "Undo")
        {
            var memento = Caretaker.Undo();
            if (memento != null)
                Canvas.Restore(memento);
        }
    }
}

class Program
{
    static void Main()
    {
        var mediator = new EditorMediator();
        var canvas = new Canvas(mediator);
        var caretaker = new Caretaker();
        var toolbar = new Toolbar(mediator);

        mediator.Canvas = canvas;
        mediator.Caretaker = caretaker;

        toolbar.ChangeText("A");
        toolbar.ChangeText("B");
        toolbar.ChangeText("C");

        toolbar.Undo();
        toolbar.Undo();

        Console.WriteLine(canvas.Text);
    }
}
