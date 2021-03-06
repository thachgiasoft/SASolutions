﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SA.Infrastructure;
using System.ComponentModel;
using Microsoft.Practices.Composite.Presentation.Commands;
using Microsoft.Practices.EnterpriseLibrary.Validation;
using System.Collections.ObjectModel;
using SA.Repository.Domain;
using Microsoft.Practices.Unity;
using System.Windows.Input;
using SA.BreadCrumb.View;
using SA.Repository.Repositories;
using SA.Repository.Enums;
using SA.Stock.View.Vendor;
using SA.Stock.ViewModel;
using SA.Stock.View.Product;



namespace SA.Stock.View.Cashier
{
    public class CashierItemsPresenter : ICashierItemsPresenter
    {
        #region Properties
        private IOrderViewModel _orderViewModel;
        private readonly ICashierItemsView _view;
        private readonly IUnityContainer _container;
        #endregion

        #region Constructors
        public CashierItemsPresenter(ICashierItemsView view, IOrderViewModel orderViewModel, IUnityContainer container)
        {
            if (view == null)
            {
                throw new ArgumentNullException("view");
            }
            if (orderViewModel == null)
            {
                throw new ArgumentNullException("orderViewModel");
            }
            if (container == null)
            {
                throw new ArgumentNullException("container");
            }

            this.SearchProductCommand = new DelegateCommand<object>(this.SearchProduct);

            this._orderViewModel = orderViewModel;
            this._container = container;
            this._view = view;
            this._view.SetPresenter(this);
        }
        #endregion

        #region ICrumbViewContent
        public object View 
        {
            get { return _view; } 
        }
        public event EventHandler CloseViewRequested = delegate { };

        #endregion

        #region ICashierItemsViewModel

        #region Commands
        /// <summary>
        /// Search Product command - called when searching a Product is requeired
        /// </summary>
        public ICommand SearchProductCommand { get; set; }
        #endregion

        public IOrderViewModel OrderViewModel
        {
            get
            {
                return this._orderViewModel;
            }
        }

        #endregion

        #region Methods

        private void SearchProduct(object param)
        {
            IProductListPresenter presenter = this._container.Resolve<IProductListPresenter>("IProductListPresenter");
            presenter.CloseViewRequested += delegate(object sender, EventArgs eventArgs)
            {
                if (eventArgs == null)
                    return;

                if ((eventArgs is CloseViewEventArgs) && ((eventArgs as CloseViewEventArgs).CloseViewOption == CloseViewType.Ok))
                {
                    this.OrderViewModel.Item.Produto = presenter.Produto;
                }
            };

            IBreadCrumbPresenter breadCrumb = this._container.Resolve<IBreadCrumbPresenter>();
            if (breadCrumb != null)
                breadCrumb.AddCrumb("Produtos", presenter);
        }

        #endregion

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler temp = PropertyChanged;

            if (temp != null)
            {
                temp(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion

    }
}
