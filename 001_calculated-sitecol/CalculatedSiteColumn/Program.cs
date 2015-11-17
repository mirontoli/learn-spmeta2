using Microsoft.SharePoint.Client;
using SPMeta2.CSOM.Services;
using SPMeta2.Definitions;
using SPMeta2.Definitions.Fields;
using SPMeta2.Enumerations;
using SPMeta2.Models;
using SPMeta2.Syntax.Default;
using System;
using System.Collections.ObjectModel;

namespace CalculatedSiteColumn
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

        /// <summary>
        /// 1033 and 1053 works
        /// Space in Title works
        /// IF statement works in 1033
        /// </summary>
        /// <returns></returns>
        private static ModelNode GetModel()
        {
            return SPMeta2Model.NewWebModel(hostWeb =>
            {
                hostWeb
                    .AddWeb(SimpleWeb, web =>
                    {
                        web
                        .AddField(SimpleTextField)
                        .AddField(SimpleCalculatedField)
                        .AddContentType(SimpleContenType, ct =>
                        {
                            ct
                                .AddContentTypeFieldLink(SimpleTextField)
                                .AddContentTypeFieldLink(SimpleCalculatedField);
                        })
                        .AddList(SimpleList, list =>
                        {
                            list
                                .AddContentTypeLink(SimpleContenType);
                        });
                    });
            });
        }
        
        private static string group = "Learn SPMeta2";
        private static string subWebUrl = "calc09";
        private static uint lcid = 1033;
        private static string simpleTextFieldInternalName = "Tolle_SimpleTextField";
        private static string simpleTextFieldTitle = "Simple Text Field";
        private static string calculatedInternalName = "Tolle_Calculated";
        private static string quote = "&quot;";

        private static WebDefinition SimpleWeb = new WebDefinition
        {
            Title = subWebUrl,
            Description = "",
            Url = subWebUrl,
            LCID = lcid,
            WebTemplate = BuiltInWebTemplates.Collaboration.TeamSite
        };

        /// <summary>
        /// A simple model
        /// </summary>

        /// <summary>
        /// A simple text site column
        /// </summary>
        private static TextFieldDefinition SimpleTextField = new TextFieldDefinition
        {
            Id = Guid.NewGuid(),
            Title = simpleTextFieldTitle,
            InternalName = "Tolle_SimpleTextField",
            Group = group,
            Description = "",
            AddToDefaultView = true,
        };

        /// <summary>
        /// A simple calculated field
        /// </summary>
        private static CalculatedFieldDefinition SimpleCalculatedField = new CalculatedFieldDefinition
        {
            Id = Guid.NewGuid(),
            Title = "Calculated Field",
            InternalName = calculatedInternalName,
            Group = group,
            Required = false,
            FieldReferences = new Collection<string> { $"{simpleTextFieldInternalName}" },
            Formula = $"=IF([{simpleTextFieldInternalName}]={quote}hello{quote}, 1)",
            OutputType = BuiltInFieldTypes.Number,
            AddFieldOptions = BuiltInAddFieldOptions.AddFieldInternalNameHint,
            AddToDefaultView = true
        };

        /// <summary>
        /// A simple content type, that has a simple text site column and a simple calculated site column
        /// </summary>
        public static ContentTypeDefinition SimpleContenType = new ContentTypeDefinition
        {
            Name = "Simple Content Type",
            Id = Guid.NewGuid(),
            ParentContentTypeId = BuiltInContentTypeId.Item,
            Group = group
        };

        /// <summary>
        /// A simple list that should have the simple content type
        /// </summary>
        public static ListDefinition SimpleList = new ListDefinition
        {
            Title = "Simple List",
            CustomUrl = "Lists/SimpleList",
            ContentTypesEnabled = true,
            TemplateType = BuiltInListTemplateTypeId.GenericList,
            OnQuickLaunch = false
        };
    }
}
