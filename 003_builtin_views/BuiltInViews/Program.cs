using Microsoft.SharePoint.Client;
using SPMeta2.BuiltInDefinitions;
using SPMeta2.CSOM.Services;
using SPMeta2.Definitions;
using SPMeta2.Enumerations;
using SPMeta2.Models;
using SPMeta2.Syntax.Default;
using System.Collections.ObjectModel;

namespace BuiltInViews
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
        }
        private static ModelNode GetModel()
        {
            return SPMeta2Model.NewWebModel(hostWeb =>
            {
                hostWeb
                    .AddWeb(SimpleWeb, web =>
                    {
                        web
                            .AddList(SimpleList1, list =>
                            {
                                list
                                    .AddListView(BuiltInListViewDefinitions.Lists.AllItems, listView =>
                                    {
                                        var definition = listView.Value as ListViewDefinition;
                                        definition.Fields = new Collection<string>
                                        {
                                            BuiltInInternalFieldNames.LinkTitle,
                                            BuiltInInternalFieldNames.ID  
                                        };
                                    });
                            })
                            .AddList(SimpleList2, list =>
                            {
                                list
                                    .AddListView(BuiltInListViewDefinitions.Lists.AllItems, listView =>
                                    {
                                        var definition = listView.Value as ListViewDefinition;
                                        definition.Fields = new Collection<string>
                                        {
                                            BuiltInInternalFieldNames.LinkTitle,
                                            BuiltInInternalFieldNames.Modified
                                        };
                                    });
                            });
                    });
            });
        }
        private static string group = "Learn SPMeta2";
        private static string subWebUrl = "views09";
        private static string listTitle1 = "Simple List 1";
        private static string listTitle2 = "Simple List 2";
        private static uint lcid = 1033;

        private static WebDefinition SimpleWeb = new WebDefinition
        {
            Title = subWebUrl,
            Description = "",
            Url = subWebUrl,
            LCID = lcid,
            WebTemplate = BuiltInWebTemplates.Collaboration.TeamSite
        };
        public static ListDefinition SimpleList1 = new ListDefinition
        {
            Title = listTitle1,
            CustomUrl = "Lists/SimpleList1",
            ContentTypesEnabled = true,
            TemplateType = BuiltInListTemplateTypeId.GenericList,
            OnQuickLaunch = false
        };
        public static ListDefinition SimpleList2 = new ListDefinition
        {
            Title = listTitle2,
            CustomUrl = "Lists/SimpleList2",
            ContentTypesEnabled = true,
            TemplateType = BuiltInListTemplateTypeId.GenericList,
            OnQuickLaunch = false
        };
    }
}
