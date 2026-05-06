using System;
using System.Collections.Generic;
using System.Linq;

public class TextMemento
{
    public string Text { get; }
    public DateTime Timestamp { get; }

    public TextMemento(string text)
    {
        Text = text;
        Timestamp = DateTime.Now;
    }
}


public interface IMediator
{
    void Notify(object sender, string action, object data = null);
}


public class DocumentMediator : IMediator
{
    private Canvas _canvas;
    private Caretaker _caretaker;
    private Toolbar _toolbar;

    public void RegisterCanvas(Canvas canvas)
    {
        _canvas = canvas;
    }

    public void RegisterCaretaker(Caretaker caretaker)
    {
        _caretaker = caretaker;
    }

    public void RegisterToolbar(Toolbar toolbar)
    {
        _toolbar = toolbar;
    }

    public void Notify(object sender, string action, object data = null)
    {
        switch (action)
        {
            case "TextChanged":
               
                if (_canvas != null && _caretaker != null)
                {
                    var memento = _canvas.SaveToMemento();
                    _caretaker.AddMemento(memento);
                    Console.WriteLine($"[Mediator] Текст изменён. Состояние сохранено в {memento.Timestamp}");
                }
                break;

            case "UndoRequest":
                
                if (_caretaker != null && _canvas != null)
                {
                    var lastMemento = _caretaker.Undo();
                    if (lastMemento != null)
                    {
                        _canvas.RestoreFromMemento(lastMemento);
                        Console.WriteLine($"[Mediator] Выполнен Undo до состояния от {lastMemento.Timestamp}");
                    }
                    else
                    {
                        Console.WriteLine("[Mediator] Нет сохранённых состояний для Undo");
                    }
                }
                break;

            case "PrintRequest":
                if (_canvas != null)
                {
                    Console.WriteLine($"[Mediator] Запрос на печать: \"{_canvas.Text}\"");
                }
                break;
        }
    }
}


public class Canvas
{
    private string _text;

    public string Text
    {
        get => _text;
        set
        {
            _text = value;
            
            _mediator?.Notify(this, "TextChanged");
        }
    }

    private IMediator _mediator;

    public Canvas(IMediator mediator)
    {
        _mediator = mediator;
        _text = string.Empty;
    }

    
    public TextMemento SaveToMemento()
    {
        return new TextMemento(_text);
    }

    
    public void RestoreFromMemento(TextMemento memento)
    {
        if (memento != null)
        {
            _text = memento.Text;
            Console.WriteLine($"[Canvas] Восстановлен текст: \"{_text}\"");
        }
    }

    public void Print()
    {
        _mediator?.Notify(this, "PrintRequest");
    }
}

public class Caretaker
{
    private Stack<TextMemento> _history = new Stack<TextMemento>();

    public void AddMemento(TextMemento memento)
    {
        _history.Push(memento);
        Console.WriteLine($"[Caretaker] Сохранено состояние. Всего в истории: {_history.Count}");
    }

    public TextMemento Undo()
    {
        if (_history.Count > 0)
        {
            var lastMemento = _history.Pop();
            Console.WriteLine($"[Caretaker] Восстановлено состояние от {lastMemento.Timestamp}");
            return lastMemento;
        }

        Console.WriteLine("[Caretaker] История пуста");
        return null;
    }
}


public class Toolbar
{
    private IMediator _mediator;

    public Toolbar(IMediator mediator)
    {
        _mediator = mediator;
    }

    
    public void ChangeText(string newText, Canvas canvas)
    {
        Console.WriteLine($"[Toolbar] Изменение текста на: \"{newText}\"");
        canvas.Text = newText;
    }

    
    public void Undo()
    {
        Console.WriteLine("[Toolbar] Нажата кнопка Undo");
        _mediator.Notify(this, "UndoRequest");
    }

    
    public void Print(Canvas canvas)
    {
        Console.WriteLine("[Toolbar] Нажата кнопка Печать");
        canvas.Print();
    }
}

