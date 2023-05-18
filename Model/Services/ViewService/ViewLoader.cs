using System;
using System.Collections.Generic;
using System.Windows.Controls;
using Microsoft.Extensions.DependencyInjection;

namespace Model.Services.ViewService;

public class ViewLoader : IViewsLoader
{
    public ContentPresenter ContentPresenter { get; set; }

    private readonly Dictionary<WindowTypes, UserControl> _views = new();
    private readonly IServiceProvider _serviceProvider;

    public ViewLoader(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public void ShowMessage(string message)
    {
        throw new NotImplementedException();
    }

    public void LoadView<TView>() where TView: IView 
    {
        var view = _serviceProvider.GetRequiredService<TView>();
        ContentPresenter.Content = view; 
    }
}

public interface IViewModel
{
}

public interface IView
{
}