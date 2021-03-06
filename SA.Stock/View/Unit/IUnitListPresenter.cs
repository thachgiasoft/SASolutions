﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using Microsoft.Practices.Composite.Presentation.Commands;
using SA.BreadCrumb.Model;
using SA.Repository.Domain;

namespace SA.Stock.View.Unit
{
    public interface IUnitListPresenter : ICrumbViewContent, INotifyPropertyChanged
    {
        ObservableCollection<Unidade> Unidades { get; }
        Unidade Unidade { get; set; }

        #region Commands
        /// <summary>
        /// Create command - called when adding a Unit is requeired
        /// </summary>
        ICommand CreateCommand { get; }

        /// <summary>
        /// Read command - called when reading a Unit is requeired
        /// </summary>
        //ICommand ReadCommand { get; }

        /// <summary>
        /// Update command - called when updating a Unit is requeired
        /// </summary>
        ICommand UpdateCommand { get; }

        /// <summary>
        /// Delete command - called when deleting a Unit is requeired
        /// </summary>
        ICommand DeleteCommand { get; }

        /// <summary>
        /// Search command - called when searching is requeired
        /// </summary>
        ICommand SearchCommand { get; }

        /// <summary>
        /// Close command - called when closing is requeired
        /// </summary>
        ICommand CloseCommand { get; }
        #endregion
    }
}
