using Microsoft.SharePoint.Client;
using SPMeta2.CSOM.Services;
using SPMeta2.Definitions;
using SPMeta2.Definitions.Fields;
using SPMeta2.Enumerations;
using SPMeta2.Models;
using SPMeta2.Syntax.Default;
using System;
using System.Collections.ObjectModel;

namespace LookupSiteCol
{
    class Program
    {
        static void Main(string[] args)
        {
            var siteUrl = "http://dev";
            var context = new ClientContext(siteUrl);
            var provisionService = new CSOMProvisionService();
            var model = GetModel();
            provisionService.DeployWebModel(context, model);
            var model2 = GetModel2();
            var context2 = new ClientContext(siteUrl + "/" + subWebUrl);
            provisionService.DeployWebModel(context2, model2);

        }
        private static ModelNode GetModel()
        {
            return SPMeta2Model.NewWebModel(hostWeb =>
            {
                hostWeb
                    .AddWeb(SimpleWeb, web =>
                    {
                        web
                            .AddList(SimpleList, list =>
                            {
                                //web.AddField(SimpleLookupField);
                            });                  
                    });
            });
        }

        private static ModelNode GetModel2()
        {
            return SPMeta2Model.NewWebModel(web =>
            {
                web.AddField(SimpleLookupField);
            });
        }
        private static string group = "Learn SPMeta2";
        private static string subWebUrl = "look08";
        private static string listTitle = "Simple List";
        private static uint lcid = 1053;

        private static WebDefinition SimpleWeb = new WebDefinition
        {
            Title = subWebUrl,
            Description = "",
            Url = subWebUrl,
            LCID = lcid,
            WebTemplate = BuiltInWebTemplates.Collaboration.TeamSite
        };

        public static ListDefinition SimpleList = new ListDefinition
        {
            Title = "Simple List",
            CustomUrl = "Lists/SimpleList",
            ContentTypesEnabled = true,
            TemplateType = BuiltInListTemplateTypeId.GenericList,
            OnQuickLaunch = false
        };

        public static LookupFieldDefinition SimpleLookupField = new LookupFieldDefinition
        {
            Title = "Simple Lookup",
            InternalName = "Simple_Lookup",
            Group = group,
            Id = Guid.NewGuid(),
            LookupListTitle = listTitle,
            LookupWebUrl = "~site",
            AddToDefaultView = true,
            //LookupListUrl
        };

    }
}
