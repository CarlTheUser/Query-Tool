﻿using QueryTool.UI.Command;
using QueryTool.UI.Navigation;
using QueryTool.UI.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace QueryTool.UI.ViewModel
{
    public class MainWindowViewModel : ViewModel, IMainView, IUserNavigation
    {
        
        public ICommand NavigateCommand { get; private set; }

        public ICommand NavigateBackCommand { get; private set; }

        public ICommand NavigateHomeCommand { get; private set; }

        public ICommand ToggleMenuCommand { get; private set; }

        public event EventHandler<PrenavigationEventArgs> Prenavigate;

        public IUserNavigation UserNavigation => this;

        private readonly Stack<NavigationItem> navigationStack = new Stack<NavigationItem>();

        private ApplicationPage ApplicationPage { get; set; }
        
        private Page currentPage = null;

        public Page CurrentPage
        {
            get => currentPage;
            private set
            {
                currentPage = value;
                OnPropertyChanged("CurrentPage");
                OnPropertyChanged("HasBackStack");
            }
        }
        
        private bool CurrentPageInBackStack { get; set; }

        public bool HasBackStack => navigationStack.Count > 1;
        
        private Func<ApplicationPage> HomePageRequestAction = () => ApplicationPage.Nothing;

        public MainWindowViewModel()
        {
            NavigateBackCommand = new RelayCommand(NavigateBack, () => HasBackStack);
            NavigateHomeCommand = new RelayCommand(NavigateHome);

            Navigate(new NavigationItem(ApplicationPage.QueryPad));
        }

        #region Public Behaviors

        public bool Navigate(NavigationItem navigationItem)
        {
            ApplicationPage navigationTarget = navigationItem.NavigationPage;
            if (ApplicationPage != navigationTarget)
            {
                if(!CheckCancelNavigation(navigationTarget))
                {
                    BasePage page = CreatePage(navigationTarget);

                    if (CurrentPageInBackStack = navigationItem.AddToNavigationStack) navigationStack.Push(navigationItem);
                    if (navigationItem.HasParameters) page.GetViewModel().Parameters = navigationItem.Parameters;
                    ApplicationPage = navigationTarget;
                    CurrentPage = page;
                    OnPropertyChanged("HasBackStack");
                    OnPropertyChanged("HomeNavigationVisible");
                    return true;
                }

            }
            return false;
        }

        public void NavigateBack()
        {
            if(navigationStack.Count > 1)
            {

                NavigationItem Current = null;
                if(CurrentPageInBackStack) Current = navigationStack.Pop();
                NavigationItem Last = navigationStack.Pop();
                if(!Navigate(Last))
                {
                    navigationStack.Push(Last);
                    if (CurrentPageInBackStack) navigationStack.Push(Current);
                }
            }
        }

        public void NavigateHome()
        {
            ClearNavigationStack();
            Navigate(new NavigationItem(HomePageRequestAction.Invoke()));
        }

        #endregion

        #region Private Methods

        private void ToggleMenu()
        {

        }

        private void ClearNavigationStack() => navigationStack.Clear();

        private bool CheckCancelNavigation(ApplicationPage page)
        {
            PrenavigationEventArgs eventArgs = new PrenavigationEventArgs(page);
            Prenavigate?.Invoke(this, eventArgs);
            return eventArgs.CancelNavigation;
        }

        private BasePage CreatePage(ApplicationPage applicationPage)
        {
            return (BasePage)Activator.CreateInstance(BasePage.ApplicationPageRegistry[applicationPage]);
        }

        //private void DisplayLogin()
        //{
        //    Navigate(new NavigationItem(ApplicationPage.Login, false));
        //}

        //private ApplicationPage GetAdministratorHomePage() { return ApplicationPage.AdministratorPage; }

        #endregion

        #region Event Handlers

        

        private void Instance_AccountLoggedOut(object sender, EventArgs e)
        {
            ClearNavigationStack();
            //DisplayLogin();
            NotificationHub.GetInstance().ShowMessage("See you again.");
        }

        #endregion

        
    }
}
