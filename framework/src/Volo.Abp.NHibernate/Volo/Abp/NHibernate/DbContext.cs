using System.Collections.Generic;
using Volo.Abp.DependencyInjection;
using NHibernate;
using Volo.Abp.Data;
using Volo.Abp.MultiTenancy;
using System;

namespace Volo.Abp.NHibernate
{
    public abstract class DbContext<TDbContext> : IDbContext
        where TDbContext: IDbContext

    {
        protected virtual Guid? CurrentTenantId => CurrentTenant?.Id;

        protected virtual bool IsMultiTenantFilterEnabled => DataFilter.IsEnabled<IMultiTenant>();

        protected virtual bool IsSoftDeleteFilterEnabled => DataFilter.IsEnabled<ISoftDelete>();

        public ICurrentTenant CurrentTenant { get; set; }

        public IDataFilter DataFilter { get; set; }

        public DbContext()
        {

        }

        public ISession Session { get; private set; }


         
        public virtual void InitializeSession(ISession session)
        {
            
            Session = session;
            CheckAndSetSoftDelete();
            CheckAndSetMustHaveTenant();
        }
        protected virtual void CheckAndSetSoftDelete()
        {
            if (IsSoftDeleteFilterEnabled)
            {
                ApplyEnableFilter(nameof(ISoftDelete)); //Enable Filters
                ApplyFilterParameterValue(nameof(ISoftDelete), nameof(ISoftDelete.IsDeleted), false); //ApplyFilter
            }
            else
            {
               ApplyDisableFilter(nameof(ISoftDelete)); //Disable filters
            }
        }

        protected virtual void CheckAndSetMustHaveTenant()
        {
            if (IsMultiTenantFilterEnabled)
            {
                ApplyEnableFilter(nameof(IMultiTenant)); //Enable Filters
                ApplyFilterParameterValue(nameof(IMultiTenant), nameof(IMultiTenant.TenantId), CurrentTenantId); //ApplyFilter
            }
            else
            {
                ApplyDisableFilter(nameof(IMultiTenant)); //Disable Filters
            }
        }
        private void ApplyDisableFilter(string filterName)
        {
            if (Session.GetEnabledFilter(filterName) != null)
            {
                Session.DisableFilter(filterName);
            }
        }

        public void ApplyEnableFilter(string filterName)
        {
            if (Session.GetEnabledFilter(filterName) == null)
            {
                Session.EnableFilter(filterName);
            }
        }

        public void ApplyFilterParameterValue(string filterName, string parameterName, object value)
        {
            if (Session == null)
            {
                return;
            }

            var filter = Session.GetEnabledFilter(filterName);
            if (filter != null)
            {
                filter.SetParameter(parameterName, value);
            }
        }




    }
}