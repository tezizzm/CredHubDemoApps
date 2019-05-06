using System.Collections.Generic;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Steeltoe.Extensions.Configuration.CloudFoundry;

namespace CredHubDemoUI
{
    [HtmlTargetElement("service-credentials", Attributes = "credentials")]
    public class ServiceCredentialsTagHelper : TagHelper
    {
        [HtmlAttributeName("credentials")]
        public Dictionary<string, Credential> Credentials { set; get; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.Content.AppendHtml("<ul>");
            if (Credentials != null)
            {
                foreach (var pair in Credentials)
                {
                    GenerateCredentialHtml(pair.Key, pair.Value, output);
                }
            }
            output.Content.AppendHtml("</ul>");

            base.Process(context, output);
        }

        private void GenerateCredentialHtml(string key, Credential credential, TagHelperOutput output)
        {
            if (!string.IsNullOrEmpty(credential.Value))
            {
                output.Content.AppendHtml("<li>");
                output.Content.Append(key + "=" + credential.Value);
                output.Content.AppendHtml("</li>");
            }
            else
            {

                output.Content.AppendHtml("<li>");
                output.Content.Append(key);
                output.Content.AppendHtml("</li>");

                output.Content.AppendHtml("<ul>");
                foreach (var pair in credential)
                {
                    GenerateCredentialHtml(pair.Key, pair.Value, output);
                }
                output.Content.AppendHtml("</ul>");
            }
        }
    }
}
