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

class Canvas
{
    private IMediator mediator;
    public string Text { get; private set; }

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
        if (memento != null)
            Text = memento.Text;
    }

    public TextMemento Save()
    {
        return new TextMemento(Text);
    }
}

class Toolbar
{
    private IMediator mediator;

    public Toolbar(IMediator mediator)
    {
        this.mediator = mediator;
    }

    public void EditText(string text)
    {
        mediator.Notify(this, "Edit:" + text);
    }

    public void Print()
    {
        mediator.Notify(this, "Print");
    }

    public void Undo()
    {
        mediator.Notify(this, "Undo");
    }
}

class EditorMediator : IMediator
{
    private Canvas canvas;
    private Caretaker caretaker;

    public EditorMediator(Canvas canvas, Caretaker caretaker)
    {
        this.canvas = canvas;
        this.caretaker = caretaker;
    }

    public void Notify(object sender, string ev)
    {
        if (ev.StartsWith("Edit:"))
        {
            caretaker.Save(canvas.Save());
            var text = ev.Substring(5);
            canvas.SetText(text);
        }
        else if (ev == "TextChanged")
        {
            caretaker.Save(canvas.Save());
        }
        else if (ev == "Undo")
        {
            var memento = caretaker.Undo();
            canvas.Restore(memento);
        }
        else if (ev == "Print")
        {
            Console.WriteLine(canvas.Text);
        }
    }
}

class Program
{
    static void Main()
    {
        var caretaker = new Caretaker();
        var canvas = new Canvas(null);
        var mediator = new EditorMediator(canvas, caretaker);
        canvas = new Canvas(mediator);
        var toolbar = new Toolbar(mediator);

        toolbar.EditText("Hello");
        toolbar.Print();
        toolbar.EditText("World");
        toolbar.Print();
        toolbar.Undo();
        toolbar.Print();
    }
}
