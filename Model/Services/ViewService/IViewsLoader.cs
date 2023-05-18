using System.Windows.Controls;

namespace Model.Services.ViewService;

public interface IViewsLoader //свой интерфейс для отображения конкретного окна
{
    ContentPresenter ContentPresenter { get; set; }

    /// <summary>
    /// Показ сообщения для пользователя
    /// </summary>
    /// <param name="message">текст сообщения</param>
    void ShowMessage(string message);

    /// <summary>
    /// Загрузка нужной View
    /// </summary>
    /// <param name="view">экземпляр UserControl</param>
    void LoadView<T>() where T: IView ;
}