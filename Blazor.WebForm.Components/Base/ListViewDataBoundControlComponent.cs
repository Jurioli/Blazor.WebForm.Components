using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Blazor.WebForm.UI.ControlComponents
{
    public abstract class ListViewDataBoundControlComponent<TControl> : ListViewBaseDataBoundControlComponent<TControl>
        where TControl : DataBoundControl, new()
    {
        private CallingDataMethodsEventHandler _callingDataMethods;
        private CreatingModelDataSourceEventHandler _creatingModelDataSource;

        //[Parameter]
        //public string SelectMethod
        //{
        //    get
        //    {
        //        return this.Control.SelectMethod;
        //    }
        //    set
        //    {
        //        this.Control.SelectMethod = value;
        //    }
        //}

        //[Parameter]
        //public string ItemType
        //{
        //    get
        //    {
        //        return this.Control.ItemType;
        //    }
        //    set
        //    {
        //        this.Control.ItemType = value;
        //    }
        //}

        [Parameter]
        public string DataMember
        {
            get
            {
                return this.Control.DataMember;
            }
            set
            {
                this.Control.DataMember = value;
            }
        }

        [Parameter]
        public CallingDataMethodsEventHandler OnCallingDataMethods
        {
            get
            {
                return _callingDataMethods;
            }
            set
            {
                if (value != null)
                {
                    this.Control.CallingDataMethods += InvokeCallingDataMethods;
                }
                else
                {
                    this.Control.CallingDataMethods -= InvokeCallingDataMethods;
                }
                _callingDataMethods = value;
            }
        }

        [Parameter]
        public CreatingModelDataSourceEventHandler OnCreatingModelDataSource
        {
            get
            {
                return _creatingModelDataSource;
            }
            set
            {
                if (value != null)
                {
                    this.Control.CreatingModelDataSource += InvokeCreatingModelDataSource;
                }
                else
                {
                    this.Control.CreatingModelDataSource -= InvokeCreatingModelDataSource;
                }
                _creatingModelDataSource = value;
            }
        }

        private void InvokeCallingDataMethods(object sender, CallingDataMethodsEventArgs e)
        {
            _callingDataMethods.Invoke(sender, e);
        }

        private void InvokeCreatingModelDataSource(object sender, CreatingModelDataSourceEventArgs e)
        {
            _creatingModelDataSource.Invoke(sender, e);
        }
    }
}
