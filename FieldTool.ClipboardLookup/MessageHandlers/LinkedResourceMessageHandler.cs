using FieldTool.ClipboardLookup.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Web.Http.Routing;

namespace FieldTool.ClipboardLookup.MessageHandlers
{
    public class LinkedResourceMessageHandler : DelegatingHandler
    {
        protected override async System.Threading.Tasks.Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, System.Threading.CancellationToken cancellationToken)
        {
            HttpResponseMessage response = await base.SendAsync(request, cancellationToken);

            LinkedResource linkedResource;
            IEnumerable<LinkedResource> linkedResources;

            if (response.TryGetContentValue<LinkedResource>(out linkedResource))
            {
                BuildLinks(DynamicCast(linkedResource, linkedResource.GetType()), request);
            }
            else if (response.TryGetContentValue<IEnumerable<LinkedResource>>(out linkedResources))
            {
                foreach (LinkedResource lr in linkedResources)
                {
                    BuildLinks(DynamicCast(lr, lr.GetType()), request);
                }
            }

            return response;
        }

        private void BuildLinks(CompanyInfoDTO companyInfo, HttpRequestMessage request)
        {
            var urlHelper = new UrlHelper(request);
            var url = urlHelper.Link("DefaultApi", new { controller = "Companies", id = companyInfo.CompanyBsid });
            companyInfo.AddSelfLink(url);
        }

        private void BuildLinks(AuditProjectInfoDTO auditProjectInfo, HttpRequestMessage request)
        {
            var urlHelper = new UrlHelper(request);
            var url = urlHelper.Link("DefaultApi", new { controller = "AuditProjects", id = auditProjectInfo.ProjectId });
            auditProjectInfo.AddSelfLink(url);
        }

        //private void BuildLinks(AccountInfo accountInfo, HttpRequestMessage request) {
        //    var urlHelper = new UrlHelper(request);
        //    foreach (string buildingId in accountInfo.BuildingIds) {
        //        var url = urlHelper.Link("DefaultApi", new { controller = "en/properties", id = buildingId });
        //        accountInfo.AddLink("property", url);
        //    }
        //}

        //private void BuildLinks(PropertyInfo propertyInfo, HttpRequestMessage request) {
        //    var urlHelper = new UrlHelper(request);
        //    foreach (ProjectInfo project in propertyInfo.Projects) {
        //        var url = urlHelper.Link("DefaultApi", new { controller = "en/projects", id = project.BensightProjectId });
        //        propertyInfo.AddLink("project", url);
        //    }
        //}

        private dynamic DynamicCast(object entity, System.Type to)
        {
            var openCast = this.GetType().GetMethod("Cast", System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.NonPublic);
            var closeCast = openCast.MakeGenericMethod(to);
            return closeCast.Invoke(entity, new[] { entity });
        }

        private static T Cast<T>(object entity) where T : class
        {
            return entity as T;
        }
    }
}