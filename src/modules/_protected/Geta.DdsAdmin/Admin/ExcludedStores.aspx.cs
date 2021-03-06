using System;
using EPiServer;
using EPiServer.Data;
using EPiServer.Shell.WebForms;
using Geta.DdsAdmin.Dds;
using Geta.DdsAdmin.Dds.Interfaces;
using Geta.DdsAdmin.Dds.Services;

namespace Geta.DdsAdmin.Admin
{
    public partial class ExcludedStores : WebFormsBase
    {
        private readonly IExcludedStoresService excludedStoresService;

        public ExcludedStores()
        {
            excludedStoresService = new ExcludedStoresService();
        }

        protected void AddClick(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(item.Text))
            {
                excludedStoresService.Add(new ExcludedStore
                {
                    Filter = item.Text.Trim(),
                    Id = Identity.NewIdentity()
                });
            }

            item.Text = string.Empty;
        }

        protected override void OnPreInit(EventArgs e)
        {
            base.OnPreInit(e);
            MasterPageFile = UriSupport.ResolveUrlFromUIBySettings("MasterPages/EPiServerUI.master");
        }

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
            LoadData();
        }

        protected void RemoveClick(object sender, EventArgs e)
        {
            excludedStoresService.Delete(list.SelectedValue);
        }

        private void LoadData()
        {
            list.DataSource = excludedStoresService.GetAll();
            list.DataBind();
        }
    }
}
