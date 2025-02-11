using System.Windows;
using ReactiveUI;
using System.Windows.Controls;

namespace Client.GUI.ViewModel
{
    public interface IContentChanger
    {
        event Action<UserControl, string> ContentChanged;
    }

    public abstract class ViewModelBase : ReactiveObject, IContentChanger
    {
        #region IContentChanger

        //Событие для отображения нужной страницы
        public event Action<UserControl, string>? ContentChanged;

        protected void RaiseContentChanged(UserControl newContent, string title)
        {
            ContentChanged?.Invoke(newContent, title);
        }

        //Подписка на обновление данных
        protected void SubscribeToContentChanged<TViewModel>(TViewModel viewModel, Action<UserControl, string> updateContent)
            where TViewModel : class
        {
            if (viewModel is IContentChanger contentChanger)
            {
                contentChanger.ContentChanged += updateContent;
            }
        }
        #endregion


        //Закрытие приложения 
        protected static void ExecExit()
        {
            Application.Current.Shutdown();
        }
    }
}
