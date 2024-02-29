namespace Assistant.WpfApp.ViewHelpers;

public interface IAbstractFactory<T>
{
    T Create();
}